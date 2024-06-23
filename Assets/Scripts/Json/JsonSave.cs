using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JsonSave : MonoBehaviour
{
    public static JsonSave jsonSave;
    public SaveVariables sv;
    [SerializeField] GameStates gameStates;
    [SerializeField] GameUIManager gameUIManager;
    [SerializeField] Transform plus, minus, multipication, division, mixed;
    private void Awake()
    {
        jsonSave = this;
    }
    void Start()
    {
        sv = SaveManager.Load();
        if (sv.save)
        {
            ScoreboardShowing();
        }
        else
        {
            ListFill(sv.categories.plus);
            ListFill(sv.categories.minus);
            ListFill(sv.categories.multipication);
            ListFill(sv.categories.division);
            ListFill(sv.categories.mixed);
        }
        GameCoinShowing(gameUIManager.coinText);
    }
    void GameCoinUpdate(float coin)
    {
        sv.gameCoin += coin;
    }
    public void GameCoinShowing(TextMeshProUGUI gameCoinText)
    {
        gameCoinText.text = sv.gameCoin.ToString();
    }
    public void ProductListFill(int count, ProductType.Type productType)
    {
        if (!sv.save)
        {
            switch (productType)
            {
                case ProductType.Type.Pencils:
                    for (int i = 0; i < count; i++)
                    {
                        sv.products.pencils.Add(true);
                    }
                    break;
                case ProductType.Type.Erasers:
                    for (int i = 0; i < count; i++)
                    {
                        sv.products.erasers.Add(true);
                    }
                    break;
                case ProductType.Type.Sharpeners:
                    for (int i = 0; i < count; i++)
                    {
                        sv.products.sharpeners.Add(true);
                    }
                    break;
                case ProductType.Type.TippedPens:
                    for (int i = 0; i < count; i++)
                    {
                        sv.products.tippedPens.Add(true);
                    }
                    break;
                default:
                    break;
            }
        }
    }
    public void ProductSave(ProductType.Type productType, int index)
    {
        switch (productType)
        {
            case ProductType.Type.Pencils:
                sv.products.pencils[index] = false;
                break;
            case ProductType.Type.Erasers:
                sv.products.erasers[index] = false;
                break;
            case ProductType.Type.Sharpeners:
                sv.products.sharpeners[index] = false;
                break;
            case ProductType.Type.TippedPens:
                sv.products.tippedPens[index] = false;
                break;
            default:
                break;
        }
    }
    public void ResultSave(float coin)
    {
        if (gameStates.randomState == GameStates.RandomState.Random)
        {
            GameCoinUpdate(coin);
            switch (gameStates.gameState)
            {
                case GameStates.GameState.Plus:
                    ScoreboardSave(coin, sv.categories.plus, plus);
                    break;
                case GameStates.GameState.Minus:
                    ScoreboardSave(coin, sv.categories.minus, minus);
                    break;
                case GameStates.GameState.Multiplication:
                    ScoreboardSave(coin, sv.categories.multipication, multipication);
                    break;
                case GameStates.GameState.Division:
                    ScoreboardSave(coin, sv.categories.division, division);
                    break;
                case GameStates.GameState.Mixed:
                    ScoreboardSave(coin, sv.categories.mixed, mixed);
                    break;
                default:
                    break;
            }
        }
    }
    void ScoreboardSave(float coin, List<float> proccess, Transform proccessText)
    {
        float point = proccess[0];
        int minIndex = 0;
        for (int i = 0; i < proccess.Count; i++)
        {
            if (point > proccess[i])
            {
                point = proccess[i];
                minIndex = i;
            }
        }
        if (coin > proccess[minIndex])
        {
            proccess[minIndex] = coin;
            proccess.Sort();
            proccess.Reverse();
            sv.save = true;
            SaveManager.Save(sv);
            ScoreboardText(proccess, proccessText);
        }
    }
    void ScoreboardShowing()
    {
        ScoreboardText(sv.categories.plus, plus);
        ScoreboardText(sv.categories.minus, minus);
        ScoreboardText(sv.categories.multipication, multipication);
        ScoreboardText(sv.categories.division, division);
        ScoreboardText(sv.categories.mixed, mixed);
    }
    void ScoreboardText(List<float> proccess, Transform proccessText)
    {
        ListFill(proccess);
        for (int i = 0; i < proccess.Count; i++)
        {
            if (proccess[i] > 0)
            {
                proccessText.transform.GetChild(i).GetComponent<TextMeshProUGUI>().text = (i + 1) + ". " + proccess[i];
            }
        }
    }
    void ListFill(List<float> proccess)
    {
        for (int i = proccess.Count; i < 5; i++)
        {
            proccess.Add(0);
        }
    }
}
