using System;

namespace tw_server
{
    class FireObject : GameObject
    {
        float timeAccum = 0f;
        public float speed = 10f;
        public float life = 1f;

        public FireObject(Game game) : base(game)
        {
        }

        public override void OnTick(float deltaTime)
        {
            throw new NotImplementedException();
        }

        public void OnTriggerEnter2D(GameObject collision)
        {

        }
    }
}
