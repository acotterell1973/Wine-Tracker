using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineTracker.Interface
{
    public interface IFileService
    {
        string GetFileContents(string fileUri);
    }
}
