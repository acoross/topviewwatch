namespace tw_server
{
    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator *(Vector2 lhs, float rhs)
        {
            lhs.x *= rhs;
            lhs.y *= rhs;
            return lhs;
        }

        public static Vector2 operator *(float lhs, Vector2 rhs)
        {
            rhs.x *= lhs;
            rhs.y *= lhs;
            return rhs;
        }

        public static Vector2 zero = new Vector2(0f, 0f);
    }
}
