using Skyline.DataMiner.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAction_5
{
    public static class SLProtocolExtensions
    {
        public static string channelName(this SLProtocolExt protocol)
        {
            var channelName = protocol.GetParameter(Parameter.channelname);
            if (!(channelName is string))
            {
                throw new Exception("Channel is not defined");
            }
            return (string)channelName;

        }
    }
}
