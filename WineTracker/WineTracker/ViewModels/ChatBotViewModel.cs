using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineTracker.Models;

namespace WineTracker.ViewModels
{
    public class ChatBotViewModel : BaseViewModel<WineItemInfo>
    {

        public override void Init(object initData)
        {
            base.Init(initData);
            Model = new WineItemInfo();

            //Adjust the chat windows when the keyboad is open or close.
            KeyboardHelper.KeyboardChanged += (sender, e) =>
            {
                //Default Height is 500
                
            };
        }
    }
}
