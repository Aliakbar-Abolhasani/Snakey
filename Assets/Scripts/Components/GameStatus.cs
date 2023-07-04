using Unity.Entities;

namespace Snakey.Components
{
    public enum GameResult
    {
        Undetermined,
        Won,
        Lost
    }

    public struct GameStatus : IComponentData
    {
        public GameResult Result;
    }
}