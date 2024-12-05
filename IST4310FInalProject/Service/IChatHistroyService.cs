using Microsoft.SemanticKernel.ChatCompletion;

namespace IST4310FInalProject.Service
{
    public interface IChatHistoryService
    {
        ChatHistory GetChatHistory();
        void AddMessage(string message, bool isUserMessage);
    }
}
