resource "azurerm_resource_group" "rg" {
  name     = var.resource_group_name
  location = var.location
}

# SQL Server
resource "azurerm_mssql_server" "sqlserver" {
  name                         = "sqlservermatricula2025"
  resource_group_name          = azurerm_resource_group.rg.name
  location                     = azurerm_resource_group.rg.location
  version                      = "12.0"
  administrator_login          = var.sql_admin_user
  administrator_login_password = var.sql_admin_password
}

# Base de datos
resource "azurerm_mssql_database" "sqldb" {
  name           = "MatriculaDb"
  server_id      = azurerm_mssql_server.sqlserver.id
  sku_name       = "S0"
}

# Plan de App Service
resource "azurerm_service_plan" "asp" {
  name                = "asp-matricula"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  os_type             = "Linux"
  sku_name            = "B1"
}

# App Service para .NET API
resource "azurerm_linux_web_app" "app" {
  name                = "matricula-api-app"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  service_plan_id     = azurerm_service_plan.asp.id

  site_config {
    application_stack {
      dotnet_version = "8.0"
    }
  }

  app_settings = {
    "ConnectionStrings__DefaultConnection" = "Server=tcp:${azurerm_mssql_server.sqlserver.name}.database.windows.net,1433;Initial Catalog=${azurerm_mssql_database.sqldb.name};Persist Security Info=False;User ID=${var.sql_admin_user};Password=${var.sql_admin_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
