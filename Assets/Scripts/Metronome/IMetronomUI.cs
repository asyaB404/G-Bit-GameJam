using Metronome.timbre;

namespace Metronome
{
    public interface IMetronomUI
    {
        void AddTimbre(ITimbre timbre);
        void DeleteTimbre(ITimbre timbre);
        
        void  AddCell(ITimbre timbre,MCell cell);
        void RemoveCell(ITimbre timbre,MCell cell);
        public void ReplaceTimbre(ITimbre beforetimbre, ITimbre newtimbre);
    }
}