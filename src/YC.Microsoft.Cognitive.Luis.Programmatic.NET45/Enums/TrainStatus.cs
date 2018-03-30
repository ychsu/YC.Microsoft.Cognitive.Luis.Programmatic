using System;
using System.Collections.Generic;
using System.Text;

namespace YC.Microsoft.Cognitive.Luis.Programmatic.Enums
{
    public enum TrainStatus
    {
        Success = 0,
        Fail = 1,
        UpToDate = 2,
        InProgress = 3,
		Queued = 9
    }
}
