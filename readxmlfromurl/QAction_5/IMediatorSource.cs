using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAction_5
{
    public interface IMediatorSource
    {
        Task<Mediator.Rootobject> ReadMediator(string uri, string channelName, int maxResults);
    }
}
