Param([switch]$TurnTheLightOn)

Write-Host 'The light is ' -NoNewLine

if($TurnTheLightOn){
	Write-Host 'ON' -ForegroundColor Green
}else{
	Write-Host 'OFF' -ForegroundColor Red
}