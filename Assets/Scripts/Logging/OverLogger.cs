using UnityEngine;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace OVERLIMIT.Logging
{
    public static class OverLogger
    {
        // Кэш настроек, загруженных из Resources
        private static LogSettings _settings;
        // Флаг, гарантирующий загрузку настроек только один раз за сессию
        private static bool _settingsLoaded;

        private static LogSettings Settings
        {
            get
            {
                if (!_settingsLoaded)
                {
                    _settings = Resources.Load<LogSettings>("LogSettings");
                    // Если файл не найден, создаем временный экземпляр в памяти, чтобы избежать ошибок
                    if (_settings == null) _settings = ScriptableObject.CreateInstance<LogSettings>();
                    _settingsLoaded = true;
                }
                return _settings;
            }
        }

        // Фильтр: сравнивает уровень лога с минимально допустимым в настройках
        private static bool IsAllowed(LogLevel level) => Settings.MinimumLevel <= level;

        // Выводит позитивное уведомление. Вырезается из финального билда.
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        [UnityEngine.HideInCallstack]
        public static void LogSuccess(string message, Object context = null)
        {
            if (!IsAllowed(LogLevel.Success)) return;
            Debug.Log(Format(LogConfig.PrefixSuccess, message, LogConfig.ColorSuccess), context);
        }

        // Выводит предупреждение. Вырезается из финального билда.
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        [UnityEngine.HideInCallstack]
        public static void LogWarning(string message, Object context = null)
        {
            if (!IsAllowed(LogLevel.Warning)) return;
            Debug.LogWarning(Format(LogConfig.PrefixWarning, message, LogConfig.ColorWarning), context);
        }

        // Выводит критическую ошибку. Работает во всех типах билдов.
        [UnityEngine.HideInCallstack]
        public static void LogError(string message, Object context = null)
        {
            if (!IsAllowed(LogLevel.Error)) return;
            Debug.LogError(Format(LogConfig.PrefixError, message, LogConfig.ColorError), context);
        }

        // Выводит системное исключение, сохраняя полную структуру ошибки и StackTrace
        [UnityEngine.HideInCallstack]
        public static void LogError(Exception ex, Object context = null)
        {
            if (!IsAllowed(LogLevel.Error) || ex == null) return;
            
            // Стилизованный заголовок ошибки для визуального единства
            Debug.LogError(Format(LogConfig.PrefixError, $"Exception: {ex.Message}", LogConfig.ColorError), context);
            // Системный вызов для сохранения кликабельного пути к ошибке
            Debug.LogException(ex, context);
        }

        // Форматирует строку: добавляет цвета (в Editor) и префиксы, проверяет на null
        private static string Format(string prefix, string message, string color)
        {
            message ??= "NULL"; 

#if UNITY_EDITOR
            // В редакторе всегда используем Rich Text для наглядности
            return $"<color={color}>{prefix} {message}</color>";
#else
            // В билде красим только если это явно разрешено (цвета могут портить внешние логи)
            if (Settings.EnableColorsInBuild)
                return $"<color={color}>{prefix} {message}</color>";
            return $"{prefix} {message}";
#endif
        }
    }
}
