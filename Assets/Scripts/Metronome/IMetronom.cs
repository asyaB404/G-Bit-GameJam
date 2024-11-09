using Metronome.timbre;

namespace Metronome
{
    public interface IMetronom
    {
        void AddTimbre(IMetronomUI ui,ITimbre timbre);
        void DeleteTimbre(IMetronomUI ui,ITimbre timbre);
        
        void AddCell(IMetronomUI ui);
        void RemoveCell(IMetronomUI ui);
        public void ReplaceTimbre(ITimbre beforetimbre, ITimbre newtimbre);
    }
}