using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

namespace PolyDeckModel.Model
{
    [Serializable]
    public class ActionPC
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
        public List<KeyCode> Shortcuts { get; set; }


        [JsonConstructor]
        public ActionPC()
        {
            Shortcuts = new List<KeyCode>();
        }

        public void Execute()
        {
            foreach (var shortcut in Shortcuts)
            {
                keybd_event((byte)shortcut, 0, KEYEVENTF_EXTENDEDKEY, 0);
                keybd_event((byte)shortcut, 0, KEYEVENTF_KEYUP, 0);
            }
        }

        public void AddShortcut(KeyCode shortcut)
        {
            Shortcuts.Add(shortcut);
        }


    }
}
