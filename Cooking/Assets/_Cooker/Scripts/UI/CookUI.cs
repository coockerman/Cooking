using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CookUI : MonoBehaviour
{
    public static CookUI _instance;
    [SerializeField] GameObject UI;
    [SerializeField] Button exitBtn;
    public UnityEvent<bool> statusCookUI;
    public UnityEvent<bool> StatusCookUI { get { return statusCookUI; } set { statusCookUI = value; } }
    private void Start()
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
        SetStatusObj(false);

    }
    void SetStatusObj(bool status)
    {
        UI.SetActive(status);
    }
}
