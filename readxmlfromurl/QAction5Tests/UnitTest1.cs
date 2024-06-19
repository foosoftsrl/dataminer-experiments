//-----------------------------------------------------------------------
// <copyright file="UnitTest1.cs" company="Foosoft SRL">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace QAction5Tests
{
    using Newtonsoft.Json;
    using QAction_5;
    using Skyline.DataMiner.Scripting;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var adSalesData = Utils.XmlDeserializeFromFile<AdSales.DataType>("adsales.xml").Flatten();
            var whatsonData = Utils.XmlDeserializeFromFile<Pharos>("whatson.xml").Flatten();
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("mediator.json", Mediator.Converter.Settings).Flatten();
            var merged = Merger.Merge(adSalesData, whatsonData, mediatorData, new List<EnablerRow>(), new List<EnablerRow>());
            var matchedMediator = merged.Count(s => s.mediatorData != null);
            var matchedWhatson = merged.Count(s => s.whatsonData != null);
            Assert.AreEqual(505, matchedWhatson);
            Assert.AreEqual(175, matchedMediator);
        }

        [TestMethod]
        public void TestMediatorParse()
        {
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("mediator.json", Mediator.Converter.Settings).Flatten();
            var firstLegacy = mediatorData.First(s => s.enablerLegacy != null)?.enablerLegacy;
            Assert.AreEqual("X0119308788", firstLegacy);

            var firstBreakStartUpid = mediatorData.First(s => s.scteBroadcastBreakStart != null)?.scteBroadcastBreakStart;
            Assert.AreEqual("urn:uuid:Break-B0023407280_0004", firstBreakStartUpid);

            var firstAdvStartUpid = mediatorData.First(s => s.scteBroadcastProviderAdvStart != null)?.scteBroadcastProviderAdvStart;
            Assert.AreEqual("urn:uuid:Break-B0023407280_0004-3-13-X0119308788", firstAdvStartUpid);

            var firstReconcileKey = mediatorData.First(s => s.ReconcileKey != null)?.ReconcileKey;
            Assert.AreEqual("0119524219", firstReconcileKey);
        }

        [TestMethod]
        public void TestAdSalesSource()
        {
            var row = new AdSalesSource().ReadAdSales("KI", ".", new DateTime(2024, 6, 17));
            Assert.AreEqual(5, row.Count);
        }
    }
}