using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketPanel : MonoBehaviour
{
    Button myButton;
    [SerializeField] List<Product> products;
    private void Awake()
    {
        myButton = GetComponent<Button>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
[System.Serializable]
public class Product
{
    public string name;
    public float price;
    public Sprite image;
}
