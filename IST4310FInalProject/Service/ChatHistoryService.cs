using Microsoft.SemanticKernel.ChatCompletion;

namespace IST4310FInalProject.Service
{
    public class ChatHistoryService : IChatHistoryService
    {
        private readonly ChatHistory _chatHistory = new ChatHistory();

        public ChatHistory GetChatHistory()
        {
            return _chatHistory;
        }

        public void AddMessage(string message, bool isUserMessage)
        {
            if (isUserMessage)
            {
                _chatHistory.AddUserMessage(message);
            }
            //else
            //{
            //    _chatHistory.AddAssistantMessage(message);
            //}
        }
    }
}
