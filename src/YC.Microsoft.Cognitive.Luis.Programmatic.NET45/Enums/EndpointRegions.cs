using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace YC.Microsoft.Cognitive.Luis.Programmatic.Enums
{
    public enum EndpointRegions
    {
        WestUs,
        WestEurope,
        AustraliaEast
    }

	[JsonConverter(typeof(StringEnumConverter))]
	public enum Regions
	{
		WestUs,
		WestEurope,
		AustraliaEast,
		SoutheastAsia
	}
}
