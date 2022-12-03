using Microsoft.Bot.Schema;
using System.Collections.Generic;

namespace Taller.Chatbot.Services.AzureClu
{
    public class AzureCluModel
    {
        public Result Result { get; set; }
    }
    public class Result
    {
        public string Query { get; set; }
        public Prediction Prediction { get; set; }
    }
    public class Prediction
    {
        public string TopIntent { get; set; }
        public string ProjectKind { get; set; }
        public List<Intent> Intents { get; set; }
    }
    public class Intent
    {
        public string Category { get; set; }
        public double ConfidenceScore { get; set; }
    }
}
