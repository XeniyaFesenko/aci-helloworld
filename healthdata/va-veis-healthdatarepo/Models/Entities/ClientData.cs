using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class ClientNames
    {
        [JsonProperty("$id")]
        public string Id { get; set; }

        public string Name { get; set; }

        [JsonPropertyName("values")]
        public List<string> Values { get; set; }
    }
    public class Customviews
    {
        [JsonPropertyName("exists")]
        public bool Exists { get; set; }

        [JsonPropertyName("customviewdetail")]
        public List<CustomViewDetail> CustomViewDetail { get; set; }
    }
    public class CustomViewDetail
    {
        [JsonPropertyName("minapiversion")]
        public float MinApiVersion { get; set; }

        [JsonPropertyName("maxapiversion")]
        public float MaxApiVersion { get; set; }

        [JsonPropertyName("requiredversion")]
        public float? RequiredVersion { get; set; }

        [JsonPropertyName("prefixname")]
        public string PrefixName { get; set; }

        [JsonPropertyName("assocclientgrp")]
        public ClientNames AssocClientGrp { get; set; }
    }

    public class ControllerConfigs
    {
        public string Name { get; set; }

        [JsonPropertyName("validclientgrp")]
        public ClientNames ValidClientGrp { get; set; }

        [JsonPropertyName("customviews")]
        public Customviews CustomViews { get; set; }
    }

    public class ClientData
    {
        [JsonPropertyName("clientnames")]
        public List<ClientNames> ClientNames { get; set; }

        [JsonPropertyName("controllers")]
        public List<ControllerConfigs> Controllers { get; set; }
    }
}
