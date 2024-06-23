using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketPanel : MonoBehaviour
{
    Button myButton;
    public List<Product> products;
    [SerializeField] MarketUIManager marketUIManager;
    public ProductType.Type productType;
    public List<bool> buy;
    private void Awake()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(PanelSelect);
    }
    void Start()
    {
        JsonSave.jsonSave.ProductListFill(products.Count, productType);
        switch (productType)
        {
            case ProductType.Type.Pencils:
                buy.AddRange(JsonSave.jsonSave.sv.products.pencils);
                break;
            case ProductType.Type.Erasers:
                buy.AddRange(JsonSave.jsonSave.sv.products.erasers);
                break;
            case ProductType.Type.Sharpeners:
                buy.AddRange(JsonSave.jsonSave.sv.products.sharpeners);
                break;
            case ProductType.Type.TippedPens:
                buy.AddRange(JsonSave.jsonSave.sv.products.tippedPens);
                break;
            default:
                break;
        }
        if (gameObject.name == "Pencils")
        {
            marketUIManager.PanelShow(products, buy);
        }
    }
    void Update()
    {
        
    }
    void PanelSelect()
    {
        marketUIManager.SelectMenu(myButton);
        marketUIManager.PanelShow(products, buy);
    }
}
[System.Serializable]
public class Product
{
    public string name;
    public float price;
    public Sprite image;
    public bool buy;
    public float progress;
}
