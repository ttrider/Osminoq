using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TTRider.Osminoq.TypeHandlers;

namespace TTRider.Osminoq.CoreTests
{
    [TestClass]
    public class TypeHandlerTests
    {
        [TestMethod]
        public void StringConverterTest00()
        {
            const string val = "$123.43";
            var pattern = new Regex(@"\$(?<value>.*)", RegexOptions.Compiled);
            Assert.AreEqual("123.43", StringTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, pattern)));
        }

        [TestMethod]
        public void StringConverterTest01()
        {
            const string val = "123.43";
            var pattern = new Regex(@"\$(?<value>.*)", RegexOptions.Compiled);
            Assert.AreEqual("123.43", StringTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, pattern)));
        }

        [TestMethod]
        public void StringConverterTest02()
        {
            const string val = "$123.43";
            var pattern = new Regex(@"\$(?<valuew>.*)", RegexOptions.Compiled);
            Assert.AreEqual("$123.43", StringTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, pattern)));
        }

        [TestMethod]
        public void StringConverterTest03()
        {
            const string val = "123.43";
            Assert.AreEqual("123.43", StringTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, null)));
        }


        [TestMethod]
        public void IntConverterTest00()
        {
            const string val = "$123";
            var pattern = new Regex(@"\$(?<value>.*)", RegexOptions.Compiled);
            Assert.AreEqual(123, IntegerTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, pattern)));
        }

        [TestMethod]
        public void IntConverterTest01()
        {
            const string val = "123.43";
            var pattern = new Regex(@"(?<value>[0-9]*)", RegexOptions.Compiled);
            Assert.AreEqual(123, IntegerTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, pattern)));
        }

        [TestMethod]
        public void IntConverterTest02()
        {
            const string val = "123";
            Assert.AreEqual(123, IntegerTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, null)));
        }

        [TestMethod]
        public void IntConverterTest03()
        {
            const string val = "foo123";
            Assert.IsNull(IntegerTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, null)));
        }


        [TestMethod]
        public void BoolConverterTest00()
        {
            const string val = "true";
            Assert.AreEqual(true,BooleanTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, null)));
        }
        [TestMethod]
        public void BoolConverterTest01()
        {
            const string val = "false";
            Assert.AreEqual(false, BooleanTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, null)));
        }
        [TestMethod]
        public void BoolConverterTest02()
        {
            const string val = "1";
            Assert.AreEqual(true, BooleanTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, null)));
        }
        [TestMethod]
        public void BoolConverterTest03()
        {
            const string val = "0";
            Assert.AreEqual(false, BooleanTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, null)));
        }
        [TestMethod]
        public void BoolConverterTest04()
        {
            const string val = "yes";
            Assert.AreEqual(true, BooleanTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, null)));
        }
        [TestMethod]
        public void BoolConverterTest05()
        {
            const string val = "no";
            Assert.AreEqual(false, BooleanTypeHandler.Convert(DataItemUtilities.ProcessPattern(val, null)));
        }

        [TestMethod]
        public void DoubleConverterTest00()
        {
            const string val = "123.123";
            Assert.AreEqual(123.123, DoubleTypeHandler.Convert(val));
        }

        [TestMethod]
        public void DoubleConverterTest01()
        {
            const string val = "foo123.123";
            Assert.IsNull(DoubleTypeHandler.Convert(val));
        }

        [TestMethod]
        public void GuidConverterTest00()
        {
            const string val = "{7B6CAE43-3C2C-4D0C-914E-EA7D1E9190B3}";
            Assert.AreEqual(new Guid("{7B6CAE43-3C2C-4D0C-914E-EA7D1E9190B3}"), GuidTypeHandler.Convert(val));
        }
        [TestMethod]
        public void GuidConverterTest01()
        {
            const string val = "7B6CAE43-3C2C-4D0C-914E-EA7D1E9190B3";
            Assert.AreEqual(new Guid("{7B6CAE43-3C2C-4D0C-914E-EA7D1E9190B3}"), GuidTypeHandler.Convert(val));
        }
        [TestMethod]
        public void GuidConverterTest02()
        {
            const string val = "7B6CAE433C2C4D0C914EEA7D1E9190B3";
            Assert.AreEqual(new Guid("{7B6CAE43-3C2C-4D0C-914E-EA7D1E9190B3}"), GuidTypeHandler.Convert(val));
        }
        [TestMethod]
        public void GuidConverterTest03()
        {
            const string val = "foo123.123";
            Assert.IsNull(GuidTypeHandler.Convert(val));
        }

    }
}
