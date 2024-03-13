using LibreHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zPoolMiner;

namespace ComputeDeviceCPU
{
    public class CpuReader
    {
        private static readonly Computer _computer = new Computer { IsCpuEnabled = true };
        /*
        public static CpuTemperatureReader()
        {
            _computer = new Computer { CPUEnabled = true };
            _computer.Open();
        }
        */
        public static int GetTemperaturesInCelsius()
        {
            // _computer = new Computer { CPUEnabled = true };
            int _ret = -1;
            _computer.Open();
            var coreAndTemperature = new Dictionary<string, float>();

            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update(); //use hardware.Name to get CPU model
                foreach (var sensor in hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Temperature && sensor.Value.HasValue)
                    {
                        //  if (sensor.Name == "Package")
                        {
                            _ret = (int)sensor.Value.Value;
                        }
                    }
                }
            }

            return _ret;
        }

        public static int GetPower()
        {
            // _computer = new Computer { CPUEnabled = true };
            int _ret = -1;
            _computer.Open();
            var coreAndTemperature = new Dictionary<string, float>();

            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update(); //use hardware.Name to get CPU model
                foreach (var sensor in hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Power && sensor.Value.HasValue)
                    {
                        //Helpers.ConsolePrint("CPU", sensor.Name + " " + sensor.Value.ToString());
                        if (sensor.Name == "Package")
                        {
                            _ret = (int)sensor.Value.Value;
                        }
                    }
                }
            }

            return _ret;
        }

        public static int GetFan()
        {
            // _computer = new Computer { CPUEnabled = true };
            int _ret = -1;
            _computer.Open();
            var coreAndTemperature = new Dictionary<string, float>();

            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update(); //use hardware.Name to get CPU model
                foreach (var sensor in hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Fan && sensor.Value.HasValue)
                    {
                        //Helpers.ConsolePrint("CPU", sensor.Name + " " + sensor.Value.ToString());
                        // if (sensor.Name == "Package")
                        {
                            _ret = (int)sensor.Value.Value;
                        }
                    }
                }
            }
            return _ret;
        }
        public static int GetLoad()
        {
            // _computer = new Computer { CPUEnabled = true };
            int _ret = -1;
            _computer.Open();
            var coreAndTemperature = new Dictionary<string, float>();

            foreach (var hardware in _computer.Hardware)
            {
                hardware.Update(); //use hardware.Name to get CPU model
                foreach (var sensor in hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Load && sensor.Value.HasValue)
                    {
                        Helpers.ConsolePrint("CPU", sensor.Name + " " + sensor.Value.ToString());
                        // if (sensor.Name == "Package")
                        {
                            _ret = (int)sensor.Value.Value;
                        }
                    }
                }
            }
            return _ret;
        }

        public void Dispose()
        {
            try
            {
                _computer.Close();
            }
            catch (Exception)
            {
                //ignore closing errors
            }
        }
    }

}