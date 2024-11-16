using Microsoft.Maui.Handlers;
using ValidationControl.Controls;

namespace ValidationControl.Handler
{
    public partial class MEntryHandler : EntryHandler
    {
        public MEntryHandler()
        {
            ViewMapper.Add(nameof(MEntry.NoUnderline), MapNoUnderline);
        }
    }
}
