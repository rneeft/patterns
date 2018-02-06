##-----------------------------------------------------------------------
## <copyright file="ApplyVersionToAssemblies.ps1">(c) Microsoft Corporation. This source is subject to the Microsoft Permissive License. See http://www.microsoft.com/resources/sharedsource/licensingbasics/sharedsourcelicenses.mspx. All other rights reserved.</copyright>
## Original script: https://msdn.microsoft.com/Library/vs/alm/Build/scripts/index
##-----------------------------------------------------------------------
# Look for a 0.0.0-*.0 OR 0.0.0.0 pattern in the build number. 
# If found use it to version the assemblies.
#
# For example, if the 'Build number format' build process parameter 
# $(BuildDefinitionName)_$(Year:yyyy).$(Month).$(DayOfMonth)$(Rev:.r)
# then your build numbers come out like this:
# "Build HelloWorld_2015.07.19.1"
# This script would then apply version 2015.07.19.1 to your assemblies.

# Other example, if the 'Build number format' build process parameter 
# $(BuildDefinitionName)_1.2.0-ci$(rev:.r)
# then your build numbers come out like this:
# "Build HelloWorld_1.2.0-ci.1"
# This script would then apply version 1.2.0.1 to your assemblies and set 1.2.0-ci1 as Informational Version

# Enable -Verbose option
[CmdletBinding()]

# Regular expression pattern to find the version in the build number 
# and then apply it to the assemblies
$BuildVersionRegex = "(\d+\.\d+\.\d+\-\w*\.\d+)|(\d+\.\d+\.\d+\.\d+)"
$InformationalVersionRegex = "(\d+\.\d+\.\d+\-\w*\d+)"
$AssemblyVersionRegex = "\d+\.\d+\.\d+\.\d+"
$ShortAssemblyVersionRegex = "\d+\.\d+\.\d+"
$VersionCodeRegex = "android:versionCode=.\d."

# If this script is not running on a build server, remind user to 
# set environment variables so that this script can be debugged
if(-not ($Env:BUILD_SOURCESDIRECTORY -and $Env:BUILD_BUILDNUMBER))
{
	Write-Host "You must set the following environment variables"
	Write-Host "to test this script interactively."
	Write-Host '$Env:BUILD_SOURCESDIRECTORY - For example, enter something like:'
	Write-Host '$Env:BUILD_SOURCESDIRECTORY = "C:\code\FabrikamTFVC\HelloWorld"'
	Write-Host '$Env:BUILD_BUILDNUMBER - For example, enter something like:'
	Write-Host '$Env:BUILD_BUILDNUMBER = "Build HelloWorld_0000.00.00.0"'
	exit 1
}

# Make sure path to source code directory is available
if (-not $Env:BUILD_SOURCESDIRECTORY)
{
	Write-Host ("BUILD_SOURCESDIRECTORY environment variable is missing.")
	exit 1
}
elseif (-not (Test-Path $Env:BUILD_SOURCESDIRECTORY))
{
	Write-Host "BUILD_SOURCESDIRECTORY does not exist: $Env:BUILD_SOURCESDIRECTORY"
	exit 1
}
Write-Host "BUILD_SOURCESDIRECTORY: $Env:BUILD_SOURCESDIRECTORY"

# Make sure there is a build number
if (-not $Env:BUILD_BUILDNUMBER)
{
	Write-Host ("BUILD_BUILDNUMBER environment variable is missing.")
	exit 1
}
Write-Host "BUILD_BUILDNUMBER: $Env:BUILD_BUILDNUMBER"

# Get and validate the version data
$VersionData = [regex]::matches($Env:BUILD_BUILDNUMBER,$BuildVersionRegex)
switch($VersionData.Count)
{
   0        
	  { 
		 Write-Host "Could not find version number data in BUILD_BUILDNUMBER."
		 exit 1
	  }
   1 {}
   default 
	  { 
		 Write-Host "Found more than instance of version data in BUILD_BUILDNUMBER." 
		 Write-Host "Will assume first instance is version."
	  }
}
$BuildNumber = [string][regex]::matches($VersionData[0],"\d+$")
$BuildNumber = $BuildNumber.Trim()
$NewInformationalVersion = [string][regex]::matches($VersionData[0],"(\d+\.\d+\.\d+\-\w*)|(\d+\.\d+\.\d+)")

$TypeOfBuild = [regex]::matches($VersionData[0],"(\d+\.\d+\.\d+\-\w*)")
switch($TypeOfBuild.Count)
{
   0        
	  { 
		 $NewInformationalVersion = $NewInformationalVersion.Trim()
	  }
   1  {
	     $NewInformationalVersion = $NewInformationalVersion.Trim() + $BuildNumber
	  }
}

$NewAssemblyVersion = [string][regex]::matches($VersionData[0],$ShortAssemblyVersionRegex)
$NewAssemblyVersion = $NewAssemblyVersion.Trim() + "." + $BuildNumber

Write-Host "Informational Version: $NewInformationalVersion"
Write-Host "Assembly Version: $NewAssemblyVersion"

# Apply the version to the assembly property files
$files = gci $Env:BUILD_SOURCESDIRECTORY -recurse -include "src" | 
	?{ $_.PSIsContainer } | 
	foreach { gci -Path $_.FullName -Recurse -include *AssemblyInfo.* }
if($files)
{
	Write-Host "Will apply $NewInformationalVersion to $($files.count) AssemblyInfo files."

	foreach ($file in $files) {
		$filecontent = Get-Content($file)
		attrib $file -r
		$filecontent -replace $InformationalVersionRegex, $NewInformationalVersion | Out-File $file
		$filecontent = Get-Content($file)
		$filecontent -replace $AssemblyVersionRegex, $NewAssemblyVersion | Out-File $file
		Write-Host "$file - version applied in AssemblyInfo"
	}
}
else
{
	Write-Host "Found no AssemblyInfo files."
}

# Apply the version to the Android manifest files
$NewVersionCode = "android:versionCode=`"" + $Env:BUILD_BUILDID + "`""
$files = gci $Env:BUILD_SOURCESDIRECTORY -recurse -include "src" | 
	?{ $_.PSIsContainer } | 
	foreach { gci -Path $_.FullName -Recurse -include *AndroidManifest.* }
if($files)
{
	Write-Host "Will apply $NewInformationalVersion to $($files.count) Android manifest files."

	foreach ($file in $files) {
		$filecontent = Get-Content($file)
		attrib $file -r
		$filecontent = $filecontent -replace $InformationalVersionRegex, $NewInformationalVersion
		$filecontent = $filecontent -replace $VersionCodeRegex, $NewVersionCode
		# Prevent a newline at the end of the file, so don't use the piped Out-File
		[io.file]::WriteAllText($file,$filecontent)
		Write-Host "$file - version applied in Android Manifest"
	}
}
else
{
	Write-Host "Found no Android Manifest files."
}