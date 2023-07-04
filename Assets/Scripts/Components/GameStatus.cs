using Unity.Entities;

namespace Snakey.Components
{
    public struct GameStatus : IComponentData
    {
        public bool IsGameLost;
    }
}