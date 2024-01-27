using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsText : MonoBehaviour
{
    public static int Coins;
    public TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Coins.ToString();
    }
}
