using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WashUI : MonoBehaviour
{
    [SerializeField] List<Stain> listStains;
    [SerializeField] GameAreaManager gameAreaManager;
    [SerializeField] GameObject UIBtn;
    [SerializeField] GameObject UIUWash;
    [SerializeField] Button exitBtn;
    public static WashUI _instance;
    public UnityEvent<bool> statusWashUI;
    WashItem washItem;

    bool isClosed;
    [SerializeField] float timeDelayClosed;
    void Start()
    {
        Initialization();
    }
    void Initialization()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        washItem = gameAreaManager.WashItem;
        exitBtn.onClick.AddListener(() => SetStatusObj(false));
        statusWashUI.AddListener(SetStatusObj);
        isClosed = true;
    }
    void LoadStain()
    {
        foreach(Stain stain in listStains)
        {
            stain.IsStain = false;
            stain.gameObject.SetActive(true);
        }
    }
    public void DragStain(Stain stainInput)
    {
        foreach (Stain stain in listStains)
        {
            if(stainInput == stain)
            {
                stain.IsStain = true;
                stainInput.gameObject.SetActive(false);
            }
        }
        CheckStain();
    }
    void CheckStain()
    {
        foreach (Stain stain in listStains)
        {
            if(!stain.IsStain)
            {
                return;
            }
        }
        isClosed = false;
        StartCoroutine(DelayClosed());
    }
    private IEnumerator DelayClosed()
    {
        // Chờ 2 giây
        yield return new WaitForSeconds(timeDelayClosed);
        isClosed = true;
        FinishWash();
        SetStatusObj(false);
    }
    public void SetStatusObj(bool status)
    {
        if (isClosed == false) return;
        UIBtn.SetActive(status);
        UIUWash.SetActive(status);
        if(status)
        {
            LoadStain();
        }
        gameAreaManager.Chef.SetIsWork(status);
    }
    void FinishWash()
    {
        
    }
    
}
