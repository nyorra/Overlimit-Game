using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OVERLIMIT.Logging;

namespace OVERLIMIT.Loading
{
    /// <summary>
    /// Внешний вид
    /// Двигает полоску прогресса и включает надписи
    /// </summary>
    public class LoadingView : MonoBehaviour
    {
        [Header("UI References")]
        public Slider loadingProgressBar; // Полоска загрузки
        public TMP_Text continueHintText; // Текст "Нажмите любую клавишу"

        public void SetupInitialState()
        {
            // Проверка, не забыли ли мы притащить кнопки в инспектор
            if (loadingProgressBar == null || continueHintText == null)
            {
                OverLogger.LogError("Забыл привязать Slider или Text в инспекторе!", this);
                return;
            }

            // Прячем подсказку, включаем и обнуляем полоску
            continueHintText.gameObject.SetActive(false);
            loadingProgressBar.gameObject.SetActive(true);
            loadingProgressBar.value = 0f;
        }

        public void UpdateProgress(float progress)
        {
            if (loadingProgressBar != null)
            {
                // Unity считает до 0.9, так что делим на 0.9, чтобы полоска дошла до конца
                loadingProgressBar.value = Mathf.Clamp01(progress / 0.9f);
            }
        }

        public void ShowReadyState()
        {
            // Прячем полоску и показываем текст, что можно входить
            loadingProgressBar?.gameObject.SetActive(false);
            continueHintText?.gameObject.SetActive(true);
            OverLogger.LogSuccess("Всё загружено, показываю подсказку игроку.", this);
        }
    }
}
