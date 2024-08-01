using CodeBase.Data;

namespace CodeBase.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressWriter
    {
        public void Save(PlayerProgress progress);
    }
}