Param( 
    [Parameter(Mandatory = $true, Position = 1, HelpMessage = "Array of test project names")] 
    [String[]] $ProjectNameList
)

$outdir = Split-Path $scriptpath
$outdir = $outdir + "\..\test_results"
if (!(Test-Path $outdir)) { mkdir $outdir -ErrorAction Continue }
$outdir = (Resolve-Path $outdir).Path

$ProjectNameFilter = $ProjectNameList -join '|'
$TestProjectDirList = Get-ChildItem `
    -Path ".." `
    -Recurse `
    -ErrorAction SilentlyContinue `
    | Where-Object { $_.PSISContainer -and $_.Name -match $ProjectNameFilter }
echo "Loop starting"
foreach ($TestProjectDir in $TestProjectDirList) {
    
    $projFilePath = $TestProjectDir.FullName + "\" + $TestProjectDir.Name + ".csproj"

    echo $projFilePath
	 
    # Have to account for spaces in the path
    $outpath = '"' + $outdir + "\" + $TestProjectDir.Name + '\"'

    dotnet test $projFilePath --no-build --no-restore  --logger:trx --results-directory $outpath
		
    $execResults += $?
}

foreach ($res in $execResults) {
    if (!$res) {
        exit(-1)
    }
}
return 0