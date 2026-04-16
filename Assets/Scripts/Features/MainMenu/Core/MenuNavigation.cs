using UnityEngine;
using System.Collections.Generic;
using OVERLIMIT.Logging;

namespace OVERLIMIT.Menu
{
    /// <summary>
    /// Управляет переключением окон в главном меню (Главная, Гараж, Настройки, Авторы).
    /// </summary>
    public class MenuNavigation : MonoBehaviour
    {
        [Header("Panels")]
        public RectTransform mainScreenPanel;
        public RectTransform garageScreenPanel;
        public RectTransform settingsScreenPanel;
        public RectTransform creditsScreenPanel;

        // Список для удобного перебора всех панелей разом
        private List<RectTransform> _allPanels;

        void Awake()
        {
            // Складываем все панели в «одну корзину»
            _allPanels = new List<RectTransform> {
                mainScreenPanel, garageScreenPanel, settingsScreenPanel, creditsScreenPanel
            };
        }

        public void ShowPanel(RectTransform targetPanel)
        {
            // Если в контроллере забыли указать панель — выдаем ошибку
            if (targetPanel == null)
            {
                OverLogger.LogError("Ошибка: Попытка открыть пустую панель!", this);
                return;
            }

            // Проходим по всем панелям: нужную включаем, остальные — выключаем
            foreach (var panel in _allPanels)
            {
                if (panel != null)
                {
                    // Если панель совпадает с целью — true (включить), если нет — false (выключить)
                    panel.gameObject.SetActive(panel == targetPanel);
                }
            }

            OverLogger.LogSuccess($"Меню: Панель [{targetPanel.name}] теперь на экране.", this);
        }

        // Быстрый метод для возврата на главный экран
        public void ShowMain() => ShowPanel(mainScreenPanel);
    }
}
