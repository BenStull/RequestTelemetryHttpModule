$httpModuleName = "HttpRequestTelemetry"

[System.Reflection.Assembly]::Load("System.EnterpriseServices, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")            
$publish = New-Object System.EnterpriseServices.Internal.Publish

$GacAssemblyNames = @(
	"BenStull.HttpRequestTelemetry.AspNetHttpModule.dll",
	"BenStull.HttpRequestTelemetry.Domain.dll",
	"BenStull.HttpRequestTelemetry.Model.dll"
)

foreach ($assembly in $GacAssemblyNames) {
  $assemblyPath = Join-Path $PSScriptRoot $assembly

  if (!(Test-Path $assemblyPath)) {
	  throw "Could not find assembly $assemblyPath"
  }

  $publish.GacRemove($assemblyPath)
}

Remove-WebManagedModule -Name $httpModuleName
Restart-Service w3svc
