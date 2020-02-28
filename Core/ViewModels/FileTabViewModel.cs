using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Services;
using MvvmCross;
using MvvmCross.ViewModels;

namespace Core.ViewModels
{
    public class FileTabViewModel : MvxViewModel
    {
        private IFileTracker _trackedFile;
        public FileTabViewModel(string filePath)
        {
            FilePath = filePath;
            _trackedFile = new FileWatcherTracker(filePath);
            _trackedFile.StartTracking();

            Task.Factory.StartNew(async () => 
            {
              var fileContent = await _trackedFile.GetFileContent();
                _ = AsyncDispatcher.ExecuteOnMainThreadAsync(() => { this.FileContent = fileContent; });
            });
            _trackedFile.FileAppended += (o, args) => { this.FileContent = args.AppendedLine; };
        }

        public string FilePath { get; }

        private string fileContent;
        public string FileContent
        {
            get => fileContent;
            set => SetProperty(ref fileContent, value);
        }

        public string FileName => Path.GetFileName(FilePath);
    }
}
