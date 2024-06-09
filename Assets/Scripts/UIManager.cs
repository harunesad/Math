using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup menu, mod, customize, scoreboard, market, game;
    [SerializeField] Button modBtn, customizeBtn, scoreboardBtn, marketBtn, languageBtn, soundBtn, backBtn;
    [SerializeField] GameStates gameStates;
    void Start()
    {
        modBtn.onClick.AddListener(delegate { WindowChange(mod, menu); });
        customizeBtn.onClick.AddListener(delegate { WindowChange(customize, menu); });
        scoreboardBtn.onClick.AddListener(delegate { WindowChange(scoreboard, menu); });
        marketBtn.onClick.AddListener(delegate { WindowChange(market, menu); });

        languageBtn.onClick.AddListener(delegate { PanelOpenClose(languageBtn.transform.GetChild(0).gameObject); });
        soundBtn.onClick.AddListener(delegate { PanelOpenClose(soundBtn.transform.GetChild(0).gameObject); });

        backBtn.onClick.AddListener(BackToMenu);
    }
    void Update()
    {
        
    }
    void WindowChange(CanvasGroup show, CanvasGroup hide)
    {
        hide.interactable = false;
        hide.blocksRaycasts = false;
        hide.DOFade(0, 1).OnComplete(() =>
        {
            show.interactable = true;
            show.blocksRaycasts = true;
            show.DOFade(1, 1);
            languageBtn.transform.parent = show.transform;
            soundBtn.transform.parent = show.transform;
            backBtn.transform.parent = show.transform;
        });
    }
    void BackToMenu()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<CanvasGroup>() && transform.GetChild(i).GetComponent<CanvasGroup>().alpha == 1 && menu.alpha == 0)
            {
                transform.GetChild(i).GetComponent<CanvasGroup>().interactable = false;
                transform.GetChild(i).GetComponent<CanvasGroup>().blocksRaycasts = false;
                transform.GetChild(i).GetComponent<CanvasGroup>().DOFade(0, 1).OnComplete(() =>
                {
                    menu.interactable = true;
                    menu.blocksRaycasts = true;
                    menu.DOFade(1, 1);
                    languageBtn.transform.parent = menu.transform;
                    soundBtn.transform.parent = menu.transform;
                    backBtn.transform.parent = menu.transform;
                });
            }
        }
    }
    void PanelOpenClose(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
}
