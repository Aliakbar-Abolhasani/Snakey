using Unity.Mathematics;

namespace Utils
{
    public class GridUtils
    {
        public static int2 GetPosition(int2 gridPos, int2 gridBounds)
        {
            var posInsideGrid = gridPos % gridBounds;
            posInsideGrid -= gridBounds / 2;
            return posInsideGrid;
        }

        public static int2 GetInBoundsPosition(int2 gridPos, int2 gridBounds)
        {
            return gridPos % gridBounds;
        }
    }
}