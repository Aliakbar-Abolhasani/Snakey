using Unity.Mathematics;

namespace Utils
{
    public class GridUtils
    {
        public static int2 GridCoordsToWorldPosition(int2 coords, int2 bounds)
        {
            return coords - bounds / 2;
        }
        
        public static int2 WorldPositionToGridCoords(int2 worldPos, int2 bounds)
        {
            return worldPos + bounds / 2;
        }

        public static bool IsInsideGrid(int2 coords, int2 bounds)
        {
            if (coords.x >= 0
                && coords.y >= 0
                && coords.x < bounds.x
                && coords.y < bounds.y)
            {
                return true;
            }

            return false;
        }

        public static bool IsNextToOrTheSame(int2 pos1, int2 pos2)
        {
            return math.all(pos1 == pos2) || math.abs(pos1.x - pos2.x) == 1 || math.abs(pos1.y - pos2.y) == 1;
        }

        public static int2 GetCoordsInsideGrid(int2 coords, int2 bounds)
        {
            return new int2(Wrap(coords.x, bounds.x + 1), Wrap(coords.y, bounds.y + 1));

            int Wrap(int x, int m)
            {
                return (x % m + m) % m;
            }
        }
    }
}