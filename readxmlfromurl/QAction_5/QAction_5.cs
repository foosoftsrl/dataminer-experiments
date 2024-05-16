using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Skyline.DataMiner.Scripting;

/// <summary>
/// DataMiner QAction Class.
/// </summary>
public static class QAction
{
    /// <summary>
    /// The QAction entry point.
    /// </summary>
    /// <param name="protocol">Link with SLProtocol process.</param>
    public static void Run(SLProtocolExt protocol)
    {
        try
        {
            protocol.Mergediterationcounter = (double)protocol.Mergediterationcounter + 1;
            List<object[]> instances = new List<object[]>();
            /*var data1 = protocol.adsales.GetRow(0);
            var data2 = protocol.mediator.GetRow(0);
            var data1_0 = data1[0];
            var data1_1 = data1[1];
            var data2_0 = data2[0];
            var data2_1 = data2[1];
            instances.Add(new MergedtableQActionRow
            {
                Mergedtablecolumn1 = data1_0,
                Mergedtablecolumn2 = data1_1,
            }.ToObjectArray());
            instances.Add(new MergedtableQActionRow
            {
                Mergedtablecolumn1 = data2_0,
                Mergedtablecolumn2 = data2_1,
            }.ToObjectArray());*/

            for (int i = 0; i < protocol.adsales.RowCount; i++)
            {
                var data1 = protocol.adsales.GetRow(i);
                instances.Add(new MergedtableQActionRow
                {
                    Mergedtablecolumn1 = data1[0],
                    Mergedtablecolumn2 = data1[1],
                }.ToObjectArray());
            }

            // Ottieni tutte le righe dalla tabella mediator
            for (int i = 0; i < protocol.mediator.RowCount; i++)
            {
                var data2 = protocol.mediator.GetRow(i);
                instances.Add(new MergedtableQActionRow
                {
                    Mergedtablecolumn1 = data2[0],
                    Mergedtablecolumn2 = data2[1],
                }.ToObjectArray());
            }

            protocol.FillArray(Parameter.Mergedtable.tablePid, instances, NotifyProtocol.SaveOption.Full);
            protocol.Mergeddebugmsg = $"Processed {instances.Count} rows";
        }
        catch (Exception ex)
        {
            protocol.Log($"QA{protocol.QActionID}|{protocol.GetTriggerParameter()}|Run|Exception thrown:{Environment.NewLine}{ex}", LogType.Error, LogLevel.NoLogging);
        }
    }
}
