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
            var e = new CsvDataItemReader(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorValNull()
        {
            var e = new CsvDataItemReader(new MemoryStream(), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CtorNullVal()
        {
            var e = new CsvDataItemReader(null, new DataPackageModel());
        }


        [TestMethod]
        public void End2EndPoliceStations()
        {
            using (
                var fl =
                    Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("TTRider.Osminoq.CoreTests.Data.Police_Stations.csv"))
            {

                var dataPackageModel = new DataPackageModel() { HasHeaderRecord = true, Delimeter = "," };

                var dataSetModel = new DataSetModel();


                dataSetModel.Properties.Add(new DataItemProperty() { Name = "District", DataType = "string", Source = "0" });
                dataSetModel.Properties.Add(new DataItemProperty() { Name = "Address", DataType = "string", Source = "1" });
                dataSetModel.Properties.Add(new DataItemProperty() { Name = "City", DataType = "string", Source = "2" });
                dataSetModel.Properties.Add(new DataItemProperty() { Name = "State", DataType = "string", Source = "3" });
                dataSetModel.Properties.Add(new DataItemProperty() { Name = "Zip", DataType = "string", Source = "4" });
                dataSetModel.Properties.Add(new DataItemProperty() { Name = "Website", DataType = "string", Source = "5" });
                dataSetModel.Properties.Add(new DataItemProperty() { Name = "Phone", DataType = "string", Source = "6" });
                dataSetModel.Properties.Add(new DataItemProperty() { Name = "Fax", DataType = "string", Source = "7" });
                dataSetModel.Properties.Add(new DataItemProperty() { Name = "TTY", DataType = "string", Source = "8" });
                dataSetModel.Properties.Add(new DataItemProperty() { Name = "Location", DataType = "string", Source = "9" });

                dataSetModel.Properties.Add(new DataItemProperty() { Name = "Latitude", DataType = "double", Source = "9", Template = @"\((?'value'[\-0-9\.]+),"});
                dataSetModel.Properties.Add(new DataItemProperty() { Name = "Longditude", DataType = "double", Source = "9", Template = @",\s*(?'value'[\-0-9\.]+)\)"});



                dataPackageModel.DataSetModels.Add(dataSetModel);

                using (var e = new CsvDataItemReader(fl, dataPackageModel))
                {
                    do
                    {
                        foreach (var item in e.ReadDataItems())
                        {
                            Console.WriteLine(item.GetPropertyValue("Phone"));
                        }
                        
                    } while (e.NextDataSet());
                }
            }
        }

        [TestMethod]
        public void End2EndMaleNames()
        {
            using (
                var fl =
                    Assembly.GetExecutingAssembly()
                        .GetManifestResourceStream("TTRider.Osminoq.CoreTests.Data.malenames.txt"))
            {

                var dataPackageModel = new DataPackageModel() { HasHeaderRecord = false, Delimeter = "," };

                var dataSetModel = new DataSetModel();


                dataSetModel.Properties.Add(new DataItemProperty() { Name = "Name", DataType = "string", Source = "0" });

                dataPackageModel.DataSetModels.Add(dataSetModel);

                using (var e = new CsvDataItemReader(fl, dataPackageModel))
                {
                    do
                    {
                        foreach (var item in e.ReadDataItems())
                        {
                            Console.WriteLine(item.GetPropertyValue("Name"));
                        }

                    } while (e.NextDataSet());
                }
            }
        }
    }
}
