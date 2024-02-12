using PolyDeckModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using PolyDeck.Core;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PolyDeck
{
    class DevicesViewModel: ViewModelBase
    {
        public ICollection<DeviceGPIO> DevicesGPIO { get; set; }

    }
}
