$old_location = Get-Location
$scriptpath = $MyInvocation.MyCommand.Path
$dir = Split-Path $scriptpath

$UnitTestProjectNameList = Get-ChildItem `
	-Path (Join-Path -Path $dir -ChildPath "..") `
	-Recurse `
	-ErrorAction SilentlyContinue `
	-Include *Test* `
	| Where-Object { $_.extension -eq ".csproj"} `
	| Select-object -Expand BaseName

cd $dir
.\Projects.ps1 -ProjectNameList $UnitTestProjectNameList
cd $old_location