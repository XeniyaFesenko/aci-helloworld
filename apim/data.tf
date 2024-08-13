# Resource groups
data "azurerm_resource_group" "rg" {
  name     = "EIS_DEVTEST-INT-EAST-SANDBOX-RG"
}

# Subnets
data "azurerm_subnet" "subnet" {
  resource_group_name  = "EIS_DEVTEST-INT-EAST-SANDBOX-RG"
  name                 = "ACI-Private"
  virtual_network_name = "vnet"
}

#APIMs
data "azurerm_api_management" "apim" {
  name = "xeniya-001"
  resource_group_name = "EIS_DEVTEST-INT-EAST-SANDBOX-RG"
}