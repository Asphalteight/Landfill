using Landfill.Common.Helpers;
using System;
using System.Linq;
using System.Windows.Markup;

namespace Landfill.Converters
{
    public class EnumToItemsSourceProvider : MarkupExtension
    {
        public Type EnumType {  get; set; }

        public EnumToItemsSourceProvider(Type type)
        {
            if (type is null || !type.IsEnum)
            {
                throw new Exception();
            }
            EnumType = type;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType).Cast<Enum>().Select(x => x.GetDescription()).ToList();
        }
    }
}
