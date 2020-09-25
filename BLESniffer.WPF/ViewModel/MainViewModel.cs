using BLESniffer.WPF.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Threading;
using Windows.Devices.Bluetooth.Advertisement;
using Windows.Storage.Streams;

namespace BLESniffer.WPF.ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<BLEModel> bag { get; set; } = new ObservableCollection<BLEModel>();

        private BluetoothLEAdvertisementWatcher watcher;

        public MainViewModel()
        {
            watcher = new BluetoothLEAdvertisementWatcher();
            watcher.Received += Watcher_Received;
            watcher.Start();
        }

        private void Watcher_Received(BluetoothLEAdvertisementWatcher sender, BluetoothLEAdvertisementReceivedEventArgs args)
        {
            var manufacturerDataCollection = args.Advertisement.ManufacturerData.ToList();
            foreach (var item in manufacturerDataCollection)
            {
                var result = new BLEModel();
                result.Company = item.CompanyId;
                result.Data = ReadBuffer(item.Data);
                result.ID = args.BluetoothAddress;

                Application.Current.Dispatcher.Invoke(() =>
                {
                    bag.Add(result);
                },DispatcherPriority.Normal);
            }
        }

        public byte[] ReadBuffer(IBuffer theBuffer)
        {
            byte[] result = new byte[theBuffer.Length];
            using (var dataReader = DataReader.FromBuffer(theBuffer))
            {
                dataReader.ReadBytes(result);
            }
            return result;
        }
    }
}
