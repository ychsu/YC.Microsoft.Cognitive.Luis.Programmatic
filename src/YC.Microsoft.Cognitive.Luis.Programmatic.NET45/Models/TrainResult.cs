using System;
using System.Collections.Generic;
using System.Text;
using YC.Microsoft.Cognitive.Luis.Programmatic.Enums;

namespace YC.Microsoft.Cognitive.Luis.Programmatic.Models
{
    public class TrainResult
    {
        public int StatusId { get; set; }

        public TrainStatus Status { get; set; }

        public ErrorMessage Error { get; set; }
    }
}
