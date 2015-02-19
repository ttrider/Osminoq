using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TTRider.Osminoq.Csv;
using TTRider.Osminoq.Settings;

namespace TTRider.Osminoq.CoreTests
{
    [TestClass]
    public class CsvExtractorTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorNullNull()
        {
            var e = new CsvExtractor(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorValNull()
        {
            var e = new CsvExtractor(new MemoryStream(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorNullVal()
        {
            var e = new CsvExtractor(null, new ExtractorSettings());
        }


        [TestMethod]
        public void End2EndPoliceStations()
        {
            using (
                var fl =
                    Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("TTRider.Osminoq.CoreTests.Data.Police_Stations.csv"))
            {

                var settings = new ExtractorSettings() { HasHeaderRecord = true, Delimeter = "," };

                var partition = new ExtractorPartition() { Index = 0 };


                partition.Fields.Add(new DataItemProperty() { Name = "DISTRICT", DataType = "string", Source = "0" });
                partition.Fields.Add(new DataItemProperty() { Name = "ADDRESS", DataType = "string", Source = "1" });
                partition.Fields.Add(new DataItemProperty() { Name = "CITY", DataType = "string", Source = "2" });
                partition.Fields.Add(new DataItemProperty() { Name = "STATE", DataType = "string", Source = "3" });
                partition.Fields.Add(new DataItemProperty() { Name = "ZIP", DataType = "string", Source = "4" });
                partition.Fields.Add(new DataItemProperty() { Name = "WEBSITE", DataType = "string", Source = "5" });
                partition.Fields.Add(new DataItemProperty() { Name = "PHONE", DataType = "string", Source = "6" });
                partition.Fields.Add(new DataItemProperty() { Name = "FAX", DataType = "string", Source = "7" });
                partition.Fields.Add(new DataItemProperty() { Name = "TTY", DataType = "string", Source = "8" });
                partition.Fields.Add(new DataItemProperty() { Name = "LOCATION", DataType = "string", Source = "9" });

                settings.Partitions.Add(partition);

                using (var e = new CsvExtractor(fl, settings))
                {

                    var item = e.ExtractDataItem();
                    while (item != null)
                    {



                        item = e.ExtractDataItem();
                    }
                }
            }
        }
    }
}
