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
            var adSalesData = Utils.XmlDeserializeFromFile<AdSales.DataType>("adsales.xml").flatten();
            var whatsonData = Utils.XmlDeserializeFromFile<Pharos>("whatson.xml").flatten();
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Rootobject>("mediator.json").flatten();
            var merged = Merger.Merge(adSalesData, whatsonData, mediatorData);
            var matchedMediator = merged.Count(s => s.mediatorData != null);
            var matchedWhatson = merged.Count(s => s.whatsonData != null);
            Assert.AreEqual(505, matchedWhatson);
            Assert.AreEqual(175, matchedMediator);
        }
    }
}