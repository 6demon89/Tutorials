using BLESniffer.WPF.Service;
using System;
using System.Collections.Generic;
using System.Security.Policy;
using System.Text;

namespace BLESniffer.WPF.Model
{
    public class BLEModel
    {
        readonly ManufacturerDataService manufacturerDataService;

        public BLEModel()
        {
            manufacturerDataService = App.AppContainer.GetService(typeof(ManufacturerDataService)) as ManufacturerDataService;
        }

        public ulong ID { get; set; }
        public string Manufacturer 
        {
            get
            {
                return manufacturerDataService.GetManufacturerName(Company);
            } 
        }
        public ushort Company { get; set; }
        public byte[] Data { get; set; }

        public string DataDisplay
        {
            get
            {
                if (Data.Length == 0) return "No Data";
                return BitConverter.ToString(Data);
            }
        }
    }
}
