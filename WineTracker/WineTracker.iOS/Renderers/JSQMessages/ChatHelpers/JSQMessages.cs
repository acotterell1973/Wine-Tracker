// ReSharper disable InconsistentNaming

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Foundation;
using JSQMessagesViewController;
using UIKit;
using WineTracker.iOS.Implementation;
using WineTracker.Models.DirectLineClient;
using WineTracker.Pages;
using Message = JSQMessagesViewController.Message;

namespace WineTracker.iOS.Renderers.JSQMessages.ChatHelpers
{
    public class JSQmessages : MessagesViewController
    {
        MessagesBubbleImage outgoingBubbleImageData;
        MessagesBubbleImage incomingBubbleImageData;

        public List<Message> messages = new List<Message>();

        public BotUser sender { get; set; } //look at the model, sender is given from the forms page

        
        public event System.EventHandler closePage;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // You must set your senderId and display name
            SenderId = sender.Id;
            SenderDisplayName = sender.DisplayName;


            // These MessagesBubbleImages will be used in the GetMessageBubbleImageData override
            var bubbleFactory = new MessagesBubbleImageFactory();
            outgoingBubbleImageData = bubbleFactory.CreateOutgoingMessagesBubbleImage(UIColorExtensions.MessageBubbleLightGrayColor);
            incomingBubbleImageData = bubbleFactory.CreateIncomingMessagesBubbleImage(UIColorExtensions.MessageBubbleBlueColor);

            // Remove the AccessoryButton as we will not be sending pics
            InputToolbar.ContentView.LeftBarButtonItem = null;

            // Remove the Avatars
            CollectionView.CollectionViewLayout.IncomingAvatarViewSize = CoreGraphics.CGSize.Empty;
            CollectionView.CollectionViewLayout.OutgoingAvatarViewSize = CoreGraphics.CGSize.Empty;

            //List for message from Notification Center (coming from the PCL)
            NSNotificationCenter.DefaultCenter.AddObserver((NSString)"OnMessegeReceviedNotification", OnMessegeReceviedNotification);
        }

        private void OnMessegeReceviedNotification(NSNotification notification)
        {
            var x = notification.UserInfo;
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
            CollectionView.CollectionViewLayout.SpringinessEnabled = true;
        }
        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = base.GetCell(collectionView, indexPath) as MessagesCollectionViewCell;

            // Override GetCell to make modifications to the cell
            // In this case darken the text for the sender
            var message = messages[indexPath.Row];
            if (message.SenderId == SenderId)
                cell.TextView.TextColor = UIColor.Black;

            return cell;
        }

        public override nfloat GetMessageBubbleTopLabelHeight(MessagesCollectionView collectionView, MessagesCollectionViewFlowLayout collectionViewLayout, NSIndexPath indexPath)
        {
            return 20.0f;
        }

        public override NSAttributedString GetMessageBubbleTopLabelAttributedText(MessagesCollectionView collectionView, NSIndexPath indexPath)
        {
            var message = messages[indexPath.Row];

            Console.WriteLine(message.SenderDisplayName);
            if (message.SenderId == SenderId)
            {
                return new NSAttributedString(message.SenderDisplayName);
            }

            if (indexPath.Length - 1 > 1)
            {
                var previousMessage = messages[indexPath.Row - 1];
                if (previousMessage.SenderId == SenderId)
                {
                    return null;
                }
            }

            return new NSAttributedString(message.SenderDisplayName);

        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return messages.Count;
        }

        public override IMessageData GetMessageData(MessagesCollectionView collectionView, NSIndexPath indexPath)
        {
            return messages[indexPath.Row];
        }

        public override IMessageBubbleImageDataSource GetMessageBubbleImageData(MessagesCollectionView collectionView, NSIndexPath indexPath)
        {
            var message = messages[indexPath.Row];
            if (message.SenderId == SenderId)
                return outgoingBubbleImageData;
            return incomingBubbleImageData;

        }

        public override IMessageAvatarImageDataSource GetAvatarImageData(MessagesCollectionView collectionView, NSIndexPath indexPath)
        {
            return null;
        }

        public override async void PressedSendButton(UIButton button, string text, string senderId, string senderDisplayName, NSDate date)
        {
            SystemSoundPlayer.PlayMessageSentSound();

            var message = new Message(SenderId, SenderDisplayName, NSDate.Now, text);
            messages.Add(message);
            var botMessage = new Activity {From = new From() {Id = SenderId}, Text = text};

            var messageAttrs = new Dictionary<string, string>
                {
                    {"SENDERID", senderId},
                    {"SENDERDISPLAYNAME", SenderDisplayName},
                    {"MESSAGE", text},
                };

            var messageDict = NSDictionary.FromObjectsAndKeys(messageAttrs.Values.ToArray(), messageAttrs.Keys.ToArray());
            NSNotificationCenter.DefaultCenter.PostNotificationName((NSString)"OnMessegeSendNotification", this, messageDict);
            FinishSendingMessage(true);

            await Task.Delay(500);
        }
        
    }
}