using UnityEngine;

namespace OVERLIMIT.Logging
{
    [CreateAssetMenu(fileName = "LogSettings", menuName = "Overlimit/Log Settings")]
    public class LogSettings : ScriptableObject
    {
        // Минимальный уовень отображения логов в консоли
        public LogLevels MinimumLevel = LogLevels.Success;
        // Оптимизация при сборке
        public bool EnableColorsInBuild = false;
    }
}
