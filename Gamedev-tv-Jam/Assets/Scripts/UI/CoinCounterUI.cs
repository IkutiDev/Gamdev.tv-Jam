using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCounterUI : MonoBehaviour
{

    [SerializeField] private Text coinCounterText;

    int coinCount=0;

    private void Start()
    {
        coinCounterText.text = "0";
    }

    public void IncreaseCointCount()
    {
        coinCount++;
        coinCounterText.text = coinCount.ToString();
    }
}
