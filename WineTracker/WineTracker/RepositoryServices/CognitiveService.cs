using System;
using System.IO;
using Microsoft.ProjectOxford.Face;
//using Microsoft.ProjectOxford.SpeakerRecognition;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using Microsoft.ProjectOxford.Face.Contract;
using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using WineTracker.Interface;
using Xamarin.Forms;

namespace WineTracker.RepositoryServices
{
    public class CognitiveService : ICognitiveService
    {
        public class RecognizeLanguage
        {
            public string ShortCode { get; set; }
            public string LongName { get; set; }
        }

        private const string subscriptionKey = "480ad17176074072865e90a9394ae115";
        private const string speackRecognitionSubscriptionKey = "ed5a9278bf6247c2896599728b61db26";
        private const string visionSubscriptionKey = "4b652a32b5614c54ae38292d96a70378";

        private readonly IFaceServiceClient _faceServiceClient;
        private readonly IVisionServiceClient _visionServiceClient;

        //  private ISpeakerIdentificationServiceClient _speakerRecognitionServiceClient;
        private string _wineHunterFacialGroupId = "AA08984B-01F6-40A5-B14A-8ACDFAB2BD30";

        public CognitiveService()
        {
            _faceServiceClient = new FaceServiceClient(subscriptionKey);
            _visionServiceClient = new VisionServiceClient(visionSubscriptionKey);
            //   _speakerRecognitionServiceClient = new SpeakerIdentificationServiceClient(speackRecognitionSubscriptionKey);

            // //Face Detection Group
            // var init = Task.Run(async () =>
            //{
            //    try
            //    {
            //         //if the group doesn't exist create it. This will occur only once.
            //         var wineHunterGroup = await _faceServiceClient.GetPersonGroupAsync(_wineHunterFacialGroupId);
            //    }
            //    catch (FaceAPIException faceApiException)
            //    {
            //        if (faceApiException.ErrorCode.Contains("PersonGroupNotFound"))
            //        {
            //            await _faceServiceClient.CreatePersonGroupAsync(_wineHunterFacialGroupId, "Wine Hunter Group");
            //        }
            //    }

            //});
            // init.Wait();
        }

        public async Task<Guid> RegisterFacialRecognitionAsync(Guid personId, Stream face)
        {
            var faces = await _faceServiceClient.DetectAsync(face);
            if (faces == null)
            {
                await _faceServiceClient.CreatePersonGroupAsync(_wineHunterFacialGroupId, "Wine Hunter Users");
                // Step 3a - Add a face for that person.
                var addPersistedFaceResult = await _faceServiceClient.AddPersonFaceAsync(_wineHunterFacialGroupId, personId, face);
                await _faceServiceClient.TrainPersonGroupAsync(_wineHunterFacialGroupId);
                return addPersistedFaceResult.PersistedFaceId;
            }
            return Guid.Empty;
        }



        public async Task<string> ExtractImageTextStringAsync(string language, Stream sourceImage)
        {
            var ocrResult = await ExtractImageTextAsync(language, sourceImage);
            string results = LogOcrResults(ocrResult);
            return results;
        }

        public async Task<OcrResults> ExtractImageTextAsync(string language, Stream sourceImage)
        {
            try
            {
                // Upload an image and perform OCR
                var ocrResult = await _visionServiceClient.RecognizeTextAsync(sourceImage, language);
                string results = LogOcrResults(ocrResult);
                return ocrResult;
            }
            catch (Exception exception)
            {


            }

            return null;
        }

        protected string LogOcrResults(OcrResults results)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (results != null && results.Regions != null)
            {
                stringBuilder.Append("Text: ");
                stringBuilder.AppendLine();
                foreach (var item in results.Regions)
                {
                    foreach (var line in item.Lines)
                    {
                        foreach (var word in line.Words)
                        {
                            stringBuilder.Append(word.Text);
                            stringBuilder.Append(" ");
                        }

                        stringBuilder.AppendLine();
                    }

                    stringBuilder.AppendLine();
                }
            }

            return stringBuilder.ToString();
        }
        private List<RecognizeLanguage> GetSupportedLanguages()
        {
            return new List<RecognizeLanguage>()
            {
                new RecognizeLanguage(){ ShortCode = "unk",     LongName = "AutoDetect"  },
                new RecognizeLanguage(){ ShortCode = "ar",      LongName = "Arabic"  },
                new RecognizeLanguage(){ ShortCode = "zh-Hans", LongName = "Chinese (Simplified)"  },
                new RecognizeLanguage(){ ShortCode = "zh-Hant", LongName = "Chinese (Traditional)"  },
                new RecognizeLanguage(){ ShortCode = "cs",      LongName = "Czech"  },
                new RecognizeLanguage(){ ShortCode = "da",      LongName = "Danish"  },
                new RecognizeLanguage(){ ShortCode = "nl",      LongName = "Dutch"  },
                new RecognizeLanguage(){ ShortCode = "en",      LongName = "English"  },
                new RecognizeLanguage(){ ShortCode = "fi",      LongName = "Finnish"  },
                new RecognizeLanguage(){ ShortCode = "fr",      LongName = "French"  },
                new RecognizeLanguage(){ ShortCode = "de",      LongName = "German"  },
                new RecognizeLanguage(){ ShortCode = "el",      LongName = "Greek"  },
                new RecognizeLanguage(){ ShortCode = "hu",      LongName = "Hungarian"  },
                new RecognizeLanguage(){ ShortCode = "it",      LongName = "Italian"  },
                new RecognizeLanguage(){ ShortCode = "ja",      LongName = "Japanese"  },
                new RecognizeLanguage(){ ShortCode = "ko",      LongName = "Korean"  },
                new RecognizeLanguage(){ ShortCode = "nb",      LongName = "Norwegian"  },
                new RecognizeLanguage(){ ShortCode = "pl",      LongName = "Polish"  },
                new RecognizeLanguage(){ ShortCode = "pt",      LongName = "Portuguese"  },
                new RecognizeLanguage(){ ShortCode = "ro",      LongName = "Romanian" },
                new RecognizeLanguage(){ ShortCode = "ru",      LongName = "Russian"  },
                new RecognizeLanguage(){ ShortCode = "sr-Cyrl", LongName = "Serbian (Cyrillic)" },
                new RecognizeLanguage(){ ShortCode = "sr-Latn", LongName = "Serbian (Latin)" },
                new RecognizeLanguage(){ ShortCode = "sk",      LongName = "Slovak" },
                new RecognizeLanguage(){ ShortCode = "es",      LongName = "Spanish"  },
                new RecognizeLanguage(){ ShortCode = "sv",      LongName = "Swedish"  },
                new RecognizeLanguage(){ ShortCode = "tr",      LongName = "Turkish"  }
            };
        }
    }
}
