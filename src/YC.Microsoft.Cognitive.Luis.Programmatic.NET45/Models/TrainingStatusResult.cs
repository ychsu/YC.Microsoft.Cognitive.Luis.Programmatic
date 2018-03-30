using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using YC.Microsoft.Cognitive.Luis.Programmatic.Enums;

namespace YC.Microsoft.Cognitive.Luis.Programmatic.Models
{
    public class TrainingStatusResult
    {
        public string ModelId { get; set; }

        public TrainingStatusDetail Details { get; set; }
    }

    public class TrainingStatusDetail
    {
        public int StatusId { get; set; }

        public TrainStatus Status { get; set; }

        public int ExampleCount { get; set; }

        public DateTimeOffset? TrainingDateTime { get; set; }
    }
}
