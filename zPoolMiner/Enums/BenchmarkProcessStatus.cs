using System;
using System.Collections.Generic;
using System.Text;

namespace zPoolMiner.Enums {
    public enum BenchmarkProcessStatus {
        NONE,
        Idle,
        Running,
        Killing,
        DoneKilling,
        Finished,
        Success
    }
}
