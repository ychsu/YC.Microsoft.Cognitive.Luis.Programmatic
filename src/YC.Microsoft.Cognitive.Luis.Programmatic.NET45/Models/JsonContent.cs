using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace YC.Microsoft.Cognitive.Luis.Programmatic.Models
{
    public class JsonContent<T>
        : StringContent
    {
        private const string DefaultMediaType = "application/json";

        public JsonContent(T entity)
            : base (GetContentString(entity))
        {
            var headerValue = new MediaTypeHeaderValue(DefaultMediaType);

            Headers.ContentType = headerValue;
        }

        private static string GetContentString(T entity)
        {
            var str = JsonConvert.SerializeObject(entity);

			Console.WriteLine(str);

			return str;
        }
    }
}
