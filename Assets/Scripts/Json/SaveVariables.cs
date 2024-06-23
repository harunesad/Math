using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveVariables
{
    public bool save;
    public float gameCoin, extraTime;
    public Products products = new Products();
    public Categories categories = new Categories();
}
[Serializable]
public class Categories
{
    public List<float> plus = new List<float>();
    public List<float> minus = new List<float>();
    public List<float> multipication = new List<float>();
    public List<float> division = new List<float>();
    public List<float> mixed = new List<float>();
}
[Serializable]
public class Products
{
    public List<bool> pencils = new List<bool>();
    public List<bool> erasers = new List<bool>();
    public List<bool> sharpeners = new List<bool>();
    public List<bool> tippedPens = new List<bool>();
}
