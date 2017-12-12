using Microsoft.Bot.Connector.Teams.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace O365_ConnectorCard_Bot
{
    public class O365ConnectorHelper
    {
        public static O365ConnectorCard GetConnectorCard()
        {
            var section = new O365ConnectorCardSection(
            null,
            null,
            null,
            null,
            null,
            null,
            true,
            new List<O365ConnectorCardFact>
            {
                new O365ConnectorCardFact("Gmail", "All your gmail actions in Teams"),
                new O365ConnectorCardFact("Wiki", "Know all the facts around the world"),
                new O365ConnectorCardFact("VSTS", "Know all your task details"),
            });

            var actionCard1 = new O365ConnectorCardActionCard(
                O365ConnectorCardActionCard.Type,
                "Run This Command",
                "card-1",
                new List<O365ConnectorCardInputBase>
                {
                    new O365ConnectorCardMultichoiceInput(
                        O365ConnectorCardMultichoiceInput.Type,
                        "list-1",
                        false,
                        "Select Command",
                        null,
                        new List<O365ConnectorCardMultichoiceInputChoice>
                        {
                            new O365ConnectorCardMultichoiceInputChoice("Gmail", "gmail"),
                            new O365ConnectorCardMultichoiceInputChoice("Wiki", "wiki"),
                            new O365ConnectorCardMultichoiceInputChoice("VSTS", "vsts")
                        },
                        "compact",
                        false)
                },
                new List<O365ConnectorCardActionBase>
                {
                    new O365ConnectorCardHttpPOST(
                        O365ConnectorCardHttpPOST.Type,
                        "Run",
                        "card-1-btn-1",
                        @"{""list1"":""{{list-1.value}}""}")
                });

            var actionCard2 = new O365ConnectorCardActionCard(
                O365ConnectorCardActionCard.Type,
                "Know this Command",
                "card-2",
                new List<O365ConnectorCardInputBase>
                {
                    new O365ConnectorCardMultichoiceInput(
                        O365ConnectorCardMultichoiceInput.Type,
                        "list-2",
                        false,
                        "Select Command",
                        null,
                        new List<O365ConnectorCardMultichoiceInputChoice>
                        {
                            new O365ConnectorCardMultichoiceInputChoice("Gmail", "gmail"),
                            new O365ConnectorCardMultichoiceInputChoice("Wiki", "wiki"),
                            new O365ConnectorCardMultichoiceInputChoice("VSTS", "vsts")
                        },
                        "compact",
                        false),
                    new O365ConnectorCardMultichoiceInput(
                        O365ConnectorCardMultichoiceInput.Type,
                        "list-3",
                        false,
                        "Select Command -2",
                        null,
                        new List<O365ConnectorCardMultichoiceInputChoice>
                        {
                            new O365ConnectorCardMultichoiceInputChoice("Gmail-2", "gmail"),
                            new O365ConnectorCardMultichoiceInputChoice("Wiki-2", "wiki"),
                            new O365ConnectorCardMultichoiceInputChoice("VSTS-2", "vsts")
                        },
                        "compact",
                        false)
                },
                new List<O365ConnectorCardActionBase>
                {
                    new O365ConnectorCardHttpPOST(
                        O365ConnectorCardHttpPOST.Type,
                        "Show help",
                        "card-2-btn-2",
                        @"{""list2"":""{{list-2.value}}"",""list3"":""{{list-3.value}}""}")
                });

            O365ConnectorCard card = new O365ConnectorCard()
            {
                Summary = "O365 card summary",
                ThemeColor = "#E67A9E",
                Title = "Supported commands:",
                Text = "These are all your integrations:",
                Sections = new List<O365ConnectorCardSection> { section },
                PotentialAction = new List<O365ConnectorCardActionBase>
                {
                    actionCard1,
                    actionCard2
                }
            };
            return card;
        }
    }
}