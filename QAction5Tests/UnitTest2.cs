#pragma warning disable SA1633 // Tabs. Not clear what is happening
#pragma warning disable SA1027 // Tabs. Not clear what is happening
#pragma warning disable SA1600 // Documentation
namespace QAction5Tests
{
    using QAction_5;
    using Skyline.DataMiner.Net.Messages;

    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestTaCheckOk()
        {
            var adSalesData = Utils.XmlDeserializeFromFile<AdSales.DataType>("Test1_LB_AdSales.xml").Flatten();
            Assert.AreEqual(12, adSalesData.Count());
            var whatsonData = Utils.XmlDeserializeFromFile<Whatson.Pharos>("Test1_LB_WON.xml").Flatten();
            Assert.AreEqual(15, whatsonData.Count());
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("Test1_LB_mediator.json", Mediator.Converter.Settings).Flatten();
            Assert.AreEqual(15, mediatorData.Count());
            var legacyData = EnablerSource.ParseText(Utils.ReadFile("Test1_LB_legacy.csv"));
            Assert.AreEqual(6, legacyData.Count());
            var scteData = EnablerSource.ParseText(Utils.ReadFile("Test1_LB_scte.csv"));
            Assert.AreEqual(4, scteData.Count());

            var merged = TaCheckProcessor.Compute(adSalesData, whatsonData, mediatorData, scteData, legacyData, "TE", "TestMux");
            Assert.AreEqual(2, merged.Count());

            var errors = merged.Count(e => e.Result != 0);
            Assert.AreEqual(0, errors);
        }

        [TestMethod]
        public void TestTaCheckNoScte1()
        {
            var adSalesData = Utils.XmlDeserializeFromFile<AdSales.DataType>("Test1_LB_AdSales.xml").Flatten();
            Assert.AreEqual(12, adSalesData.Count());
            var whatsonData = Utils.XmlDeserializeFromFile<Whatson.Pharos>("Test1_LB_WON.xml").Flatten();
            Assert.AreEqual(15, whatsonData.Count());
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("Test1_LB_mediator.json", Mediator.Converter.Settings).Flatten();
            Assert.AreEqual(15, mediatorData.Count());
            var legacyData = EnablerSource.ParseText(Utils.ReadFile("Test1_LB_legacy.csv"));
            Assert.AreEqual(6, legacyData.Count());
            var scteData = EnablerSource.ParseText(Utils.ReadFile("Test2_LB_scte.csv")); // Removed super_load and adv_start entries
            Assert.AreEqual(2, scteData.Count());

            var merged = TaCheckProcessor.Compute(adSalesData, whatsonData, mediatorData, scteData, legacyData, "TE", "TestMux");
            Assert.AreEqual(2, merged.Count());

            var errors = merged.Count(e => e.Result != 0);
            Assert.AreEqual(1, errors);

            var errorEntry = merged.First(e => e.Result != 0);
            Assert.AreEqual("missing scte adv start\n", errorEntry.Message);
        }

        [TestMethod]
        public void TestTaCheckNoScte2()
        {
            var adSalesData = Utils.XmlDeserializeFromFile<AdSales.DataType>("Test1_LB_AdSales.xml").Flatten();
            Assert.AreEqual(12, adSalesData.Count());
            var whatsonData = Utils.XmlDeserializeFromFile<Whatson.Pharos>("Test1_LB_WON.xml").Flatten();
            Assert.AreEqual(15, whatsonData.Count());
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("Test1_LB_mediator.json", Mediator.Converter.Settings).Flatten();
            Assert.AreEqual(15, mediatorData.Count());
            var legacyData = EnablerSource.ParseText(Utils.ReadFile("Test1_LB_legacy.csv"));
            Assert.AreEqual(6, legacyData.Count());
            var scteData = EnablerSource.ParseText(Utils.ReadFile("Test3_LB_scte.csv")); // Changed code
            Assert.AreEqual(2, scteData.Count());

            var merged = TaCheckProcessor.Compute(adSalesData, whatsonData, mediatorData, scteData, legacyData, "TE", "TestMux");
            Assert.AreEqual(2, merged.Count());

            var errors = merged.Count(e => e.Result != 0);
            Assert.AreEqual(2, errors);

            var errorEntry = merged[0];
            Assert.AreEqual("missing scte overlay placement start\n", errorEntry.Message);

            errorEntry = merged[1];
            Assert.AreEqual("missing scte adv start\n", errorEntry.Message);
        }

        [TestMethod]
        public void TestParsingWonWithPreviousBlockNameEmpty()
        {
            var whatsonData = Utils.XmlDeserializeFromFile<Whatson.Pharos>("B6_Schedule_2024-09-30_0007_0535_-_3002.xml").Flatten();
            Assert.AreEqual(778, whatsonData.Count());
        }
    }
}