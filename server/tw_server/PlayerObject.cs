using System.Collections;
using System.Threading.Tasks;

using C = System.Console;

namespace tw_server
{
    class PlayerObject : GameObject
    {   
        bool moving = false;

        float moveAccumTime = 0f;
        Vector2 movingDirection = Vector2.zero;
        TWSession session = null;

        public PlayerObject(Game game, TWSession session, string name) : base(game)
        {
            this.session = session;
            this.name = name;
        }

        public override void OnTick(float deltaTime)
        {

        }
        
        public IEnumerator Dash(Vector2 orgPos, Vector2 direction, float speed, float actionTime, int effect_count)
        {
            if (moving)
                yield break;

            moving = true;
            movingDirection = direction;

            int count = 0;
            while (count < effect_count)
            {
                transform.Translate(movingDirection * speed * deltaTime, Space.World);
                C.WriteLine($"dash: {transform.position.x}, {transform.position.y}");

                moveAccumTime += deltaTime;
                if (moveAccumTime > actionTime)
                {
                    moveAccumTime -= actionTime;
                    count++;

                    var pos = new Acoross.tw.Rpc.vec2();
                    pos.x = transform.position.x;
                    pos.y = transform.position.y;
                    session.NotiPosSync(pos);
                }

                yield return null;
            }

            {
                var pos = new Acoross.tw.Rpc.vec2();
                pos.x = transform.position.x;
                pos.y = transform.position.y;
                session.NotiDashEnd(pos);
            }

            moveAccumTime = 0f;
            moving = false;
        }
    }
}
