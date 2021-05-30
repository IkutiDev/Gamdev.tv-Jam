using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour
{
    [Foldout("Programmer stuff")]
    [SerializeField] private EnergyUI energyUI;

    [SerializeField] private int maxEnergy=100;

    [ProgressBar("Energy", "maxEnergy", EColor.Yellow)]
    [ReadOnly] public int currentEnergy=0;

    private void Start()
    {
        energyUI.UpdateEnergyBar(maxEnergy, currentEnergy);
    }

    public void IncreaseEnergy(int increase)
    {
        currentEnergy += increase;
        energyUI.UpdateEnergyBar(maxEnergy, currentEnergy);
    }
}
