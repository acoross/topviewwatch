using System.Collections;

namespace tw_server
{
    class PlayerObject : GameObject
    {   
        bool firing = false;
        bool moving = false;

        float moveAccumTime = 0f;
        Vector2 movingDirection = Vector2.zero;

        public PlayerObject(Game game) : base(game)
        {
        }

        public override void OnTick(float deltaTime)
        {
            if (Input.GetButton("left shift"))
            {
                StartCoroutine("Dash", 0.1f, Vector2.zero, new Vector2(1f, 0f), 1f, 0.1f, 5);
            }
        }
        
        public IEnumerator Dash(float deltaTime, Vector2 orgPos, Vector2 direction, float speed, float actionTime, int effect_count)
        {
            if (moving)
                yield break;

            moving = true;

            movingDirection = direction;

            float limit = actionTime * effect_count;
            while (moveAccumTime < limit)
            {
                moveAccumTime += Time.deltaTime;
                transform.Translate(movingDirection * speed * Time.deltaTime, Space.World);
                yield return null;
            }

            moveAccumTime = 0f;
            moving = false;
        }
    }
}
