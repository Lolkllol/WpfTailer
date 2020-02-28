using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Core.Services;
using MvvmCross.Commands;
using MvvmCross.ViewModels;

namespace Core.ViewModels
{
    public class HomePageViewModel : MvxViewModel
    {
        private FileSystemWatcher watcher;

        private readonly IFIleProvider _fileProvider;

        public HomePageViewModel(IFIleProvider fileProvider)
        {
            this._fileProvider = fileProvider;
        }

        private MvxObservableCollection<FileTabViewModel> fileTabs;
        public MvxObservableCollection<FileTabViewModel> FileTabs
        {
            get
            {
                fileTabs ??= new MvxObservableCollection<FileTabViewModel>();
                return fileTabs;
            }
        }

        public int FileTabsCount => fileTabs == null ? -1 : fileTabs.Count -1;

        private string helloText = "Hello there";
        public string HelloText
        {
            get => helloText;
            set => SetProperty(ref helloText, value);
        }

        private string title = "This is title";
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private ICommand openFileDialogCommand;
        public ICommand OpenFileDialogCommand
        {
            get
            {
                openFileDialogCommand ??= new MvxCommand(OpenFileDialog);
                return openFileDialogCommand;
            }
        }

        private ICommand trackFileCommand;
        public ICommand TrackFileCommand
        {
            get
            {
                trackFileCommand ??= new MvxCommand(Followfile);
                return trackFileCommand;
            }
        }

        private void Followfile()
        {

        }

        private void _fileTracker_FileAppended(object arg1, FileAppendedEventArgs arg2)
        {
            throw new NotImplementedException();
        }

        private void OpenFileDialog()
        {
            string fileName = null;
            try
            {
                fileName = this._fileProvider.SelectFile();
                var fileTab = new FileTabViewModel(fileName);
                FileTabs.Add(fileTab);
                RaisePropertyChanged("FileTabsCount");
            }
            catch(Exception ex)
            {
                HelloText = ex.Message;
            }
        }
    }
}
