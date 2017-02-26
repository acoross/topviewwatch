using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tw_server
{
    class Board
    {
        ConcurrentBag<PlayerObject> players = new ConcurrentBag<PlayerObject>();

        void AddPlayer(PlayerObject pobj)
        {
            players.Add(pobj);
        }

        bool RemovePlayer(PlayerObject pobj)
        {
            return players.TryTake(out pobj);
        }
    }
}
