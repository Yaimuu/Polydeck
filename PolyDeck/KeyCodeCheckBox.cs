using PolyDeckModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolyDeck
{
    internal class KeyCodeCheckBox
    {
        public KeyCodeCheckBox(KeyCode keyCode)
        {
            KeyCode = keyCode;
            IsChecked = false;
        }

        public KeyCodeCheckBox(KeyCode keyCode, bool isChecked) : this(keyCode)
        {
            IsChecked = isChecked;
        }

        public KeyCode KeyCode { get; set; }

        public bool IsChecked { get; set; }



    }
}
