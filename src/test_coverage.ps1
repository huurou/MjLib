Set-Location (Split-Path -Parent $MyInvocation.MyCommand.Path)
dotnet test --collect:"XPlat Code Coverage" 
$coverageFile = (Get-ChildItem ".\MjLib.Test\TestResults" -Filter *.xml -Recurse -File | Sort-Object -Descending LastWriteTime)[0].FullName
$coverageDir = (Split-Path -Path $coverageFile)
reportgenerator -reports:$coverageFile -targetdir:$coverageDir -reporttypes:Html
Invoke-Item ($coverageDir + "\index.html")