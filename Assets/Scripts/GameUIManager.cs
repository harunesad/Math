using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI question, a, b, c, d;
    [SerializeField] TMP_InputField min, max;
    Button aBtn, bBtn, cBtn, dBtn;
    private void Awake()
    {
        aBtn = a.GetComponentInParent<Button>();
        bBtn = b.GetComponentInParent<Button>();
        cBtn = c.GetComponentInParent<Button>();
        dBtn = d.GetComponentInParent<Button>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
