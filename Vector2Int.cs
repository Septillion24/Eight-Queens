public class Vector2Int
    {
        public int x { get; set; }
        public int y { get; set; }
        
        public Vector2Int(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public string toString()
        {
            return "(" + this.x + ", " + this.y +")";
        }

    }