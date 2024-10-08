namespace SurvivalChicken.Interfaces
{
    public interface ISaveLoadPersistentData
    {
        public string FileName { get; set; }

        public void Save();

        public void Load();
    }
}
