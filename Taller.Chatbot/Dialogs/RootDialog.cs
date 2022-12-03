using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Threading;
using System.Threading.Tasks;
using Taller.Chatbot.Services.AzureClu;

namespace Taller.Chatbot.Dialogs
{
    public class RootDialog: ComponentDialog
    {
        private readonly IAzureCluService _azureCluService;
        public RootDialog(IAzureCluService azureCluService)
        {
            _azureCluService = azureCluService;
            var waterfallSteps = new WaterfallStep[]
            {
                StartDialog,
                EndDialog
            };

            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), waterfallSteps));
        }

        private async Task<DialogTurnResult> StartDialog(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var userText = stepContext.Context.Activity.Text;
            var resultClu = await _azureCluService.Execute(userText);

            var topIntent = resultClu.Result.Prediction.TopIntent;

            switch (topIntent.ToUpper())
            {
                case "SALUDAR":
                    await Saludar(stepContext);
                    break;

                case "DESPEDIR":
                    await Despedir(stepContext);
                    break;

                case "AGRADECER":
                    await Agradecer(stepContext);
                    break;

                default:
                    await Ninguno(stepContext);
                    break;
            }

            return await stepContext.NextAsync();
        }

        private async Task Ninguno(WaterfallStepContext stepContext)
        {
            await stepContext.Context.SendActivityAsync("Lo siento, pero no puedo entenderte.");
        }

        private async Task Agradecer(WaterfallStepContext stepContext)
        {
            await stepContext.Context.SendActivityAsync("Gracias a ti por escribirme");
        }

        private async Task Despedir(WaterfallStepContext stepContext)
        {
            await stepContext.Context.SendActivityAsync("Hasta pronto, aquí estaré siempre.");
        }

        private async Task Saludar(WaterfallStepContext stepContext)
        {
            await stepContext.Context.SendActivityAsync("Hola, ¿Cómo puedo ayudarte?");
        }

        private async Task<DialogTurnResult> EndDialog(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.EndDialogAsync();
        }
    }
}
