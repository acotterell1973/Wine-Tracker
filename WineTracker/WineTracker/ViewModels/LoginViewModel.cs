using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using Plugin.Media;
using Plugin.Media.Abstractions;
using WineTracker.Models;
using WineTracker.PageModels;
using Xamarin.Forms;

namespace WineTracker.ViewModels
{
    public class LoginViewModel : BaseViewModel<Face>
    {
        private string _wineHunterFacialGroup = "AA08984B-01F6-40A5-B14A-8ACDFAB2BD30";
        private readonly Guid _personId = Guid.NewGuid();
        private readonly IFaceServiceClient _faceServiceClient;
        public LoginViewModel()
        {
            _faceServiceClient = new FaceServiceClient("480ad17176074072865e90a9394ae115");

        }
        public async Task RegisterUser()
        {
            await _faceServiceClient.CreatePersonGroupAsync(_wineHunterFacialGroup, "Wine Hunter Users");

            await _faceServiceClient.TrainPersonGroupAsync(_wineHunterFacialGroup);


        }


        /// <summary>
        /// Take or select a photo to login the user
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> TakePhotoAsync()
        {
            MediaFile photo;
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "WineHunter",
                    Name = $"{_personId}.png"
                });
            }
            else
            {
                photo = await CrossMedia.Current.PickPhotoAsync();
            }
            ProfileImage = ImageSource.FromStream(() => photo.GetStream());

            using (var stream = photo.GetStream())
            {
                var faces = await _faceServiceClient.DetectAsync(stream);
                if (faces == null)
                {
                    await _faceServiceClient.CreatePersonGroupAsync(_wineHunterFacialGroup, "Wine Hunter Users");
                    // Step 3a - Add a face for that person.
                    await _faceServiceClient.AddPersonFaceAsync(_wineHunterFacialGroup, _personId, stream);
                    await _faceServiceClient.TrainPersonGroupAsync(_wineHunterFacialGroup);
                    return true;
                }

                var faceIds = faces.Select(face => face.FaceId).ToArray();
                // Step 4b - Identify the person in the photo, based on the face.
                //     var results = await _faceServiceClient.IdentifyAsync(_wineHunterFacialGroup, faceIds);
                //    var result = results[0].Candidates[0].PersonId;

                // Step 4c - Fetch the person from the PersonId and display their name.
                //    var person = await _faceServiceClient.GetPersonAsync(_wineHunterFacialGroup, result);

            }
            return true;
        }

        public ImageSource ProfileImage { set; get; }

        #region Commnad Handlers
        public Command FindSimilarFaceCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    await TakePhotoAsync();
                });
            }
        }

        public Command RegisterUserCommand
        {
            get
            {
                return new Command( (obj) =>
                {
                    CoreMethods.SwitchOutRootNavigation(NavigationContainerNames.MainContainer);
                });
            }
        }
        #endregion
    }
}
