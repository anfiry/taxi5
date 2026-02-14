using System;
using System.Net.Http;

namespace taxi4.Services
{
    public class SimpleTelegramService
    {
        private readonly string _botToken;
        private readonly long _chatId;

        public SimpleTelegramService(string botToken, long chatId)
        {
            _botToken = botToken;
            _chatId = chatId;
        }

        public bool SendMessage(string text)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string url = $"https://api.telegram.org/bot{_botToken}/sendMessage";

                    var parameters = new System.Collections.Generic.Dictionary<string, string>
                    {
                        { "chat_id", _chatId.ToString() },
                        { "text", text }
                    };

                    var content = new FormUrlEncodedContent(parameters);
                    var response = client.PostAsync(url, content).Result;

                    return response.IsSuccessStatusCode;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Telegram ошибка: {ex.Message}");
                return false;
            }
        }

        public bool SendVerificationCode(string phoneNumber, string code)
        {
            string message = $"Код подтверждения: {code}\n" +
                           $"Для номера: {phoneNumber}\n" +
                           $"Действует 2 минуты";

            return SendMessage(message);
        }

        public bool SendRegistrationNotification(string phoneNumber, string firstName, string lastName, string login)
        {
            string message = $"✅ Новая регистрация\n\n" +
                           $"👤 {firstName} {lastName}\n" +
                           $"📱 {phoneNumber}\n" +
                           $"🔑 {login}\n" +
                           $"📅 {DateTime.Now:dd.MM.yyyy HH:mm}";

            return SendMessage(message);
        }
    }
}