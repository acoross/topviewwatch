using System.Collections;

namespace tw_server
{
    public abstract class GameObject
    {
        public string name = "";
        protected Game game;
        public Transform transform = new Transform();

        public GameObject(Game game)
        {
            this.game = game;
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
