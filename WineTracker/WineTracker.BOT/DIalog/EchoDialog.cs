using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace WineTracker.BOT.DIalog
{
    [Serializable]
    public class EchoDialog : IDialog<Object>
    {
        protected int count = 1;
        public async Task StartAsync(IDialogContext context)
        {
          context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            var message = await argument;
            if (message.Text.ToLower() == "reset")
            {
                PromptDialog.Confirm(context,
                    AfterResetAsync,
                    "Are your sure you want to reset the count?",
                    "Didn't get that!",
                    promptStyle: PromptStyle.None
                    );
            }
            else
            {
                
                await context.PostAsync($"{this.count++}: You said: " + message.Text);
                context.Wait(MessageReceivedAsync);
            }

        }

        private async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> result)
        {
            var confirm = await result;
            if (confirm)
            {
                this.count = 1;
                await context.PostAsync("Reset count");

            }
            else
            {
                await context.PostAsync("No reset for you!");
            }

            context.Wait(MessageReceivedAsync);
        }
    }
}
