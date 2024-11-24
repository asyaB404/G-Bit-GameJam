using GameTools.MonoTool.Player;

namespace GameTools.MonoTool
{
    public interface ICanTouch
    {
        void StartTouch(PlayerContronal player);
        void EndTouch(PlayerContronal player);
    }
}