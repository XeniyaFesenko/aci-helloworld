using System.Text.Json.Serialization;
using va_veis_healthdatarepo.Middleware;

namespace va_veis_healthdatarepo.Models.Entities
{
    public class Medication
    {
        public Medication()
        {
            Fills = new List<Fill>();
            Orders = new List<MedOrder>();
            Products = new List<Product>();
            Dosages = new List<Dosages>();
        }

        [JsonPropertyName("dosages")]
        public List<Dosages> Dosages { get; set; }

        [JsonPropertyName("facilityCode")]
        [JsonConverter(typeof(StringConverter))]
        public string FacilityCode { get; set; }

        [JsonPropertyName("facilityName")]
        public string FacilityName { get; set; }

        [JsonPropertyName("fills")]
        public List<Fill> Fills { get; set; }

        [JsonPropertyName("lastFilled")]
        [JsonConverter(typeof(StringConverter))]
        public string LastFilled { get; set; }

        [JsonPropertyName("localId")]
        public string LocalId { get; set; }

        [JsonPropertyName("medStatus")]
        public string MedStatus { get; set; }

        [JsonPropertyName("medStatusName")]
        public string MedStatusName { get; set; }

        [JsonPropertyName("medType")]
        public string MedType { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("orders")]
        public List<MedOrder> Orders { get; set; }

        [JsonPropertyName("overallStart")]
        [JsonConverter(typeof(StringConverter))]
        public string OverallStart { get; set; }

        [JsonPropertyName("overallStop")]
        [JsonConverter(typeof(StringConverter))]
        public string OverallStop { get; set; }

        [JsonPropertyName("patientInstruction")]
        public string PatientInstruction { get; set; }

        [JsonPropertyName("productFormName")]
        public string ProductFormName { get; set; }

        [JsonPropertyName("products")]
        public List<Product> Products { get; set; }

        [JsonPropertyName("qualifiedName")]
        public string QualifiedName { get; set; }

        [JsonPropertyName("sig")]
        public string Sig { get; set; }

        [JsonPropertyName("stopped")]
        [JsonConverter(typeof(StringConverter))]
        public string Stopped { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("uid")]
        public string Uid { get; set; }

        [JsonPropertyName("vaStatus")]
        public string VaStatus { get; set; }

        [JsonPropertyName("vaType")]
        public string VaType { get; set; }
    }
    public class Dosages
    {
        [JsonPropertyName("dose")]
        public string Dose { get; set; }

        [JsonPropertyName("relativeStart")]
        [JsonConverter(typeof(StringConverter))]
        public string RelativeStart { get; set; }

        [JsonPropertyName("relativeStop")]
        [JsonConverter(typeof(StringConverter))]
        public string RelativeStop { get; set; }

        [JsonPropertyName("routeName")]
        public string RouteName { get; set; }

        [JsonPropertyName("scheduleFreq")]
        [JsonConverter(typeof(StringConverter))]
        public string ScheduleFrequency { get; set; }

        [JsonPropertyName("scheduleName")]
        public string ScheduleName { get; set; }

        [JsonPropertyName("scheduleType")]
        public string ScheduleType { get; set; }

        [JsonPropertyName("start")]
        [JsonConverter(typeof(StringConverter))]
        public string Start { get; set; }

        [JsonPropertyName("stop")]
        [JsonConverter(typeof(StringConverter))]
        public string Stop { get; set; }

        [JsonPropertyName("units")]
        public string Units { get; set; }
    }
    public class Fill
    {
        [JsonPropertyName("daysSupplyDispensed")]
        [JsonConverter(typeof(StringConverter))]
        public string DaysSupplyDispensed { get; set; }

        [JsonPropertyName("dispenseDate")]
        [JsonConverter(typeof(StringConverter))]
        public string DispenseDate { get; set; }

        [JsonPropertyName("quantityDispensed")]
        [JsonConverter(typeof(StringConverter))]
        public string QuantityDispensed { get; set; }

        [JsonPropertyName("releaseDate")]
        [JsonConverter(typeof(StringConverter))]
        public string ReleaseDate { get; set; }

        [JsonPropertyName("routing")]
        public string Routing { get; set; }
    }
    public class MedOrder
    {
        [JsonPropertyName("daysSupply")]
        [JsonConverter(typeof(StringConverter))]
        public string DaysSupply { get; set; }

        [JsonPropertyName("fillCost")]
        [JsonConverter(typeof(StringConverter))]
        public string FillCost { get; set; }

        [JsonPropertyName("fillsAllowed")]
        [JsonConverter(typeof(StringConverter))]
        public string FillsAllowed { get; set; }

        [JsonPropertyName("fillsRemaining")]
        [JsonConverter(typeof(StringConverter))]
        public string FillsRemaining { get; set; }

        [JsonPropertyName("orderUid")]
        public string OrderUid { get; set; }

        [JsonPropertyName("ordered")]
        [JsonConverter(typeof(StringConverter))]
        public string Ordered { get; set; }

        [JsonPropertyName("pharmacistName")]
        public string PharmacistName { get; set; }

        [JsonPropertyName("pharmacistUid")]
        public string PharmacistUid { get; set; }

        [JsonPropertyName("predecessor")]
        public string Predecessor { get; set; }

        [JsonPropertyName("prescriptionId")]
        [JsonConverter(typeof(StringConverter))]
        public string PrescriptionId { get; set; }

        [JsonPropertyName("providerName")]
        public string ProviderName { get; set; }

        [JsonPropertyName("providerUid")]
        public string ProviderUid { get; set; }

        [JsonPropertyName("quantityOrdered")]
        [JsonConverter(typeof(StringConverter))]
        public string QuantityOrdered { get; set; }

        [JsonPropertyName("successor")]
        public string Successor { get; set; }

        [JsonPropertyName("vaRouting")]
        public string VaRouting { get; set; }
    }
    public class Product
    {
        [JsonPropertyName("drugClassCode")]
        public string DrugClassCode { get; set; }

        [JsonPropertyName("drugClassName")]
        public string DrugClassName { get; set; }

        [JsonPropertyName("ingredientCode")]
        public string IngredientCode { get; set; }

        [JsonPropertyName("ingredientCodeName")]
        public string IngredientCodeName { get; set; }

        [JsonPropertyName("ingredientName")]
        public string IngredientName { get; set; }

        [JsonPropertyName("ingredientRole")]
        public string IngredientRole { get; set; }

        [JsonPropertyName("strength")]
        public string Strength { get; set; }

        [JsonPropertyName("suppliedCode")]
        public string SuppliedCode { get; set; }

        [JsonPropertyName("suppliedName")]
        public string SuppliedName { get; set; }
    }
}
