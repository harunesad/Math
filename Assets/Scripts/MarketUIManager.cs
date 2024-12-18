using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MarketUIManager : MonoBehaviour
{
    [SerializeField] Sprite selectMenu, unselectMenu;
    [SerializeField] Button selectButton;
    [SerializeField] GameUIManager gameUIManager;
    [SerializeField] UIManager uIManager;
    public List<ProductPanels> products;
    void Start()
    {
        for (int i = 0; i < products.Count; i++)
        {
            int j = i;
            products[i].btn.onClick.AddListener(delegate { BuyProduct(j); });
        }
    }
    void Update()
    {
        
    }
    public void SelectMenu(Button myButton)
    {
        selectButton.GetComponent<Image>().sprite = unselectMenu;
        myButton.GetComponent<Image>().sprite = selectMenu;
        selectButton = myButton;
    }
    public void PanelShow(List<Product> products, List<bool> buy)
    {
        for (int i = 0; i < products.Count; i++)
        {
            this.products[i].name.transform.parent.gameObject.SetActive(true);
            this.products[i].name.text = products[i].name;
            this.products[i].price.text = products[i].price.ToString();
            this.products[i].image.sprite = products[i].image;
            this.products[i].buy = buy[i];
            this.products[i].progress = products[i].progress;
            if (!this.products[i].buy)
            {
                this.products[i].price.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                this.products[i].price.transform.parent.gameObject.SetActive(true);
            }
        }
        for (int i = products.Count; i < this.products.Count; i++)
        {
            this.products[i].name.transform.parent.gameObject.SetActive(false);
        }
    }
    void BuyProduct(int i)
    {
        Debug.Log(int.Parse(products[i].price.text));
        if (JsonSave.jsonSave.sv.gameCoin >= int.Parse(products[i].price.text))
        {
            JsonSave.jsonSave.sv.gameCoin -= int.Parse(products[i].price.text);
            JsonSave.jsonSave.GameCoinShowing(gameUIManager.coinText);
            //gameUIManager.coinText.text = gameUIManager.gameCoin.ToString();
            products[i].price.transform.parent.gameObject.SetActive(false);
            selectButton.GetComponent<MarketPanel>().buy[i] = false;
            JsonSave.jsonSave.ProductSave(selectButton.GetComponent<MarketPanel>().productType, i);
            JsonSave.jsonSave.sv.extraTime += products[i].progress;
            SaveManager.Save(JsonSave.jsonSave.sv);
        }
        else
        {
            uIManager.Warn("Yeterli paran�z yok.", uIManager.marketEntry);
        }
    }
}
[System.Serializable]
public class ProductPanels
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI price;
    public Image image;
    public Button btn;
    public bool buy;
    public float progress;
}
