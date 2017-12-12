using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams.Models;

namespace O365_ConnectorCard_Bot.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;

            var reply = activity.CreateReply();

            // Get the connector card and send as attachment.
            var connectorCard = O365ConnectorHelper.GetConnectorCard();

            reply.Attachments.Add(connectorCard.ToAttachment());

            // return our reply to the user.
            await context.PostAsync(reply); 

            context.Wait(MessageReceivedAsync);
        }
    }
}