﻿using Azure;
using Azure.AI.Language.Conversations;
using Azure.Core;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Taller.Chatbot.Services.AzureClu
{
    public class AzureCluService: IAzureCluService
    {
        public async Task<AzureCluModel> Execute(string text)
        {
            var endpoint = "";
            var key = "";
            var projectName = "";
            var deploymentName = "";

            AzureKeyCredential credential = new AzureKeyCredential(key);
            Uri endPointUri = new Uri(endpoint);

            var client = new ConversationAnalysisClient(endPointUri, credential);

            var data = new
            {
                kind = "Conversation",
                analysisInput = new
                {
                    conversationItem = new
                    {
                        text = text,
                        id = "1",
                        participantId = "1"
                    }
                },
                parameters = new
                {
                    projectName,
                    deploymentName
                }                
            };
            var response = await client.AnalyzeConversationAsync(RequestContent.Create(data));
            if(response.Status == 200)
            {
                var content = response.Content.ToString();
                var resultModel = JsonConvert.DeserializeObject<AzureCluModel>(content);
                return resultModel;
            }
            return null;
        }
    }
}
