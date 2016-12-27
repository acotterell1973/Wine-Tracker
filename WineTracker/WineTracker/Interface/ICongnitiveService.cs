using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Vision.Contract;

namespace WineTracker.Interface
{
    public interface ICognitiveService
    {
        Task<Guid> RegisterFacialRecognitionAsync(Guid personId, Stream face);

        Task<OcrResults> ExtractImageTextAsync(string language, Stream sourceImage);
        Task<string> ExtractImageTextStringAsync(string language, Stream sourceImage);
    }
}