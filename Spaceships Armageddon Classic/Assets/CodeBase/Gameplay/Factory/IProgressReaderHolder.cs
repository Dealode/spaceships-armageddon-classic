using System.Collections.Generic;
using CodeBase.Infrastructure.Services.PersistentProgress;

namespace CodeBase.Factory
{
    public interface IProgressReaderHolder
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        void Cleanup();
    }
}