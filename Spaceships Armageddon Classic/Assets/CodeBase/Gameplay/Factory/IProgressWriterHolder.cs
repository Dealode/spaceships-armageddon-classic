using System.Collections.Generic;
using CodeBase.Infrastructure.Services.PersistentProgress;

namespace CodeBase.Factory
{
    public interface IProgressWriterHolder
    {
        List<ISavedProgressWriter> ProgressWriters { get; }
        void Cleanup();
    }
}