using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace winetracker.bot.connector.Dialog
{
    [Serializable]
    public class EchoDialog : IDialog<object>
    {
        protected int Count = 1;
        public async Task StartAsync(IDialogContext context)
        {
           context.Wait(MessageReceivedAsync);
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> messageActivity)
        {
            var message = await messageActivity;
            if (message.Text.ToLower() == "reset")
            {
                PromptDialog.Confirm(context,
                    AfterResetAsync,
                    "Are your sure you want to reset the Count?",
                    "Didn't get that!",
                    promptStyle: PromptStyle.None
                    );
            }
            else
            {
                //Sends the message back to the user
                await context.PostAsync($"{Count++}: You said: {message.Text} " );
                //wait for the next message
                context.Wait(MessageReceivedAsync);
            }
        }

        private async Task AfterResetAsync(IDialogContext context, IAwaitable<bool> result)
        {
            var confirm = await result;
            if (confirm)
            {
                Count = 1;
                await context.PostAsync("Reset Count");

            }
            else
            {
                await context.PostAsync("No reset for you!");
            }

            context.Wait(MessageReceivedAsync);
        }
    }
}
