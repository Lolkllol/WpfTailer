using System;
using System.IO;
using System.Threading.Tasks;
using Core.Services;

public class FileWatcherTracker : IFileTracker
{
    private readonly string _fileName;
    public FileWatcherTracker(string fileName)
    {
        this._fileName = fileName;
    }

    ~FileWatcherTracker()
    {
        StopTracking();
    }
    public event Action<object, FileAppendedEventArgs> FileAppended;
    public event Action FileDeleted;
    public event RenamedEventHandler Renamed;

    public FileSystemWatcher FileWatcher
    {
        get; protected set;
    }
    
    public Task StartTracking()
    {
        FileWatcher = new FileSystemWatcher();
        FileWatcher.Path = Path.GetDirectoryName(_fileName);
        FileWatcher.Filter = Path.GetFileName(_fileName);

        FileWatcher.Changed += FileChangedHandler;
        FileWatcher.Deleted += FileDeletedHandler;

        FileWatcher.EnableRaisingEvents = true;
        return Task.CompletedTask;
    }

    public async Task<string> GetFileContent()
    {
        try
        {
            return await File.ReadAllTextAsync(_fileName);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return string.Empty;
    }

    private async void FileChangedHandler(object sender, FileSystemEventArgs args)
    {
        var text = await this.GetFileContent();
        FileAppended?.Invoke(this, new FileAppendedEventArgs(text));
    }    

    private void FileDeletedHandler(object sender, FileSystemEventArgs args)
    {
        StopTracking();
    }

    public void StopTracking()
    {
        FileWatcher.EnableRaisingEvents = false;
        FileDeleted?.Invoke();
    }
}