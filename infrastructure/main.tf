terraform {
  required_providers {
    azuread = {
      source = "hashicorp/azuread"
      version = "2.34.0"
    }
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "~> 3.27.0"
    }
  }

  required_version = ">= 1.3.2"
}

provider "azuread" {
  tenant_id = "4c8dc4f6-3fc2-46c2-8b98-39e0390a01d9"
}

provider "azurerm" {
  features {}
}

data "azurerm_subscription" "current" {
}

resource "azurerm_resource_group" "hotelsbooking" {
  name     = "hotelsbooking"
  location = "westeurope"
}

resource "azurerm_container_registry" "hotelsbooking_acr" {
  name                = "hotelsbooking"
  resource_group_name = azurerm_resource_group.hotelsbooking.name
  location            = azurerm_resource_group.hotelsbooking.location
  sku                 = "Basic"
  admin_enabled       = true
}

resource "azurerm_service_plan" "hotelsbooking_app_plan" {
  name                = "hotelsbooking-plan"
  resource_group_name = azurerm_resource_group.hotelsbooking.name
  location            = azurerm_resource_group.hotelsbooking.location
  os_type             = "Linux"
  sku_name            = "F1"
  depends_on = [
    azurerm_resource_group.hotelsbooking
  ]
}

resource "azurerm_linux_web_app" "hotelsbooking_app" {
  name                = "hotelsbooking-app"
  resource_group_name = azurerm_resource_group.hotelsbooking.name
  location            = azurerm_service_plan.hotelsbooking_app_plan.location
  service_plan_id     = azurerm_service_plan.hotelsbooking_app_plan.id
  https_only          = true

  identity {
    type = "SystemAssigned"
  }

  site_config {
    always_on           = false
    container_registry_use_managed_identity = true
  }

  logs {
    application_logs {
      file_system_level = "Information"
    }
    http_logs {

      file_system {
        retention_in_days = 7
        retention_in_mb   = 35
      }
    }
  }

  app_settings = {
    DOCKER_REGISTRY_SERVER_PASSWORD = azurerm_container_registry.hotelsbooking_acr.admin_password
    DOCKER_REGISTRY_SERVER_URL      = "https://hotelsbooking.azurecr.io"
    DOCKER_REGISTRY_SERVER_USERNAME = "hotelsbooking"
    WEBSITES_PORT = 3000
    SECRET_KEY_BASE                 = "${var.secret_key_base}"
  }

  depends_on = [
    azurerm_resource_group.hotelsbooking,
    azurerm_service_plan.hotelsbooking_app_plan
  ]
}

resource "azurerm_role_assignment" "hotelsbooking_app_acr_pull" {
  scope              = data.azurerm_subscription.current.id
  role_definition_name = "AcrPull"
  principal_id       = azurerm_linux_web_app.hotelsbooking_app.identity.0.principal_id
}