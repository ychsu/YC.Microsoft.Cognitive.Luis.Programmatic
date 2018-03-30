using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace YC.Microsoft.Cognitive.Luis.Programmatic.Models
{
    public class ErrorMessage
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
