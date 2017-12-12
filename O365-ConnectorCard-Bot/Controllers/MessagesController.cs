using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector.Teams.Models;
using Microsoft.Bot.Connector.Teams;
using System;

namespace O365_ConnectorCard_Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
            }
            else if (activity.IsO365ConnectorCardActionQuery())
            {
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                return HandleO365ConnectorCardActionQuery(activity, connector);
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        /// <summary>
        /// Handles O365 connector card action queries.
        /// </summary>
        /// <param name="activity">Incoming request from Bot Framework.</param>
        /// <param name="connectorClient">Connector client instance for posting to Bot Framework.</param>
        /// <returns>Task tracking operation.</returns>
        private HttpResponseMessage HandleO365ConnectorCardActionQuery(Activity activity, ConnectorClient connectorClient)
        {

            var connectorCard = O365ConnectorHelper.GetConnectorCard();
            // Update the connector Title and Text for testing.
            connectorCard.Title += " - Updated";
            connectorCard.Text += " - Upddated";

            var httpResponse = Request.CreateResponse(HttpStatusCode.OK);
            string jsonCard = Newtonsoft.Json.JsonConvert.SerializeObject(connectorCard);//   .GetConnectorCardJson(task);

            httpResponse.Headers.Add("CARD-ACTION-STATUS", "The task is uppdate.");
            httpResponse.Headers.Add("CARD-UPDATE-IN-BODY", "true");
            httpResponse.Content = new StringContent(jsonCard, System.Text.Encoding.UTF8);
            httpResponse.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/vnd.microsoft.teams.card.o365connector");

            return httpResponse;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

       
    }
}