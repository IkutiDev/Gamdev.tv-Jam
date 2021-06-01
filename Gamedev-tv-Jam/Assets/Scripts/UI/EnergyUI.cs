using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyUI : MonoBehaviour
{
    [Foldout("Programming stuff")]
    [SerializeField] private Image energyFillImage;
    float maxEnergyGaugeWidth;
    // Start is called before the first frame update
    void Awake()
    {
        maxEnergyGaugeWidth = energyFillImage.rectTransform.sizeDelta.x;
    }
    public void UpdateEnergyBar(int maxEnergy, int currentEnergy)
    {
        energyFillImage.rectTransform.sizeDelta = new Vector2(maxEnergyGaugeWidth * ((float)currentEnergy / (float)maxEnergy), energyFillImage.rectTransform.sizeDelta.y);
    }
}
