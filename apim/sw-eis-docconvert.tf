# #ACR
# resource "azurerm_container_registry" "acr" {
#   name                = "vaeisdocconvertacrdev"
#   resource_group_name = data.azurerm_resource_group.rg.name
#   location            = data.azurerm_resource_group.rg.location
#   sku                 = "Standard"
#   admin_enabled       = true
#   tags = local.devtesttags
# }

#ACI

resource "azurerm_container_group" "container" {
  name                = "va-eis-doc-convert-aci-dev"
  location            = data.azurerm_resource_group.rg.location
  resource_group_name = data.azurerm_resource_group.rg.name
  ip_address_type     = "Private"
  os_type             = "Linux"
  restart_policy      = var.restart_policy
  subnet_ids          = [data.azurerm_subnet.subnet.id]
  image_registry_credential {
    username = var.username
    password = var.password
    server   = var.server
  }
  container {
    name   = "doc-convert-container"
    image  = var.image
    cpu    = var.cpu_cores
    memory = var.memory_in_gb

    ports {
      port     = var.port
      protocol = "TCP"
    }
    environment_variables = {
      ASPNETCORE_URLS = "http://0.0.0.0:80"
    }
  }

    tags = local.devtesttags
}

#API
resource "azurerm_api_management_api" "api" {
  name                = "doc-convert-api"
  resource_group_name = data.azurerm_api_management.apim.resource_group_name
  api_management_name = data.azurerm_api_management.apim.name
  revision            = "1"
  display_name        = "Doc Convert API"
  protocols           = ["https"]

  import {
    content_format = "openapi+json"
    content_value  = <<JSON
    {
    "openapi": "3.0.1",
    "info": {
        "title": "doc-convert",
        "description": "",
        "version": "1.0"
    },
    "servers": [
        {
          "url": "https://xeniya-001.azure-api.net"
        }
    ],
    "paths": {
        "/ping": {
            "get": {
                "summary": "Ping",
                "operationId": "ping",
                "responses": {
                    "200": {
                        "description": null
                    }
                }
            }
        },
        "/ConvertDocument/ConvertTiffToPdf": {
            "post": {
                "summary": "converttifftopdf",
                "operationId": "converttifftopdf",
                "responses": {
                    "200": {
                        "description": null
                    }
                }
            }
        },
        "/ConvertDocument/ConvertWordToPdf": {
            "post": {
                "summary": "convertwordtopdf",
                "operationId": "convertwordtopdf",
                "responses": {
                    "200": {
                        "description": null
                    }
                }
            }
        }
    },
    "components": {
        "securitySchemes": {
            "apiKeyHeader": {
                "type": "apiKey",
                "name": "Ocp-Apim-Subscription-Key",
                "in": "header"
            },
            "apiKeyQuery": {
                "type": "apiKey",
                "name": "subscription-key",
                "in": "query"
            }
        }
    },
    "security": [
        {
            "apiKeyHeader": []
        },
        {
            "apiKeyQuery": []
        }
    ]
  }
  JSON
  }

  subscription_key_parameter_names {
    header = "api-key"
    query  = "api-key"
  }

  
}

resource "azurerm_api_management_backend" "this" {
  name                = "doc-convert-backend"
  resource_group_name = data.azurerm_api_management.apim.resource_group_name
  api_management_name = data.azurerm_api_management.apim.name
  protocol            = "http"
  url                 = "http://${azurerm_container_group.container.ip_address}/api"
}

resource "azurerm_api_management_api_policy" "this" {
  api_name            = azurerm_api_management_api.api.name
  api_management_name = azurerm_api_management_api.api.api_management_name
  resource_group_name = azurerm_api_management_api.api.resource_group_name
  depends_on = [ azurerm_api_management_backend.this ]
  xml_content = <<XML
      <policies>
        <inbound>
            <base />
            <set-backend-service backend-id="doc-convert-backend" />
        </inbound>
        <backend>
            <base />
        </backend>
        <outbound>
            <base />
        </outbound>
      </policies>
  XML
}
