using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject UiLoad;
    public Slider slider;
    public TextMeshProUGUI textMeshPro;
    public void StartGame()
    {
        SceneManager.LoadScene(2);
    }

    public void StartGameHaveUI()
    {
        UiLoad.SetActive(true);
        StartCoroutine(LoadAsynchronously(1));
    }
    
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            textMeshPro.text = ((int)(progress * 100)).ToString() + "%";
            yield return null;
        }
    }
}
