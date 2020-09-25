using System;
using Windows.Devices.Bluetooth.Advertisement;

namespace BLESniffer.WPF.Service
{

    public class BluetoothService
    {
        private BluetoothLEAdvertisementWatcher watcher;

        public BluetoothService()
        {
            watcher = new BluetoothLEAdvertisementWatcher();
            watcher.Received += Watcher_Received;
            watcher.Start();
        }

        private void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
