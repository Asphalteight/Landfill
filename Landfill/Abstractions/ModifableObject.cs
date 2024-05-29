using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Landfill.Abstractions
{
    public abstract class ModifableObject : ObservableObject
    {
        public void OnModify([CallerMemberName] string name = "")
        {
            var newValue = GetType().GetProperty(name).GetValue(this);

            if (!ChangesHistory.TryGetValue(name, out var history))
            {
                history = new ModifiedObject { OriginalValue = newValue };
                ChangesHistory.Add(name, history);
            }

            history.IsModified = (history.OriginalValue?.ToString() ?? "") != (newValue?.ToString() ?? "");
        }

        private Dictionary<string, ModifiedObject> ChangesHistory { get; set; } = new();
        public bool IsModified => ChangesHistory.Any(x => x.Value.IsModified);
        public bool IsNew { get; set; }

        private record ModifiedObject
        {
            public bool IsModified { get; set; }
            public object OriginalValue { get; set; }
        }
    }
}
