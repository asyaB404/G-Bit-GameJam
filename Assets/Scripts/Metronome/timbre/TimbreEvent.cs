namespace Metronome.timbre
{
    public enum TimbreEvent
    {
        //每一个该音色的鼓点开始或者结束播放
        BegainHit,
        AfterHit,
        
        //开始和结束循环
        BeginLoop,
        EndLoop,
        
        //开始和结束播放
        BeginPlay,
        EndPlay,
    }
}