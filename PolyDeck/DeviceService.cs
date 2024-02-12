using PolyDeckModel.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PolyDeck
{
    internal sealed class DeviceService
    {
        private static HttpClient _client;

        private static DeviceService _instance;

        public DeviceService()
        {
            _client = new HttpClient();
        }

        internal static DeviceService Instance 
        { 
            get
            {
                if (_instance == null)
                {
                    _instance = new DeviceService();
                }
                return _instance;
            }
        }

        public async Task<ObservableCollection<DeviceGPIO>> GetDevicesGPIOsAsync()
        {
            List<Device> devices = new List<Device>();
            ObservableCollection<DeviceGPIO> devicesGPIO = new ObservableCollection<DeviceGPIO>();    
            string url = "https://localhost:7224/device";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                devices = await response.Content.ReadFromJsonAsync<List<Device>>();
            }
            foreach (Device device in devices)
            {
                foreach (DeviceGPIO deviceGPIO in device.DeviceGPIOs)
                {
                    deviceGPIO.Device = device;
                    devicesGPIO.Add(deviceGPIO);
                }
            }
            return devicesGPIO;
        }

        public async Task UpdateDevicesAsync(List<Device> devices)
        {
            string url = "https://localhost:7224/device/update";
            await _client.PutAsJsonAsync<List<Device>>(url, devices);
        }


        public async Task<int> CountDevices()
        {
            List<Device> devices = new List<Device>();
            string url = "https://localhost:7224/device";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                devices = await response.Content.ReadFromJsonAsync<List<Device>>();
            }
            return devices.Count();
        }


        public async Task<int> CountDevicesGPIO()
        {
            List<Device> devices = new List<Device>();
            int nbGPIO = 0;
            string url = "https://localhost:7224/device";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                devices = await response.Content.ReadFromJsonAsync<List<Device>>();
            }
            foreach (Device device in devices)
            {
                nbGPIO += device.DeviceGPIOs.Count();
            }
            return nbGPIO;
        }
    }
}
