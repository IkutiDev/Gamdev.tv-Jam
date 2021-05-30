using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownUI : MonoBehaviour
{
    [SerializeField] private Text cooldownText;

    public void UpdateTimer(int timerValue)
    {
        cooldownText.text = timerValue.ToString();
    }
}
