﻿using System;
using System.Collections.Generic;
using System.Text;

namespace zPoolMiner.Interfaces
{
    public interface IBenchmarkComunicator
    {

        void OnBenchmarkComplete(bool success, string status);
    }
}
