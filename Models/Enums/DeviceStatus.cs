using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceLibrary.Models.Enums
{
    /// <summary>
    /// Represent the current status of the device.
    /// </summary>
    public enum DeviceStatus
    {
        /// <summary>
        /// The device is connected.
        /// </summary>
        Connected,
        /// <summary>
        /// The device is NOT connected.
        /// </summary>
        Disconnected,
        /// <summary>
        /// The device is currently accepting coins and bills.
        /// </summary>
        Enabled,
        /// <summary>
        /// The device is currently NOT accepting coins or bills.
        /// </summary>
        Disabled,
        /// <summary>
        /// The device is currently dispensing documents.
        /// </summary>
        Dispensing
    }
}
