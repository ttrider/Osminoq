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

                var settings = new ExtractorSettings() {HasHeaderRecord = true, Delimeter = ","};

                settings.Partitions.Add(new ExtractorPartition(){Index = 0});

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
