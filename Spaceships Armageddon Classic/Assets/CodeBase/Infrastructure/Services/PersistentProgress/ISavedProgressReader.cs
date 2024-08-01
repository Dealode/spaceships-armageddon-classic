namespace CodeBase.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        public void Read(IPersistentProgressService progress);
    }
}