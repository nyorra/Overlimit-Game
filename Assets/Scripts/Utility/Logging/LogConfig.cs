// Логирование по трем уровням: warning, error, success. Различие по цветам

namespace OVERLIMIT.Logging
{
    public static class LogConfig
    // Задаем префиксы и цвета для логов
    {
        public const string ColorSuccess = "#77ffaa";
        public const string ColorWarning = "#ffff00";
        public const string ColorError = "#ff0000";

        public const string PrefixSuccess = "<b>[SUCCESS]</b>";
        public const string PrefixWarning = "<b>[WARN]</b>";
        public const string PrefixError = "<b>[ERROR]</b>";
    }
}