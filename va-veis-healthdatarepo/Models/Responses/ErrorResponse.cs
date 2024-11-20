using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace va_veis_healthdatarepo.Models.Responses
{
    public class ErrorResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
