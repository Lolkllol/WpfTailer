using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Services
{
    public class FileTracker : IFileTracker
    {
        public event Action<object, FileAppendedEventArgs> FileAppended;

        private string  _filePath;
        private int msSpan = 100;
        private Task _runningTask;
        private CancellationToken cancellationToken;
        public FileTracker(string filePath)
        {
            this._filePath = filePath;
            this._runningTask = this.StartTracking();
        }

        ~FileTracker()
        {
            StopTracking();
        }

        public Task StartTracking()
        {
            cancellationToken = new CancellationToken();

            var task = new Task(async () =>
            {
                await using (var fs = new FileStream(_filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using var sr = new StreamReader(fs);
                    await sr.ReadToEndAsync();
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        try
                        {
                            if (!sr.EndOfStream)
                            {
                                var line = sr.ReadLine();
                                FileAppended?.Invoke(this, new FileAppendedEventArgs(line));
                                Thread.Sleep(msSpan);
                            }
                        }
                        catch (Exception ex)
                        {
                            FileAppended?.Invoke(this, new FileAppendedEventArgs(ex.Message));
                        }
                    }
                }
            }, cancellationToken);
            task.Start();
            return task;
        }

        public void StopTracking()
        {
            
        }

        public Task<string> GetFileContent()
        {
            throw new NotImplementedException();
        }
    }
}
