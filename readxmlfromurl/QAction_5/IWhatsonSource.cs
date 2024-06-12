using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAction_5
{
    interface IWhatsonSource
    {
        Pharos ReadWhatson(string channelName, string dir);

    }
}
