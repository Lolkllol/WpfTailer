using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Core.Services;
using MvvmCross.ViewModels;

namespace Core.ViewModels
{
    public class FileTabViewModel : MvxViewModel
    {
        public FileTabViewModel(string tabContent)
        {
            TabContent = tabContent;
            var trackedFile = new FileTracker(tabContent);
            trackedFile.FileAppended += (o, args) => { this.FileContent += args.AppendedLine + "\n"; };
        }

        public string TabContent { get; }

        private string fileContent;
        public string FileContent
        {
            get => fileContent;
            set => SetProperty(ref fileContent, value);
        }
    }
}
