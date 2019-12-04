namespace MvcApplication.ViewModels
{
    public class SortViewModel
    {
        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            LifeSort = sortOrder == SortState.LifeAsc ? SortState.LifeDesc : SortState.LifeAsc;
            PowerSort = sortOrder == SortState.PowerAsc ? SortState.PowerDesc : SortState.PowerAsc;
            CurrentState = sortOrder;
        }

        public enum SortState
        {
            NameAsc,
            NameDesc,
            LifeAsc,
            LifeDesc,
            PowerAsc,
            PowerDesc,
            IdAsc,
            IdDesc,
        }

        public SortState IdSort { get; private set; }
        public SortState NameSort { get; private set; }
        public SortState LifeSort { get; private set; }
        public SortState PowerSort { get; private set; }
        public SortState CurrentState { get; private set; }
    }
}
