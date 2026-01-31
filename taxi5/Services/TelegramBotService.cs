using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace taxi4.Services
{
    public class TelegramBotService
    {
        private readonly string _botToken;
        private readonly long _chatId;
        private TelegramBotClient _botClient;

        public TelegramBotService(string botToken, long chatId)
        {
            _botToken = botToken;
            _chatId = chatId;
            _botClient = new TelegramBotClient(_botToken);
        }

        // Отправка кода подтверждения
        public async Task<bool> SendVerificationCodeAsync(string phoneNumber, string verificationCode)
        {
            try
            {
                string message = $"🔐 *Код подтверждения для регистрации в такси*\n\n" +
                                 $"📱 Телефон: `{phoneNumber}`\n" +
                                 $"🔢 Код: `{verificationCode}`\n\n" +
                                 $"⏱ Срок действия: 2 минуты\n" +
                                 $"❓ *Никому не сообщайте этот код!*";

                await _botClient.SendTextMessageAsync(
                    chatId: _chatId,
                    text: message,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                );

                return true;
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                System.Diagnostics.Debug.WriteLine($"Telegram ошибка: {ex.Message}");
                return false;
            }
        }

        // Отправка уведомления о регистрации
        public async Task<bool> SendRegistrationNotificationAsync(string phoneNumber, string firstName, string lastName, string login)
        {
            try
            {
                string message = $"✅ *Новая регистрация в такси*\n\n" +
                                 $"👤 Клиент: {firstName} {lastName}\n" +
                                 $"📱 Телефон: `{phoneNumber}`\n" +
                                 $"🔑 Логин: `{login}`\n" +
                                 $"📅 Время: {DateTime.Now:dd.MM.yyyy HH:mm}";

                await _botClient.SendTextMessageAsync(
                    chatId: _chatId,
                    text: message,
                    parseMode: Telegram.Bot.Types.Enums.ParseMode.Markdown
                );

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Telegram ошибка: {ex.Message}");
                return false;
            }
        }

        // Проверка подключения к боту
        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                var me = await _botClient.GetMeAsync();
                return me != null;
            }
            catch
            {
                return false;
            }
        }
    }
}