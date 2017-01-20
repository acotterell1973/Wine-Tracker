using System;
using System.Collections.Generic;
using WineTracker.EventHandler;
using WineTracker.Models.DirectLineClient;
namespace WineTracker.Pages
{
    public partial class ChatBotPage : BasePage, IBotMessageHandler
    {
        public ChatBotPage()
        {
            InitializeComponent();
        }

        #region Public Properties
        //set the public fields that describes the sender (specific for ios Renderer)
        public static string SenderName { get; set; } = "Ravi";

        public static string SenderId { get; set; } = "2";


        #endregion

        public event EventHandler<BotMessageEventEventArgs> BotMessageEvent;
        public void UpdateUiWithResponse(string messageType, string message)
        {
            throw new NotImplementedException();
        }
    }



}
