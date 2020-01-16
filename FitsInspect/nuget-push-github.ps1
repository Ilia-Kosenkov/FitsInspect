Get-ChildItem nupkg\*.nupkg | Sort-Object -Property LastWriteTime -Desc | Select-Object -First 1 | ForEach-Object {nuget push $_.FullName -source "GPR"}
