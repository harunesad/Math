using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] CanvasGroup menu, mod, customize, scoreboard, market, game;
    [SerializeField] Button modBtn, customizeBtn, scoreboardBtn, marketBtn, languageBtn, soundBtn, backBtn;
    [SerializeField] List<Button> modsBtn, customizesBtn;
    [SerializeField] GameStates gameStates;
    [SerializeField] GameUIManager gameUIManager;
    void Start()
    {
        modBtn.onClick.AddListener(delegate { WindowChange(mod, menu); });
        customizeBtn.onClick.AddListener(delegate { WindowChange(customize, menu); });
        scoreboardBtn.onClick.AddListener(delegate { WindowChange(scoreboard, menu); });
        marketBtn.onClick.AddListener(delegate { WindowChange(market, menu); });

        languageBtn.onClick.AddListener(delegate { PanelOpenClose(languageBtn.transform.GetChild(0).gameObject); });
        soundBtn.onClick.AddListener(delegate { PanelOpenClose(soundBtn.transform.GetChild(0).gameObject); });

        backBtn.onClick.AddListener(BackToMenu);

        for (int i = 0; i < modsBtn.Count; i++)
        {
            int j = i;
            modsBtn[i].onClick.AddListener(delegate { StateSelect(j, GameStates.RandomState.Random); });
        }
        for (int i = 0; i < customizesBtn.Count; i++)
        {
            int j = i;
            customizesBtn[i].onClick.AddListener(delegate { StateSelect(j, GameStates.RandomState.Input); });
        }
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
            gameUIManager.coinText.transform.parent = show.transform;
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
                gameUIManager.enabled = false;
                transform.GetChild(i).GetComponent<CanvasGroup>().DOFade(0, 1).OnComplete(() =>
                {
                    menu.interactable = true;
                    menu.blocksRaycasts = true;
                    menu.DOFade(1, 1);
                    languageBtn.transform.parent = menu.transform;
                    soundBtn.transform.parent = menu.transform;
                    backBtn.transform.parent = menu.transform;
                });
                gameUIManager.CoinReset();            }
        }
    }
    void PanelOpenClose(GameObject panel)
    {
        panel.SetActive(!panel.activeSelf);
    }
    void StateSelect(int i, GameStates.RandomState randomState)
    {
        if (randomState == GameStates.RandomState.Input && gameUIManager.max.value < gameUIManager.min.value)
        {
            return;
        }
        switch (i)
        {
            case 0:
                gameStates.randomState = randomState;
                gameStates.gameState = GameStates.GameState.Plus;
                break;
            case 1:
                gameStates.randomState = randomState;
                gameStates.gameState = GameStates.GameState.Minus;
                break;
            case 2:
                gameStates.randomState = randomState;
                gameStates.gameState = GameStates.GameState.Multiplication;
                break;
            case 3:
                gameStates.randomState = randomState;
                gameStates.gameState = GameStates.GameState.Division;
                break;
            case 4:
                gameStates.randomState = randomState;
                gameStates.gameState = GameStates.GameState.Mixed;
                break;
            default:
                break;
        }
        if (randomState == GameStates.RandomState.Random)
        {
            WindowChange(game, mod);
            gameUIManager.TimeStart(false);
        }
        else
        {
            WindowChange(game, customize);
            gameUIManager.TimeStart(true);
        }
        gameUIManager.enabled = true;
        gameUIManager.QuestionAndAnswers();
        gameUIManager.Countdown();
    }
}
