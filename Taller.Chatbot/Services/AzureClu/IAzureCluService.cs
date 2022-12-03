using System.Threading.Tasks;

namespace Taller.Chatbot.Services.AzureClu
{
    public interface IAzureCluService
    {
        Task<AzureCluModel> Execute(string text);
    }
}
