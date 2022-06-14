using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using SerpAnalysis.Core.Models;

namespace SerpAnalysis.Test
{
    internal class CommonServiceTest
    {
        internal static List<dynamic> GetRecordsFromCsv(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>().ToList();
                return records;
            }
        }
    }
}
