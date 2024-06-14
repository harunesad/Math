using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketPanel : MonoBehaviour
{
    Button myButton;
    public List<Product> products;
    [SerializeField] MarketUIManager marketUIManager;
    private void Awake()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(PanelSelect);
        if (gameObject.name == "Pencils")
        {
            marketUIManager.PanelShow(products);
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    void PanelSelect()
    {
        marketUIManager.SelectMenu(myButton);
        marketUIManager.PanelShow(products);
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
