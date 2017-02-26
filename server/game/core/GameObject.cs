using System;
using System.Collections;
using System.Threading.Tasks;

namespace tw_server
{
    public abstract class GameObject
    {
        public string name = "";
        Game game;
        public Transform transform = new Transform();

        protected float deltaTime
        {
            get
            {
                return game.deltaTime;
            }
        }


        public GameObject(Game game)
        {
            this.game = game;
        }
        
        public GameObjectT Instantiate<GameObjectT>(Func<Game, GameObjectT> maker)
            where GameObjectT : GameObject
        {
            return game.Instantiate<GameObjectT>(maker);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deltaTime"> seconds </param>
        public abstract void OnTick(float deltaTime);

        public void StartCoroutine(string method, params object[] objs)
        {
            var methodInfo = this.GetType().GetMethod(method);
            var ret = methodInfo.Invoke(this, objs);
            var coro = ret as IEnumerator;
            if (coro != null)
            {
                game.AddCoroutine(name, coro);
            }
        }

        public void Destroy(GameObject obj)
        {
            if (obj != null)
                game.Remove(obj);
        }
    }
}
