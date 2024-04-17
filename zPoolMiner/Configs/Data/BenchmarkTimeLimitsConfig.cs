using System;
using zPoolMiner.Enums;

namespace zPoolMiner.Configs.Data
{
    /// <summary>
    /// BenchmarkTimeLimitsConfig is used to set the time limits for benchmarking.
    /// There are three types: Quick, Standard,Precise (look at BenchmarkType.cs).
    /// </summary>
    ///
    [Serializable]
    public class BenchmarkTimeLimitsConfig
    {
        #region CONSTANTS

        [field: NonSerialized]
        private static readonly int[] DEFAULT_CPU_NVIDIA = { 30, 60, 120 };

        [field: NonSerialized]
        private static readonly int[] DEFAULT_AMD = { 300, 360, 420 };

        [field: NonSerialized]
        public static readonly int SIZE = 3;

        #endregion CONSTANTS

        #region PRIVATES

        private int[] _benchmarkTimeLimitsCPU = MemoryHelper.DeepClone(DEFAULT_CPU_NVIDIA);
        private int[] _benchmarkTimeLimitsNVIDIA = MemoryHelper.DeepClone(DEFAULT_CPU_NVIDIA);
        private int[] _benchmarkTimeLimitsAMD = MemoryHelper.DeepClone(DEFAULT_AMD);

        private bool IsValid(int[] value)
        {
            return value != null && value.Length == SIZE;
        }

        #endregion PRIVATES

        #region PROPERTIES

        public int[] CPU
        {
            get { return _benchmarkTimeLimitsCPU; }
            set
            {
                if (IsValid(value))
                {
                    _benchmarkTimeLimitsCPU = MemoryHelper.DeepClone(value);
                }
                else
                {
                    _benchmarkTimeLimitsCPU = MemoryHelper.DeepClone(DEFAULT_CPU_NVIDIA);
                }
            }
        }

        public int[] NVIDIA
        {
            get { return _benchmarkTimeLimitsNVIDIA; }
            set
            {
                if (IsValid(value))
                {
                    _benchmarkTimeLimitsNVIDIA = MemoryHelper.DeepClone(value);
                }
                else
                {
                    _benchmarkTimeLimitsNVIDIA = MemoryHelper.DeepClone(DEFAULT_CPU_NVIDIA);
                }
            }
        }

        public int[] AMD
        {
            get { return _benchmarkTimeLimitsAMD; }
            set
            {
                if (IsValid(value))
                {
                    _benchmarkTimeLimitsAMD = MemoryHelper.DeepClone(value);
                }
                else
                {
                    _benchmarkTimeLimitsAMD = MemoryHelper.DeepClone(DEFAULT_AMD);
                }
            }
        }

        #endregion PROPERTIES

        public int GetBenchamrktime(BenchmarkPerformanceType benchmarkPerformanceType, DeviceGroupType deviceGroupType)
        {
            if (deviceGroupType == DeviceGroupType.CPU)
            {
                return CPU[(int)benchmarkPerformanceType];
            }
            if (deviceGroupType == DeviceGroupType.AMD_OpenCL)
            {
                return AMD[(int)benchmarkPerformanceType];
            }

            return NVIDIA[(int)benchmarkPerformanceType];
        }
    }
}