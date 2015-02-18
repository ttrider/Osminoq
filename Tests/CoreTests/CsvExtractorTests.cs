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


                partition.Fields.Add(new DataItemProperty() { Name = "DISTRICT", DataType = "string", Input = "0" });
                partition.Fields.Add(new DataItemProperty() { Name = "ADDRESS", DataType = "string", Input = "1" });
                partition.Fields.Add(new DataItemProperty() { Name = "CITY", DataType = "string", Input = "2" });
                partition.Fields.Add(new DataItemProperty() { Name = "STATE", DataType = "string", Input = "3" });
                partition.Fields.Add(new DataItemProperty() { Name = "ZIP", DataType = "string", Input = "4" });
                partition.Fields.Add(new DataItemProperty() { Name = "WEBSITE", DataType = "string", Input = "5" });
                partition.Fields.Add(new DataItemProperty() { Name = "PHONE", DataType = "string", Input = "6" });
                partition.Fields.Add(new DataItemProperty() { Name = "FAX", DataType = "string", Input = "7" });
                partition.Fields.Add(new DataItemProperty() { Name = "TTY", DataType = "string", Input = "8" });
                partition.Fields.Add(new DataItemProperty() { Name = "LOCATION", DataType = "string", Input = "9" });

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
