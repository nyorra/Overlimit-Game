namespace OVERLIMIT.Logging
{
    public enum LogLevels
    {
        // В LogSettings MinimumLevel = LogLevel.НАШ_УРОВЕНЬ; Для разного отображения разные уровни
        None = 0, // дефолт значение, гарант что enum не ляжет с багом
        Success = 1, // уровень всех логов
        Warning = 2, // war error
        Error = 3, // only error
        Off = 4, // уровень отключения всех логов

    }
}

