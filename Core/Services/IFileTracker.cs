using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IFileTracker
    {
        event Action<object, FileAppendedEventArgs> FileAppended;
        Task StartTracking();
        void StopTracking();
    }

    public class FileAppendedEventArgs : EventArgs
    {
        public FileAppendedEventArgs(string appendedLine)
        {
            AppendedLine = appendedLine;
        }
        public string AppendedLine { get; }
    }
}
