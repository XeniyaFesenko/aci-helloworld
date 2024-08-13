locals {
  location = "East US"

  devtesttags = {
    CKID           = "195"
    Environment    = "DEVTEST"
    ProjectName    = "Veterans Experience Integration Solution"
    VAAzureProgram = "EIS"
    VAECID         = "AZG20181226001"
    #Terraform      = "veis-terraform-logic-apps/devtest"
  }
  prodtags = {
    CKID           = "195"
    Environment    = "PROD"
    ProjectName    = "Veterans Experience Integration Solution"
    VAAzureProgram = "EIS"
    VAECID         = "AZG20181226001"
    #Terraform      = "veis-terraform-logic-apps/devtest"
  }
  viesdevtesttags = {
    CKID           = "195"
    Environment    = "PROD"
    ProjectName    = "Veterans Experience Integration Solution"
    VAAzureProgram = "EIS"
    VAECID         = "AZG20181226001"
    VRMConsumer    = "VEIS"
    VRMEnvironment = "DEVTEST"
    VRMMaintainer  = "VEIS"
    #Terraform      = "veis-terraform-logic-apps/devtest"
  }
  tmpprodtags = {
    CKID                                     = "195"
    Environment                              = "PROD"
    ProjectName                              = "Veterans Experience Integration Solution"
    VAAzureProgram                           = "EIS"
    VAECID                                   = "AZG20181226001"
    VRMConsumer                              = "TMP"
    VRMEnvironment                           = "DEVTEST"
    VRMMaintainer                            = "TMP"  
    #Terraform                                = "veis-terraform-logic-apps/devtest"
  }
  tmpprodnoaitags = {
    CKID           = "195"
    Environment    = "PROD"
    ProjectName    = "Veterans Experience Integration Solution"
    VAAzureProgram = "EIS"
    VAECID         = "AZG20181226001"
    VRMConsumer    = "TMP"
    VRMEnvironment = "DEVTEST"
    VRMMaintainer  = "TMP"
    #Terraform      = "veis-terraform-logic-apps/devtest"
  }
  tmpprodveistags = {
    CKID                                     = "195"
    Environment                              = "PROD"
    ProjectName                              = "Veterans Experience Integration Solution"
    VAAzureProgram                           = "EIS"
    VAECID                                   = "AZG20181226001"
    VRMConsumer                              = "TMP"
    VRMEnvironment                           = "DEVTEST"
    VRMMaintainer                            = "VEIS"
    #Terraform                                = "veis-terraform-logic-apps/devtest"
  }
  tmpprodecaitags = {
    CKID                                     = "195"
    Environment                              = "PROD"
    ProjectName                              = "Veterans Experience Integration Solution"
    VAAzureProgram                           = "EIS"
    VAECID                                   = "AZG20181226001"
    VRMConsumer                              = "TMP"
    VRMEnvironment                           = "DEVTEST"
    VRMMaintainer                            = "TMP"
    #Terraform                                = "veis-terraform-logic-apps/devtest"
  }
}
