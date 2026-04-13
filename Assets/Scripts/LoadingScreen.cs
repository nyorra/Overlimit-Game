using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using OVERLIMIT.Scenes;
using OVERLIMIT.Logging;

namespace OVERLIMIT.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        public Slider LoadingProgressBar;
        public TMP_Text ContinueHintText;
        public SceneType nextScene = SceneType.MainMenu;

        void Start()
        {
            // проверка на подключение slider и continueText
            if (LoadingProgressBar == null || ContinueHintText == null)
            {
                OverLogger.LogError("UI references are missing in LoadingScreen!", this);
                return;
            }

            ContinueHintText.gameObject.SetActive(false);
            LoadingProgressBar.gameObject.SetActive(true);
            LoadingProgressBar.value = 0f;

            StartCoroutine(LoadProcess());

            // Здесь используем context (this), чтобы при клике на лог сразу найти этот объект на сцене
            OverLogger.LogSuccess("Loading process initialized.", this);
        }

        IEnumerator LoadProcess()
        {
            // Используем название сцены из переменной, чтобы избежать опечаток в строках
            string sceneName = nextScene.ToString();
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

            if (asyncLoad == null)
            {
                OverLogger.LogError($"Failed to start loading scene: {sceneName}");
                yield break;
            }

            asyncLoad.allowSceneActivation = false;

            while (asyncLoad.progress < 0.9f)
            {
                LoadingProgressBar.value = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                yield return null;
            }

            LoadingProgressBar.value = 1f;
            LoadingProgressBar.gameObject.SetActive(false);
            ContinueHintText.gameObject.SetActive(true);

            // Лог уровня Success для подтверждения, что ресурсы в памяти и мы ждем только игрока
            OverLogger.LogSuccess($"Scene '{sceneName}' pre-loaded. Waiting for user input...");

            yield return new WaitUntil(() =>
                (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
                (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame));

            // Лог перед активацией: если игра зависнет в этот момент, ты будешь знать, 
            // что проблема именно в инициализации новой сцены (Start методы новых скриптов)
            OverLogger.LogSuccess($"Activating scene: {sceneName}");
            asyncLoad.allowSceneActivation = true;
            OverLogger.ClearConsole();
        }
    }
}
