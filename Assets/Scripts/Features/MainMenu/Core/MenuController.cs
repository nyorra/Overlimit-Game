using UnityEngine;
using UnityEngine.UI;
using OVERLIMIT.Scenes;
using OVERLIMIT.Logging;

namespace OVERLIMIT.Menu
{
    /// <summary>
    /// Главный мозг меню. Связывает кнопки на экране с действиями
    /// переключение окон или запуск загрузки города.
    /// </summary>
    public class MenuController : MonoBehaviour
    {
        [Header("Scene Settings")]
        public SceneType cityScene; // Сцена, куда поедем (обычно City)

        [Header("References")]
        [SerializeField] private MenuNavigation navigation; // Скрипт переключения панелей
        [SerializeField] private MenuLoader loader;         // Скрипт загрузки сцен

        [Header("Buttons")]
        public Button toCityButton;    // Кнопка "Играть"
        public Button garageButton;    // Кнопка "Гараж"
        public Button settingsButton;  // Кнопка "Настройки"
        public Button creditsButton;   // Кнопка "Авторы"
        public Button[] backButtons;   // Все кнопки "Назад" со всех панелей разом

        void Start()
        {
            // Проверка на установку всех компонентов
            if (!ValidateComponents()) return;

            // Основные кнопки
            toCityButton.onClick.AddListener(() => loader.LoadScene(cityScene));
            garageButton.onClick.AddListener(() => navigation.ShowPanel(navigation.garageScreenPanel));
            settingsButton.onClick.AddListener(() => navigation.ShowPanel(navigation.settingsScreenPanel));
            creditsButton.onClick.AddListener(() => navigation.ShowPanel(navigation.creditsScreenPanel));

            // Кнопки "Назад"
            for (int i = 0; i < backButtons.Length; i++)
            {
                if (backButtons[i] != null)
                {
                    backButtons[i].onClick.AddListener(navigation.ShowMain);
                }
                else
                {
                    // Пишем индекс, чтобы в инспекторе было легко найти пустую ячейку
                    OverLogger.LogWarning($"Кнопка 'Назад' под номером [{i}] не назначена в списке!", this);
                }
            }

            OverLogger.LogSuccess("Меню настроено.", this);
        }

        private bool ValidateComponents()
        {
            // Короткая проверка: если мы забыли что-то притащить в инспектор, логгер об этом скажет
            if (navigation != null && loader != null) return true;

            OverLogger.LogError("Забыл привязать Navigation или Loader в инспекторе!", this);
            return false;
        }
    }
}
