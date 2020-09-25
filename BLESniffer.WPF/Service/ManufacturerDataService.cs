using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace BLESniffer.WPF.Service
{
    public class ManufacturerDataService
    {
        Dictionary<ulong, string> IdToName = new Dictionary<ulong, string>();

        public ManufacturerDataService()
        {
            if (IdToName.Count is 0)
                Task.Run(async () => 
                {
                    await LoadAsync();
                });
        }

        private async Task<bool> LoadAsync()
        {
            var resourceName = "BLESniffer.WPF.Data.CompanyIdentfiers.csv";
            var filedata = await ReadResource(resourceName);
            var splitByRows = filedata.Split("\n");
            for (int i = 1; i < splitByRows.Length - 1; i++)
            {
                var data = splitByRows[i].Split(",");
                var ID = ushort.Parse(data[0].Replace("\"",""));
                IdToName.Add(ID, data[2]);
            }
            return true;
        }

        public async Task<string> ReadResource(string _resourceFullName)
        {
            if (String.IsNullOrEmpty(_resourceFullName))
                throw new ArgumentNullException();
            Char[] buffer;
            using (var sr = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(_resourceFullName), Encoding.GetEncoding("iso-8859-1"), true))
            {
                buffer = new Char[(int)sr.BaseStream.Length];
                await sr.ReadAsync(buffer, 0, (int)sr.BaseStream.Length);
            }
            var result = new String(buffer);
            return result;
        }

        public string GetManufacturerName(ulong _id)
        {
            if (IdToName.ContainsKey(_id))
                return IdToName[_id];
            return "Unknown";
        }
    }
}
