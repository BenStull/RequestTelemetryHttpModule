﻿$httpModuleName = "HttpRequestTelemetry"

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

  # Register the assembly in the GAC
  $publish.GacInstall($assemblyPath)
}

$existingModule = Get-WebManagedModule -Name $httpModuleName

if ($existingModule -ne $null) {
	Remove-WebManagedModule $httpModuleName
}

# Register the Http Module
New-WebManagedModule -Name $httpModuleName -Type "BenStull.HttpRequestTelemetry.AspNetHttpModule.HttpModule.AspNetHttpModule,BenStull.HttpRequestTelemetry.AspNetHttpModule,Version=1.0.0.0,Culture=neutral,PublicKeyToken=3244448e74f08f32" -Precondition "integratedMode,managedHandler"

Restart-Service w3svc
