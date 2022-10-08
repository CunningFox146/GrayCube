namespace GrayCube.SlotGridSystem
{
    public interface IGridTracker
    {
        public void OnRowPopped(int row);
        public void OnColumnPopped(int column);
        public void OnItemPut();
    }
}
