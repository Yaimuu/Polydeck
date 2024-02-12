using System.Text;
using System.Text.Json.Serialization;

namespace PolyDeckModel.Model
{
    [Serializable]
    public class DeviceGPIO
    {
        public int Pin { get; set; }
        public ActionPC Action { get; set; }
        public string? LogoPath { get; set; }


        [JsonIgnore]
        public Device? Device { get; set; }

        [JsonIgnore]
        public string? DeviceName
        {
            get 
            { 
                if (Device is not null) 
                { 
                    return Device.Name;
                }
                else
                {
                    return null;
                } 
            }
        }

        
        [JsonIgnore]
        public string ShortcutsString
        {
            get
            {
                bool first = true;
                StringBuilder shortcutsString = new StringBuilder();
                foreach (var shortcuts in Action.Shortcuts)
                {
                    if (first)
                    {
                        shortcutsString.Append(shortcuts.ToString());
                    }
                    else
                    {
                        shortcutsString.Append(String.Format(", {0}", shortcuts.ToString()));
                    }
                    first = false;
                }
                return shortcutsString.ToString();
            } 
            set
            {

            }
        }


        public DeviceGPIO()
        {
            Pin = 0;
            Action = new ActionPC();
            LogoPath = "";
        }

        [JsonConstructor]
        public DeviceGPIO(int pin, ActionPC action, string logoPath)
        {
            Pin = pin;
            Action = action;
            LogoPath = logoPath;
        }
    }
}
