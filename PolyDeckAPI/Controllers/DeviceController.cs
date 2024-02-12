using Abp.IO.Extensions;
using Abp.MimeTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PolyDeckModel;
using PolyDeckModel.Model;
using System.Text.Json;
using System.Web.Http.Description;

namespace PolyDeckAPI.Controllers
{
    [ApiController]
    [Route("device")]
    public class DeviceController : ControllerBase
    {
        private readonly ILogger<DeviceController> _logger;

        public DeviceController(ILogger<DeviceController> logger)
        {
            _logger = logger;
        }

        [HttpGet("logo/{macAdress}/{targetGPIO}", Name = "GetLogo")]
        public IActionResult GetLogo(String macAdress, int targetGPIO)
        {
            _logger.LogInformation("Get Logo");
            // Receive Device and GPIO infos then Check if the device and GPIO are stored

            // If yes, return the content associated, else, do nothing or return blank content
            Device device = this.findDeviceById(macAdress);
            DeviceGPIO gpio = device.getGPIO(targetGPIO);
            
            try
            {
                string filename = SerializeHelper.DEFAULT_IMAGE;
                var image = System.IO.File.OpenRead(filename);
                if (gpio.LogoPath != null && gpio.LogoPath != "")
                {
                    filename = gpio.LogoPath;
                    image = System.IO.File.OpenRead(filename);
                    
                    /*HttpResponseMessage res = CreateResponse(HttpStatusCode.OK);
                    res.Content = new StreamContent(System.IO.File.OpenRead(gpio.LogoPath));*/
                }
                MimeTypeMap mime = new MimeTypeMap();
                string mimeType = mime.GetMimeType(image.Name);
                _logger.LogDebug(image.Name);
                _logger.LogDebug(mimeType);
                // _logger.LogInformation(BitConverter.ToString(image.GetAllBytes()));
                return Ok(image.GetAllBytes());
                // return File(image, mimeType);
            }
            catch (FileNotFoundException)
            {
                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [ResponseType(typeof(List<Device>))]
        [HttpGet("")]
        public IActionResult GetAllDevices()
        {
            _logger.LogInformation("Get All Devices");

            return Ok(this.deserializeAll());
        }

        [HttpGet("{macAdress}", Name = "GetDevice")]
        [ResponseType(typeof(Device))]
        public IActionResult GetDevice(String macAdress)
        {
            _logger.LogInformation("Get Device");
            String deviceJsonFile = SerializeHelper.DEVICES_STORAGE_FILE;
            // Receive Device and GPIO infos then Check if the device and GPIO are stored
            Device deviceResult = null;
            if (!System.IO.File.Exists(deviceJsonFile))
                return new NotFoundResult();

            // If yes, return the content associated, else, do nothing or return blank content
            List<Device> devices = this.deserializeAll();

            foreach(Device device in devices)
            {
                if(device.MACAddress.Equals(macAdress))
                {
                    deviceResult = device;
                    break;
                }
            }

            if(deviceResult == null)
                return new NotFoundResult();

            return Ok(deviceResult);
        }

        [HttpPost("action/{macAdress}/{targetGPIO}", Name = "TriggerAction")]
        public IActionResult TriggerAction(String macAdress, int targetGPIO)
        {
            _logger.LogInformation("Trigger Action");
            // Receive Device and GPIO infos then Check if the device and GPIO are stored

            // If yes, execute the associated action
            Device device = this.findDeviceById(macAdress);

            if(device == null) return new NotFoundResult();

            DeviceGPIO gpio =  device.getGPIO(targetGPIO);
            if (gpio == null)
                return NotFound("GPIO " + targetGPIO + " does not exists");

            gpio.Action.Execute();

            return CreatedAtAction("TriggerAction", new { macAdress = device.MACAddress, gpio = gpio});
        }

        [HttpPost("init", Name = "InitDevice")]
        public IActionResult InitDevice(Device device)
        {
            _logger.LogInformation("Init Device");
            // Receive Device and GPIO infos then Check if the device and GPIO are stored

            // If not, store the device
            String deviceJsonFile = SerializeHelper.DEVICES_STORAGE_FILE;
            if (!System.IO.File.Exists(deviceJsonFile))
            {
                using (FileStream fs = (System.IO.File.Create(deviceJsonFile)));
                List<Device> initDevices = new List<Device>();
                initDevices.Add(device);
                this.serializeAll(initDevices);

                return CreatedAtAction(nameof(device), new { macAdress = device.MACAddress });
            }

            if(findDeviceById(device.MACAddress) != null)
                return Ok(device.MACAddress + " already exists !");

            List<Device> allDevices = this.deserializeAll();
            allDevices.Add(device);
            this.serializeAll(allDevices);

            return CreatedAtAction("InitDevice", new {macAdress = device.MACAddress});
        }
         

        [HttpPut("update", Name = "Update")]
        public IActionResult UpdateDevices (List<Device> devices) 
        {
            this.serializeAll(devices);
            return Ok();
        }


        [HttpPut("{macAdress}/{targetGPIO}", Name = "ConfigureAction")]
        public IActionResult ConfigureAction(String macAdress, int targetGPIO, Action newAction)
        {
            _logger.LogInformation("Configure Action");

            Device device = this.findDeviceById(macAdress);

            if (device == null) return NotFound("Device " + macAdress + " does not exists");

            DeviceGPIO gpio = device.getGPIO(targetGPIO);
            if (gpio == null)
                return NotFound("GPIO " + targetGPIO + " does not exists");

            // TODO: Store new actions for the selected GPIO

            return NoContent();
        }

        [HttpPut("{macAdress}", Name = "AddGPIO")]
        public IActionResult AddGPIO(String macAdress, DeviceGPIO newGPIO)
        {
            _logger.LogInformation("Add GPIO");

            Device device = this.findDeviceById(macAdress);

            if (device == null) return new NotFoundResult();

            // TODO: Add GPIO to device file
            DeviceGPIO gpio = device.getGPIO(newGPIO.Pin);
            if (gpio != null)
                return BadRequest("GPIO " + newGPIO.Pin + " already exists");



            return NoContent();
        }

        [HttpDelete("{macAdress}", Name = "DeleteDevice")]
        public IActionResult DeleteDevice(String macAdress)
        {
            _logger.LogInformation("Delete Device");

            Device device = this.findDeviceById(macAdress);

            if (device == null) return new NotFoundResult();

            // TODO: Delete Device

            return NoContent();
        }

        private List<Device> deserializeAll()
        {
            String deviceJsonFile = SerializeHelper.DEVICES_STORAGE_FILE;

            List<Device> devices;
            using (StreamReader r = new StreamReader(deviceJsonFile))
            {
                string json = r.ReadToEnd();
                devices = JsonSerializer.Deserialize<List<Device>>(json);
            }

            return devices;
        }

        private void serializeAll(List<Device> devices)
        {
            String deviceJsonFile = SerializeHelper.DEVICES_STORAGE_FILE;
            if (!System.IO.File.Exists(deviceJsonFile))
            {
                using (FileStream fs = (System.IO.File.Create(deviceJsonFile))) ;
            }

            String jsonDevice = JsonSerializer.Serialize(devices);
            System.IO.File.WriteAllText(deviceJsonFile, jsonDevice);
        }

        private Device? findDeviceById(String macAdress)
        {
            Device? result = null;
            List<Device> allDevices = this.deserializeAll();

            foreach(Device device in allDevices)
            {
                if(device.MACAddress.Equals(macAdress))
                {
                    result = device;
                    break;
                }
            }

            return result;
        }
    }
}
