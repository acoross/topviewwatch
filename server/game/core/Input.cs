using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tw_server
{
    public static class Input
    {
        static ConcurrentBag<string> buttons = new ConcurrentBag<string>();

        public static bool GetButton(string name)
        {
            return buttons.TryPeek(out name);
        }

        public static void SetButton(string name)
        {
            if (!buttons.TryPeek(out name))
                buttons.Add(name);
        }

        public static void Clear()
        {
            buttons = new ConcurrentBag<string>();
        }
    }
}
