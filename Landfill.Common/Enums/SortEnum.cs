using System.ComponentModel;

namespace Landfill.Common.Enums
{
    public enum SortEnum
    {
        [Description("Сначала новые")]
        CreatedDesc,

        [Description("Сначала старые")]
        CreatedAsc,

        [Description("По убыванию стоимости")]
        PriceDesc,

        [Description("По возрастанию стоимости")]
        PriceAsc
    }
}
