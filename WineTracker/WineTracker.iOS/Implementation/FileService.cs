using System;
using WineTracker.iOS.Implementation;
using WineTracker.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(FileService))]
namespace WineTracker.iOS.Implementation
{
    public class FileService : IFileService
    {
        public string GetFileContents(string fileUri)
        {
            throw new NotImplementedException();
        }
    }
}
