using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using OVERLIMIT.Scenes;

namespace OVERLIMIT.Loading
{
    
    public class LoadingScreen  : MonoBehaviour
    {
        public Slider slider;
        public TMP_Text continueText;

        public SceneType nextScene = SceneType.MainMenu; 

        void Start()
        {   
            //скрытие текста, готовим слайдер
            continueText.gameObject.SetActive(false);
            slider.gameObject.SetActive(true);
            slider.value = 0f;

            // запуск генератора
            StartCoroutine(LoadProcess());
        }

        // Генератор, если оформить while в Start() игре пиздец. Unity пытается сделать все в 1 кадре,
        // генератор стопит программу пока не выполнится кусок
        IEnumerator LoadProcess()
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");
                
                asyncLoad.allowSceneActivation = false;

                while (asyncLoad.progress < 0.9f)
                {
                    slider.value = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                    yield return null;
                }

                slider.value = 1f;
                slider.gameObject.SetActive(false);
                continueText.gameObject.SetActive(true);

                // Ждем нажатия
                 yield return new WaitUntil(() => 
                (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame) ||
                (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame));

                asyncLoad.allowSceneActivation = true;
            }
    }
}