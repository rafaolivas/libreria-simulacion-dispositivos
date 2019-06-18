using DeviceLibrary.Models;
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
                    _log.Message($"DeviceStatus Status Change : [{_Status}].");
                }
            }
        }
        private event Action<Document> _AcceptedDocument;
        public event Action<Document> AcceptedDocument {
            add
            {
                lock (_events)
                {
                    _log.Message($"AcceptedDocument Event Add.");
                    _AcceptedDocument += value;
                }
            }
            remove
            {
                lock (_events)
                {
                    _log.Message($"AcceptedDocument Event Remove.");
                    _AcceptedDocument -= value;
                }
            }
        }
        public DeviceLibrary()
        {
            string netInterface = (System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces().Select(x => x.GetPhysicalAddress().ToString()).FirstOrDefault());
            _log.EnableSecureCopy($"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\ol-GS", "sys", Security.ToMd5("LYF" + (netInterface ?? string.Empty)));
        }
        public void Open()
        {
            _log.Message($"Open : [{_Status}].");
            lock (_functions)
            {
                if (Status == DeviceStatus.Disconnected)
                {
                    Status = DeviceStatus.Connected;
                }
                _log.Message($"Open : [{_Status}].");
            }
        }
        public void Close()
        {
            _log.Message($"Close : [{_Status}].");
            lock (_functions)
            {
                if (Status == DeviceStatus.Connected)
                {
                    Status = DeviceStatus.Disconnected;
                }
                _log.Message($"Close : [{_Status}].");
            }
        }
        public void Enable()
        {
            _log.Message($"Enable : [{_Status}].");
            lock (_functions)
            {
                if (Status == DeviceStatus.Connected || Status == DeviceStatus.Disabled)
                {
                    Status = DeviceStatus.Enabled;
                }
                _log.Message($"Enable : [{_Status}].");
            }
        }
        public void Disable()
        {
            _log.Message($"Disable : [{_Status}].");
            lock (_functions)
            {
                if (Status == DeviceStatus.Connected || Status == DeviceStatus.Enabled)
                {
                    Status = DeviceStatus.Disabled;
                }
                _log.Message($"Disable : [{_Status}].");
            }
        }
        public decimal Dispense(decimal amount)
        {
            _log.Message($"Dispense amount : [{amount}] : [{_Status}].");
            lock (_functions)
            {
                if (Status == DeviceStatus.Connected || Status == DeviceStatus.Disabled)
                {
                    Status = DeviceStatus.Dispensing;
                    Thread.Sleep(2000);
                    _log.Message($"Dispense remaining : [0] : [{_Status}].");
                    return 0;
                }
                else
                {
                    _log.Message($"Dispense remaining : [{amount}] : [{_Status}].");
                    return amount;
                }
            }
        }
    }
}