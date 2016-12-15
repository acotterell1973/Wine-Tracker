using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineTracker.Models;
using WineTracker.PageModels;

namespace WineTracker.ViewModels
{
    public class UserViewModel : BaseViewModel<Face>
    {
        public async Task<Face> RegisterUser()
        {
            var faceService = new FaceServiceClient("");
        }
    }
}
