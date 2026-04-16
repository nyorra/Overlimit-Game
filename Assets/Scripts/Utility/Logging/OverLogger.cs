using UnityEngine;
using System;
using System.Reflection;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

namespace OVERLIMIT.Logging
{
    /// <summary>
    /// Глобальная система логирования. 
    /// Поддерживает фильтрацию по уровням, цветовое форматирование и контекстные ссылки на объекты.
    /// </summary>
    public static class OverLogger
    {
        // Кэш настроек
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

        // сравнивает уровень лога с минимально допустимым в настройках
        private static bool IsAllowed(LogLevels level) => Settings.MinimumLevel <= level;

        // Выводит позитивное уведомление. Вырезается из финального билда благодаря [Conditional].
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        [UnityEngine.HideInCallstack]
        public static void LogSuccess(string message, Object context = null)
        {
            if (IsAllowed(LogLevels.Success))
                Debug.Log(Format(LogConfig.PrefixSuccess, message, LogConfig.ColorSuccess, context), context);
        }

        //Выводит предупреждение. Вырезается из финального билда.
        [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
        [UnityEngine.HideInCallstack]
        public static void LogWarning(string message, Object context = null)
        {
            if (IsAllowed(LogLevels.Warning))
                Debug.LogWarning(Format(LogConfig.PrefixWarning, message, LogConfig.ColorWarning, context), context);
        }

        // Выводит критическую ОШИБКУ. Работает во всех типах билдов.
        [UnityEngine.HideInCallstack]
        public static void LogError(string message, Object context = null)
        {
            if (IsAllowed(LogLevels.Error))
                Debug.LogError(Format(LogConfig.PrefixError, message, LogConfig.ColorError, context), context);
        }

        // Выводит системное ИСКЛЮЧЕНИЕ, сохраняя полную структуру ошибки
        [UnityEngine.HideInCallstack]
        public static void LogError(Exception ex, Object context = null)
        {
            if (!IsAllowed(LogLevels.Error) || ex == null) return;
            Debug.LogError(Format(LogConfig.PrefixError, $"Exception: {ex.Message}", LogConfig.ColorError, context), context);
            Debug.LogException(ex, context);
        }

        private static string Format(string prefix, string message, string color, Object context)
        {
            message ??= "NULL";
            string contextName = context != null ? $"[{context.name}] " : "";
            string finalMsg = $"{prefix} {contextName}{message}";

#if UNITY_EDITOR
            return $"<color={color}>{finalMsg}</color>";
#else
            if (Settings.EnableColorsInBuild)
                return $"<color={color}>{finalMsg}</color>";
            return finalMsg;
#endif
        }

        [Conditional("UNITY_EDITOR")]
        public static void ClearConsole()
        {
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            type.GetMethod("Clear").Invoke(new object(), null);
        }
    }
}
