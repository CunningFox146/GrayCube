namespace GrayCube.Save
{
    public interface ISaver
    {
        public void Save();
        public void Load();
        public GameSave GetGameSave();
    }
}