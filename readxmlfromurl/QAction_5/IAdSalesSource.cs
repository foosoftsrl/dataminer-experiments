using Skyline.DataMiner.Scripting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAction_5
{
    internal interface IAdSalesSource
    {
        AdSales.DataType ReadAdSales(string channelName, string dir);
    }
}
