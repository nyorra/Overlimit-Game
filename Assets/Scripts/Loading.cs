using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;


public class Loading: MonoBehaviour
{
    public Slider slider;
    public TMP_Text continueText;
    public string NextScene = "MainMenu";

    void Start()
    {   
        //скрытие текста, готовим слайдер
        continueText.gameObject.SetActive(false);
        slider.gameObject.SetActive(true);
        slider.value = 0f;

        // запуск генератора
        StartCoroutine(LoadSceneAsync());
    }

    // Генератор, если оформить while в Start() игре пиздец. Unity пытается сделать все в 1 кадре, генератор стопит программу пока не выполнится кусок
    IEnumerator LoadSceneAsync()
    {
        // Прогружаем сцену
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(NextScene);
        asyncLoad.allowSceneActivation = false;
        
        while (asyncLoad.progress < 0.9f)
         {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            slider.value = progress;
        
            yield return null;
         }

        slider.value = 1f;
        slider.gameObject.SetActive(false);
        continueText.gameObject.SetActive(true);

        yield return new WaitUntil(() => Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame);

        asyncLoad.allowSceneActivation = true;

        Debug.Log("Сцена загружена");

    }
        
}