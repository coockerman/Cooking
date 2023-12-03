using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CookUI : MonoBehaviour
{
    [SerializeField] GameAreaManager gameAreaManager;
    public static CookUI _instance;

    [SerializeField] GameObject UI;
    [SerializeField] Button exitBtn;

    public UnityEvent<bool> statusCookUI;
    
    private void Awake()
    {
        Initialization();
    }
    void Initialization()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        exitBtn.onClick.AddListener(() => SetStatusObj(false));
        statusCookUI.AddListener(SetStatusObj);
    }
    public void SetStatusObj(bool status)
    {
        UI.SetActive(status);
        gameAreaManager.Chef.SetIsWork(status);
    }
}
