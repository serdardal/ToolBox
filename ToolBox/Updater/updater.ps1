$localChilds = Get-ChildItem | Where-Object {$_.Name -ne 'Updater'}
$localChilds | Remove-Item -Recurse

$sourceChilds = Get-ChildItem -Path 'C:\SomeSharedToolsFolder\ToolBoxPublish' | Where-Object {$_.Name -ne 'Updater'}
$sourceChilds | Copy-Item -Destination '.' -Recurse

.\ToolBox.exe

# close powershell terminal
Stop-Process -Id $PID