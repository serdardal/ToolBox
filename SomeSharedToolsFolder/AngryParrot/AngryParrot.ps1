Param(
    [Parameter(Mandatory)]
    [string]$Message
)

for($i = 0; $i -lt 5; $i++){
	Write-Host $Message.ToUpper() -ForegroundColor Red	
}