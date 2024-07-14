using Unity.Entities;

namespace Snakey.Components
{
    public enum GameState
    {
        WaitingForInput = 0,
        Running = 1,
        Won = 2,
        Lost = 3
    }

    public struct GameStatus : IComponentData
    {
        public GameState State;
    }
}