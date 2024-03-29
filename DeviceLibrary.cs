﻿using DeviceLibrary.Models;
using DeviceLibrary.Models.Enums;
using LibreR.Controllers;
using System;
using System.Linq;
using System.Threading;
namespace DeviceLibrary
{
    public class DeviceLibrary
    {
        private readonly object _events = new object();
        private readonly object _functions = new object();
        private readonly Logger _log = new Logger("DeviceLibraryLogs", "\r\nhttp://www.lyfingenieria.com\r\nDiseño e integracion de tecnolgias electronicas y de computo.\r\ne-mail: benny@lopez-fernandez.com\r\nCONTACTO:\r\nOfc: 6622603825\r\nCel: 6623000946");
        private DeviceStatus _Status;
        /// <summary>
        /// Represent the current status of the device.
        /// </summary>
        public DeviceStatus Status {
            get {
                lock (_events)
                {
                    return _Status;
                }
            }
            private set {
                lock (_events)
                {                  
                    _Status = value;
                    _log.Message(_Status, "STATUS-CHANGED");
                }
            }
        }
        private event Action<Document> _AcceptedDocument;

        /// <summary>
        /// Triggered when a bill or coin is inserted into the kiosk.
        /// </summary>
        public event Action<Document> AcceptedDocument {
            add
            {
                lock (_events)
                {
                    _log.Message("", "ACCEPTED-EVENT-ADD");
                    _AcceptedDocument += value;
                }
            }
            remove
            {
                lock (_events)
                {
                    _log.Message("", "ACCEPTED-EVENT-REMOVE");
                    _AcceptedDocument -= value;
                }
            }
        }
        public DeviceLibrary()
        {
            string netInterface = (System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().Select(x => x.GetPhysicalAddress().ToString()).FirstOrDefault());
            _log.EnableSecureCopy($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\ol-GS", "sys", Security.ToMd5("LYF" + (netInterface ?? string.Empty)));
        }
        /// <summary>
        /// Simulates connection to the device, usually called when the application starts.
        /// </summary>
        public void Open()
        {
            _log.Message("", "OPEN");
            lock (_functions)
            {
                if (Status == DeviceStatus.Disconnected)
                {
                    Status = DeviceStatus.Connected;
                }
            }
        }
        /// <summary>
        /// Ends connection to the device, usually called when the application stops.
        /// </summary>
        public void Close()
        {
            _log.Message("", "CLOSE");
            lock (_functions)
            {
                if (Status == DeviceStatus.Connected)
                {
                    Status = DeviceStatus.Disconnected;
                }
            }
        }
        /// <summary>
        /// Lets the device accept inserted coins/bills.
        /// </summary>
        public void Enable()
        {
            _log.Message("", "ENABLE");
            lock (_functions)
            {
                if (Status == DeviceStatus.Connected || Status == DeviceStatus.Disabled)
                {
                    Status = DeviceStatus.Enabled;
                }
            }
        }
        /// <summary>
        /// Prevents the device to accept inserted coins/bills.
        /// </summary>
        public void Disable()
        {
            _log.Message("", "DISABLE");
            lock (_functions)
            {
                if (Status == DeviceStatus.Connected || Status == DeviceStatus.Enabled)
                {
                    Status = DeviceStatus.Disabled;
                }
            }
        }
        /// <summary>
        /// Dispenses the requested amount to the user.
        /// </summary>
        /// <param name="amount">The amount that the device should dispense to the user.</param>
        /// <returns>The amount that could not be dispensed to the user.</returns>
        public decimal Dispense(decimal amount)
        {
            lock (_functions)
            {
                if (Status == DeviceStatus.Connected || Status == DeviceStatus.Disabled)
                {
                    Status = DeviceStatus.Dispensing;
                    Thread.Sleep(2000);
                    _log.Message($"Dispensed: {amount.ToString("c")}, Requested: {amount.ToString("c")}, Remaing: {0.ToString("c")}", "DISPENSE");
                    return 0;
                }
                else
                {
                    _log.Message($"Dispensed: {0.ToString("c")}, Requested: {amount.ToString("c")}, Remaing: {amount.ToString("c")}", "DISPENSE");
                    return amount;
                }
            }
        }
        /// <summary>
        /// Simulates a bill or coin being inserted into the kiosk. This method will be called in the payment view.
        /// </summary>
        /// <param name="document">The bill or coin inserted into the kiosk.</param>
        public void SimulateInsertion(Document document)
        {
            lock (_functions)
            {
                _log.Message(document.ToString(), "DOCUMENT-ACCEPTED");
                _AcceptedDocument?.Invoke(document);
            }
        }
    }
}