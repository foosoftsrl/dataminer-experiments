#pragma warning disable SA1633 // Tabs. Not clear what is happening
#pragma warning disable SA1027 // Tabs. Not clear what is happening
#pragma warning disable SA1600 // Documentation
namespace QAction5Tests
{
    using QAction_5;
    using Skyline.DataMiner.Net.Messages;

    [TestClass]
    public class UnitTestParentalRating
    {

        [TestMethod]
        public void TestParseParentalRating()
        {
            var parentalRatingData = ParentalRatingSource.ParseText(Utils.ReadFile("parental.csv"));

            Assert.IsNotNull(parentalRatingData);
            Assert.AreEqual(58, parentalRatingData.Count);

            Assert.AreEqual(0, parentalRatingData[0].ParentalRating);
            Assert.AreEqual(14, parentalRatingData[1].ParentalRating);
        }

        [TestMethod]
        public void TestParseWhatson()
        {
            var whatsonData = Utils.XmlDeserializeFromFile<Whatson.Pharos>("LT_Schedule_2024-08-22_0030_0602_-_2948.xml").Flatten();
            Assert.AreEqual(312, whatsonData.Count());

            var prCount = whatsonData.FindAll(d => d.ParentalRatingValue != null).Count;
            Assert.AreEqual(16, prCount);

            var pr0Count = whatsonData.FindAll(d => d.ParentalRatingValue == "0").Count;
            Assert.AreEqual(14, pr0Count);

            var pr14Count = whatsonData.FindAll(d => d.ParentalRatingValue == "14").Count;
            Assert.AreEqual(2, pr14Count);
        }

        [TestMethod]
        public void TestParseMediator()
        {
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("LT_Mediator_2024-08-22.json", Mediator.Converter.Settings).Flatten();
            Assert.AreEqual(1000, mediatorData.Count());

            var prCount = mediatorData.FindAll(d => d.ParentalRatingValue != null).Count;
            Assert.AreEqual(42, prCount);

            var pr0Count = mediatorData.FindAll(d => d.ParentalRatingValue == "0").Count;
            Assert.AreEqual(36, pr0Count);

            var pr14Count = mediatorData.FindAll(d => d.ParentalRatingValue == "14").Count;
            Assert.AreEqual(6, pr14Count);
        }

        [TestMethod]
        public void TestParentalRatingProcessorOk()
        {
            var whatsonData = Utils.XmlDeserializeFromFile<Whatson.Pharos>("LT_Schedule_2024-08-22_0030_0602_-_2948.xml").Flatten();
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("LT_Mediator_2024-08-22.json", Mediator.Converter.Settings).Flatten();
            var parentalRatingData = ParentalRatingSource.ParseText(Utils.ReadFile("parental-synt.csv"));

            var result = ParentalRatingProcessor.Compute(whatsonData, mediatorData, parentalRatingData, "testChannel", "testMux");

            Assert.IsNotNull(result);

            foreach (var item in result)
            {
                Assert.AreEqual("ok", item.CheckResult);
            }
        }

        [TestMethod]
        public void TestParentalRatingProcessorFailDelta()
        {
            var whatsonData = Utils.XmlDeserializeFromFile<Whatson.Pharos>("LT_Schedule_2024-08-22_0030_0602_-_2948.xml").Flatten();
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("LT_Mediator_2024-08-22.json", Mediator.Converter.Settings).Flatten();
            var parentalRatingData = ParentalRatingSource.ParseText(Utils.ReadFile("parental-synt-fail-probe.csv"));

            var result = ParentalRatingProcessor.Compute(whatsonData, mediatorData, parentalRatingData, "testChannel", "testMux");

            Assert.IsNotNull(result);

            int i = 0;
            foreach (var item in result)
            {
                if (i++ == 11)
                {
                    Assert.AreEqual("warn - high delta", item.CheckResult);
                }
                else
                {
                    Assert.AreEqual("ok", item.CheckResult);
                }
            }
        }

        [TestMethod]
        public void TestParentalRatingProcessorFailNoProbeData()
        {
            var whatsonData = Utils.XmlDeserializeFromFile<Whatson.Pharos>("PR_last_14_LT_Schedule.xml").Flatten();
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("PR_Last_14_LT_Mediator.json", Mediator.Converter.Settings).Flatten();
            var parentalRatingData = ParentalRatingSource.ParseText(Utils.ReadFile("parental-synt.csv"));

            var result = ParentalRatingProcessor.Compute(whatsonData, mediatorData, parentalRatingData, "testChannel", "testMux");

            Assert.IsNotNull(result);

            int i = 0;
            foreach (var item in result)
            {
                if (i++ == 15)
                {
                    Assert.AreEqual("ko - No probe data", item.CheckResult);
                }
                else
                {
                    Assert.AreEqual("ok", item.CheckResult);
                }
            }
        }

        [TestMethod]
        public void TestForDevelop()
        {
            var whatsonData = Utils.XmlDeserializeFromFile<Whatson.Pharos>("PR_last_14_LT_Schedule.xml").Flatten();
            var mediatorData = Utils.JsonDeserializeFromFile<Mediator.Welcome>("PR_Last_14_LT_Mediator.json", Mediator.Converter.Settings).Flatten();
            var parentalRatingData = ParentalRatingSource.ParseText(Utils.ReadFile("parental-synt.csv"));

            var result = ParentalRatingProcessor.Compute(whatsonData, mediatorData, parentalRatingData, "testChannel", "testMux");

            var matchingRows = result.FindAll(d => d.MediatorData != null);
            int i = 0;
            foreach (var row in result)
            {
                //System.Diagnostics.Debug.WriteLine(row.ElementTime.ToString("yyyy-MM-ddTHH:mm:ssZ") + ";" + (row.MediatorData.ParentalRatingValue == "0" ? string.Empty : "11"));
                System.Diagnostics.Debug.WriteLine(i++);
                System.Diagnostics.Debug.WriteLine("Whatson:  " + row.ElementTime + " / " + row.WhatsonData.ParentalRatingValue);
                System.Diagnostics.Debug.WriteLine("Mediator: " + row.MediatorData?.StartTime + " / " + row.MediatorData?.ParentalRatingValue);
                System.Diagnostics.Debug.WriteLine("Probe:    " + row.ParentalRatingRow?.TimeStamp + " / " + row.ParentalRatingRow?.ParentalRating);
                System.Diagnostics.Debug.WriteLine("Result: " + row.CheckResult + " / " + row.Delta);
                System.Diagnostics.Debug.WriteLine("");
            }
        }
    }
}