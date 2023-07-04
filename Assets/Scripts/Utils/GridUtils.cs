using Unity.Mathematics;

namespace Utils
{
    public class GridUtils
    {
        public static int2 GridPosToPosition(int2 gridPos, int2 gridBounds)
        {
            return gridPos - gridBounds / 2;
        }

        public static bool IsInsideGrid(int2 gridPos, int2 gridBounds)
        {
            if (gridPos.x >= 0
                && gridPos.y >= 0
                && gridPos.x < gridBounds.x
                && gridPos.y < gridBounds.y)
            {
                return true;
            }

            return false;
        }

        public static bool IsNextToOrTheSame(int2 pos1, int2 pos2)
        {
            return math.all(pos1 == pos2) || math.abs(pos1.x - pos2.x) == 1 || math.abs(pos1.y - pos2.y) == 1;
        }

        public static int2 GetCoordsInsideGrid(int2 gridPos, int2 gridBounds)
        {
            return gridPos % gridBounds;
        }
    }
}