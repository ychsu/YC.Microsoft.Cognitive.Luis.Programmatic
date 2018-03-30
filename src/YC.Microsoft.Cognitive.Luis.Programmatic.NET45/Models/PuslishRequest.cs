using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using YC.Microsoft.Cognitive.Luis.Programmatic.Enums;

namespace YC.Microsoft.Cognitive.Luis.Programmatic.Models
{
	public class PuslishRequest
	{
		[JsonProperty("versionId")]
		public string VersionId { get; set; }

		[JsonProperty("isStaging")]
		public bool IsStaging { get; set; }

		[JsonProperty("region")]
		public Regions Region { get; set; }
	}
}
