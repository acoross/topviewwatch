using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace tw_server
{
    class CoroutineBox
    {
        List<IEnumerator> instant_coros = null;
        SortedList<DateTime, IEnumerator> timed_coros = null;

        public void Process(DateTime now)
        {
            List<IEnumerator> old_coros = instant_coros;
            instant_coros = null;
            if (old_coros != null)
            {
                foreach (var coro in old_coros)
                {
                    if (coro.MoveNext())
                    {
                        Add(coro);
                    }
                }
            }

            if (timed_coros != null)
            {
                while (timed_coros.Count > 0)
                {
                    var coro_kv = timed_coros.First();
                    if (coro_kv.Key < now)
                    {
                        timed_coros.RemoveAt(0);

                        var coro = coro_kv.Value;
                        if (coro.MoveNext())
                        {
                            Add(coro);
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void Add(IEnumerator coro)
        {
            var delay = coro.Current as float?;
            if (delay == null)
            {
                if (instant_coros == null)
                    instant_coros = new List<IEnumerator>();

                instant_coros.Add(coro);
            }
            else
            {
                if (timed_coros == null)
                    timed_coros = new SortedList<DateTime, IEnumerator>();

                var time = DateTime.Now.AddSeconds(delay.Value);
                timed_coros.Add(time, coro);
            }
        }
    }
}
