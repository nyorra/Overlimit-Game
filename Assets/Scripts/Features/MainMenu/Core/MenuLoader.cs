using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using OVERLIMIT.Scenes;
using OVERLIMIT.Logging;

namespace OVERLIMIT.Menu
{
    /// <summary>
    /// Простой загрузчик сцен для главного меню. 
    /// Используется для быстрых переходов
    /// </summary>
    public class MenuLoader : MonoBehaviour
    {
        // Защита от "двойного клика": чтобы не запускать загрузку несколько раз подряд
        private bool _isLoading = false;

        public void LoadScene(SceneType scene)
        {
            if (_isLoading) return;
            StartCoroutine(LoadRoutine(scene));
        }

        private IEnumerator LoadRoutine(SceneType scene)
        {
            _isLoading = true;
            string sceneName = scene.ToString();

            OverLogger.LogSuccess($"Меню: начинаем загрузку сцены {sceneName}", this);

            // Запускаем асинхронную загрузку Unity
            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

            if (op == null)
            {
                OverLogger.LogError($"Критическая ошибка: Забыл добавить сцену '{sceneName}' в Build Settings!", this);
                _isLoading = false;
                yield break;
            }

            // Ждем, пока сцена полностью прогрузится (в отличие от LoadingProcessor, здесь идем до конца)
            while (!op.isDone)
            {
                yield return null;
            }
        }
    }
}
