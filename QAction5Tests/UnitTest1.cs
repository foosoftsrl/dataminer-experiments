#pragma warning disable SA1633 // Tabs. Not clear what is happening
#pragma warning disable SA1027 // Tabs. Not clear what is happening
namespace QAction5Tests
{
    using QAction_5;
    using Skyline.DataMiner.Net.Messages;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestPalline()
        {
            var adSalesData = Utils.XmlDeserializeFromFile<AdSales.DataType>("adsales.xml").Flatten();
            var whatsonData = Utils.XmlDeserializeFromFile<Whatson.Pharos>("whatson.xml").Flatten();
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("mediator.json", Mediator.Converter.Settings).Flatten();
            var legacyData = EnablerSource.ParseText(Utils.ReadFile("legacy.csv"));
            var scteData = EnablerSource.ParseText(Utils.ReadFile("scte.csv"));
            var merged = Palline.Compute(adSalesData, whatsonData, mediatorData, scteData, legacyData, "Pippo", "PippoMux");
            var matchedMediator = merged.Count(s => s.MediatorData != null);
            var matchedWhatson = merged.Count(s => s.WhatsonData != null);
            Assert.AreEqual(9, matchedWhatson);
            Assert.AreEqual(2, matchedMediator);
            var firstLegacy = whatsonData.First(s => s.EnablerLegacy != null);
            var firstLegacyOnMerged = merged.First(s => s.WhatsonData == firstLegacy);
            Assert.IsNotNull(firstLegacyOnMerged.LegacyEventLoad);
            Assert.IsNotNull(firstLegacyOnMerged.LegacyEventStart);
            Assert.IsNotNull(firstLegacyOnMerged.LegacyEventStop);

            var firstBreakStartUpid = whatsonData.First(s => s.ScteBroadcastBreakStart != null);
            var firstBreakStartUpidOnMerged = merged.First(s => s.WhatsonData == firstBreakStartUpid);
            Assert.IsNotNull(firstBreakStartUpidOnMerged.ScteBroadcastBreakStart);

            var firstAdvStartUpid = whatsonData.First(s => s.ScteBroadcastProviderAdvStart != null);
            var firstAdvStartUpidOnMerged = merged.First(s => s.WhatsonData == firstAdvStartUpid);
            Assert.IsNotNull(firstAdvStartUpidOnMerged.ScteBroadcastProviderAdvStart);
        }

        [TestMethod]
        public void TestTACheckWithPush()
        {
            var adSalesData = Utils.XmlDeserializeFromFile<AdSales.DataType>("AdSales_LB_20240718_20240719002821.xml").Flatten();
            var whatsonData = Utils.XmlDeserializeFromFile<Whatson.Pharos>("Whatson_LB_Schedule_2024-07-18_0021_0600_-_2959.xml").Flatten();
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("mediator.json", Mediator.Converter.Settings).Flatten();
            var legacyData = EnablerSource.ParseText(Utils.ReadFile("legacy.csv"));
            var scteData = EnablerSource.ParseText(Utils.ReadFile("scte.csv"));
            var taCheckRows = Palline.Compute(adSalesData, whatsonData, mediatorData, scteData, legacyData, "Pippo", "PippoMux");
            var matchedMediator = taCheckRows.Count(s => s.MediatorData != null);
            var matchedWhatson = taCheckRows.Count(s => s.WhatsonData != null);
            Assert.AreEqual(36, taCheckRows.Length);
            Assert.AreEqual(18, taCheckRows.Count(s => s.AdSalesData.TimeAllocationType == "PUSH"));
            Assert.AreEqual(3, taCheckRows.Count(s => s.AdSalesData.Enabler == "E"));
            Assert.AreEqual(15, taCheckRows.Count(s => s.AdSalesData.Enabler == "X"));
            Assert.AreEqual(36, matchedWhatson);
            Assert.AreEqual(0, matchedMediator);

            var firstSubstitution = taCheckRows.First(s => s.AdSalesData.Enabler == "X");
            Assert.AreEqual(firstSubstitution.AdSalesData.ReconcileKey, "0119996378");
            Assert.AreEqual(firstSubstitution.WhatsonData.ReconcileKey, "0119996378");
            Assert.AreEqual(firstSubstitution.WhatsonData.EnablerLegacy, "X0119996378");
            Assert.AreEqual("urn:uuid:Break-B0023476544_0005", firstSubstitution.WhatsonData.ScteBroadcastBreakStart);
            Assert.AreEqual("urn:uuid:Break-B0023476544_0005-3-10-X0119996378", firstSubstitution.WhatsonData.ScteBroadcastProviderAdvStart);

            var firstEnhancement = taCheckRows.First(s => s.AdSalesData.Enabler == "E");
            Assert.AreEqual(firstEnhancement.AdSalesData.ReconcileKey, "0119932622");
            Assert.AreEqual(firstEnhancement.WhatsonData.ReconcileKey, "0119932622");
            Assert.AreEqual(firstEnhancement.WhatsonData.EnablerLegacy, "0119932622");
            Assert.AreEqual("urn:uuid:0119932622", firstEnhancement.WhatsonData.ScteBroadcastProviderOverlayPlacementStart);
            Assert.AreEqual("urn:uuid:0119932622", firstEnhancement.WhatsonData.ScteBroadcastProviderOverlayPlacementEnd);

            var firstPush = taCheckRows.First(s => s.AdSalesData.TimeAllocationType == "PUSH");
            Assert.AreEqual(firstPush.AdSalesData.ReconcileKey, string.Empty);
            Assert.AreEqual("P0061063068", firstPush.AdSalesData.BreakId);
            Assert.AreEqual("P0061063068", firstPush.WhatsonData.EnablerLegacy);
            Assert.AreEqual("urn:uuid:P0061063068", firstPush.WhatsonData.ScteBroadcastProviderOverlayPlacementStart);
            Assert.AreEqual("urn:uuid:P0061063068", firstPush.WhatsonData.ScteBroadcastProviderOverlayPlacementEnd);
        }

        [TestMethod]
        public void TestAdSalesWhatsonDiff()
        {
            var adSalesData = Utils.XmlDeserializeFromFile<AdSales.DataType>("adsales.xml").Flatten();
            var whatsonData = Utils.XmlDeserializeFromFile<Whatson.Pharos>("whatson.xml").Flatten();
            var diff = XPrint.ComputeAdSalesWhatsonDiff(adSalesData, whatsonData!.FilterSpots());
            Assert.IsNotNull(diff);
            var countOk = diff.FindAll(e => e.Item3 == "ok").Count();
            Assert.AreEqual(482, countOk);
        }

        [TestMethod]
        public void TestWhatsonMediatorDiff()
        {
            var whatsonData = Utils.XmlDeserializeFromFile<Whatson.Pharos>("whatson.xml").Flatten();
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("mediator.json", Mediator.Converter.Settings).Flatten();
            var diff = XPrint.ComputeWhatsonMediatorDiff(whatsonData!.FilterSpots(), mediatorData!.FilterSpots());
            Assert.IsNotNull(diff);
            var countOk = diff.FindAll(e => e.Item3 == "ok").Count();
            Assert.AreEqual(178, countOk);
        }

        [TestMethod]
        public void TestParseMediator()
        {
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("mediator.json", Mediator.Converter.Settings).Flatten();
            var firstLegacy = mediatorData.First(s => s.EnablerLegacy != null)?.EnablerLegacy;
            Assert.AreEqual("X0119308788", firstLegacy);

            var firstBreakStartUpid = mediatorData.First(s => s.ScteBroadcastBreakStart != null)?.ScteBroadcastBreakStart;
            Assert.AreEqual("urn:uuid:Break-B0023407280_0004", firstBreakStartUpid);

            var firstAdvStartUpid = mediatorData.First(s => s.ScteBroadcastProviderAdvStart != null)?.ScteBroadcastProviderAdvStart;
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