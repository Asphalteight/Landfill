using Landfill.Common.Enums;
using System;

namespace Landfill.MVVM.Models
{
    public class FilterModel
    {
        public string SearchText { get => _searchText; set { _searchText = value; OnSearchTextChanged(); } }
        public Action OnSearchTextChanged;
        public bool MyProjectsOnly { get => _myProjectsOnly; set { _myProjectsOnly = value; OnMyProjectsOnlyCheck(); } }
        public Action OnMyProjectsOnlyCheck;
        public ProjectStateEnum? ProjectState { get => _projectState; set { _projectState = value; OnProjectStateChange(); } }
        public Action OnProjectStateChange;
        public int? MinPrice { get => _minPrice; set { _minPrice = value; OnMinPriceChange(); } }
        public Action OnMinPriceChange;
        public int? MaxPrice { get => _maxPrice; set { _maxPrice = value; OnMaxPriceChange(); } }
        public Action OnMaxPriceChange;
        public SortEnum Sort { get => _sort; set { _sort = value; OnSortChange(); } }
        public Action OnSortChange;

        private string _searchText;
        private bool _myProjectsOnly;
        private ProjectStateEnum? _projectState;
        private int? _minPrice;
        private int? _maxPrice;
        private SortEnum _sort;
    }
}
