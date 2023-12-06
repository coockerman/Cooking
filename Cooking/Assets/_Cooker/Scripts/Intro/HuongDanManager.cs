using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HuongDanManager : MonoBehaviour
{
    [SerializeField] GameObject gameObjectNext;
    [SerializeField] Button clickButton;
    
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] string textToPrint;
    [SerializeField] float printSpeed = 0.1f;
    bool isShowFullText = false;
    private void OnEnable()
    {
        textMeshProUGUI.text = "";
        StartCoroutine(PrintText());
        clickButton.onClick.AddListener(OnShowText);
    }
    public void OnShowText()
    {
        isShowFullText = true;
        textMeshProUGUI.text = textToPrint;
        clickButton.onClick.RemoveAllListeners();
        clickButton.onClick.AddListener(nextObject);
    }
    private IEnumerator PrintText()
    {
        for (int i = 0; i < textToPrint.Length; i++)
        {
            if(isShowFullText == true)
            {
                yield return null;
            }else
            {
                char currentChar = textToPrint[i];
                textMeshProUGUI.text += currentChar;
            }
            yield return new WaitForSeconds(printSpeed);
        }
        yield return new WaitForSeconds(1f);
        nextObject();
    }
    void nextObject()
    {
        if(gameObjectNext != null)
        {
            gameObjectNext.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            LoadNextScene();
            gameObject.SetActive(false);
        }
    }
    void LoadNextScene()
    {
        SceneManager.LoadScene(3);
    }
}
