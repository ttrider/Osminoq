using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TTRider.Osminoq.CoreTests
{
    [TestClass]
    public class CsvTests
    {
        [TestMethod]
        public void StringConverterTest00()
        {
            const string val = "$123.43";
            var pattern = new Regex(@"\$(?<value>.*)", RegexOptions.Compiled);
            Assert.AreEqual("123.43", FieldExtractor.ToString(val, pattern));
        }

        [TestMethod]
        public void StringConverterTest01()
        {
            const string val = "123.43";
            var pattern = new Regex(@"\$(?<value>.*)", RegexOptions.Compiled);
            Assert.IsNull(FieldExtractor.ToString(val, pattern));
        }

        [TestMethod]
        public void StringConverterTest02()
        {
            const string val = "$123.43";
            var pattern = new Regex(@"\$(?<valuew>.*)", RegexOptions.Compiled);
            Assert.IsNull(FieldExtractor.ToString(val, pattern));
        }

        [TestMethod]
        public void StringConverterTest03()
        {
            const string val = "123.43";
            Assert.AreEqual("123.43", FieldExtractor.ToString(val, null));
        }


        [TestMethod]
        public void IntConverterTest00()
        {
            const string val = "$123";
            var pattern = new Regex(@"\$(?<value>.*)", RegexOptions.Compiled);
            Assert.AreEqual(123, FieldExtractor.ToInt(val, pattern));
        }

        [TestMethod]
        public void IntConverterTest01()
        {
            const string val = "123.43";
            var pattern = new Regex(@"\$(?<value>.*)", RegexOptions.Compiled);
            Assert.IsNull(FieldExtractor.ToInt(val, pattern));
        }

        [TestMethod]
        public void IntConverterTest02()
        {
            const string val = "123";
            Assert.AreEqual(123, FieldExtractor.ToInt(val, null));
        }
    }
}
