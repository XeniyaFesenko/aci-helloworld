terraform {
  required_version = ">= 0.13"
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = "3.115.0"
    
    }
  }
 backend "azurerm" {
      resource_group_name  = "poc"
      storage_account_name = "xentfs"
      container_name       = "tfstate"
      key                  = "terraform.tfstate"
  }  
}

#Configure the Azure provider
provider "azurerm" { 
  environment = "public"  
  features {
     resource_group {
       prevent_deletion_if_contains_resources = false
     }
   }
    skip_provider_registration = true
    subscription_id = "96cb683f-4550-464a-8fcf-a7054553e9e6"   
}
