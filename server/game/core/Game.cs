using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace tw_server
{
    public class Game
    {
        public float deltaTime
        {
            get;
            private set;
        }

        DateTime last = DateTime.MinValue;

        void updateTime()
        {
            var now = DateTime.Now;
            if (last == DateTime.MinValue)
                last = now;

            deltaTime = (float)(now - last).TotalSeconds;

            last = now;
        }

        BufferBlock<Action> invoker = new BufferBlock<Action>();
        ConcurrentDictionary<string, GameObject> objects = new ConcurrentDictionary<string, GameObject>();
        ConcurrentDictionary<string, CoroutineBox> coroutines = new ConcurrentDictionary<string, CoroutineBox>();

        public Task Invoke(Action job)
        {
            return invoker.SendAsync(job);
        }

        public void Register(GameObject obj)
        {
            objects.TryAdd(obj.name, obj);
            coroutines.TryAdd(obj.name, new CoroutineBox());
        }

        public void Remove(GameObject obj)
        {
            GameObject outVal1;
            objects.TryRemove(obj.name, out outVal1);

            CoroutineBox outVal2;
            coroutines.TryRemove(obj.name, out outVal2);
        }
        
        public void AddCoroutine(string name, IEnumerator coro)
        {
            CoroutineBox box;
            if (coroutines.TryGetValue(name, out box))
            {
                box.Add(coro);  // box 가 있을 때만 add 가능
            }
        }

        public async Task MainLoop()
        {
            while (true)
            {
                updateTime();

                IList<Action> jobs;
                if (invoker.TryReceiveAll(out jobs))
                {
                    foreach (var job in jobs)
                    {
                        job.Invoke();
                    }
                }

                foreach (var obj in objects.Values)
                {
                    obj.OnTick(1f);
                }

                var now = DateTime.Now;
                // 코루틴 실행, timed coroutine 의 경우 해당 컨테이너에 넣기
                foreach (var coroBox in coroutines.Values)
                {
                    coroBox.Process(now);
                }

                Input.Clear();

                await Task.Delay(33);
            }
        }
    }
}
