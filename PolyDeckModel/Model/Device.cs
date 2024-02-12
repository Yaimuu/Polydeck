using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PolyDeckModel.Model
{
    [Serializable]
    public class Device
    {

        public string Name { get; set; }
        public string MACAddress { get; set; }

        private List<DeviceGPIO> _deviceGPIOs;

        public List<DeviceGPIO> DeviceGPIOs { 
            get
            {
                return _deviceGPIOs;
            } 
            set
            {
                _deviceGPIOs = value;
                foreach (var gpio in _deviceGPIOs)
                {
                    gpio.Device = this;
                }
            }

        }


        public Device()
        {
            Name = "default";
            MACAddress = "MACdefault";
            DeviceGPIOs = new List<DeviceGPIO>();
        }

        [JsonConstructor]
        public Device(string name, string macAddress, List<DeviceGPIO> deviceGPIOs)
        {
            Name = name;
            MACAddress = macAddress;
            _deviceGPIOs = deviceGPIOs;
        }


        public void addDeviceGPIO(DeviceGPIO newDevice)
        {
            newDevice.Device = this;
            DeviceGPIOs.Add(newDevice);
        }


        public void deleteDeviceGPIO(DeviceGPIO device)
        {
            DeviceGPIOs.Remove(device);
        }

        public DeviceGPIO? getGPIO(int pin)
        {
            DeviceGPIO? result = null;
            foreach (DeviceGPIO gpio in DeviceGPIOs)
            {
                if (gpio.Pin == pin)
                {
                    result = gpio;
                    break;
                }
            }

            return result;
        }
    }
}
