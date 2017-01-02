using WineTracker.Interface;
using WineTracker.Models;

namespace WineTracker.ViewModels
{
    public class VoiceVerificationViewModel : BaseViewModel<VoiceVerification>
    {
        public VoiceVerificationViewModel(ICognitiveService CognitiveService)
        {

        }
        public override void Init(object initData)
        {
            base.Init(initData);
          



        }
    }
}
