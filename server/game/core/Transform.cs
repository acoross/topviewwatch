using System;

namespace tw_server
{
    public class Transform
    {
        public Vector2 position = Vector2.zero;

        public void Translate(Vector2 translation, Space relativeTo = Space.World)
        {
            if (relativeTo == Space.World)
            {
                Console.WriteLine($"tran: tran({translation.x}, {translation.y})");

                Console.WriteLine($"tran: org({position.x}, {position.y})");

                position = new Vector2(position.x + translation.x, position.y + translation.y);

                Console.WriteLine($"tran: new({position.x}, {position.y})");
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
