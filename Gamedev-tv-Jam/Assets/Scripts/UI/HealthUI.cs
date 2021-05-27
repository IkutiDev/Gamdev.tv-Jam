using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private GameObject heartContaintersParent;
    [SerializeField] private Sprite fullHeartContainer;
    [SerializeField] private Sprite emptyHeartContainer;

    int healthPerHeart;
    int heartsCount;

    public void Init(int maxHealth, int currentHealth)
    {
        healthText.text =maxHealth.ToString()+"/"+ currentHealth.ToString();
        heartsCount = heartContaintersParent.GetComponentsInChildren<Image>().Length;
        healthPerHeart = maxHealth / heartsCount;
        //maxFill = hpBarFilImage.rectTransform.sizeDelta.x;
    }

    public void UpdateHpBar(int maxHealth,int currentHealth)
    {
        healthText.text = maxHealth.ToString() + "/" + currentHealth.ToString();
        int disabledHearts = heartsCount - (currentHealth / healthPerHeart);
        if (disabledHearts == heartsCount && currentHealth > 0) disabledHearts = 4;
        for(int i= heartsCount - 1; i>=0 ; i--)
        {
            if (disabledHearts <= 0)
            {
                heartContaintersParent.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = fullHeartContainer;
            }
            else
            {
                heartContaintersParent.transform.GetChild(i).gameObject.GetComponent<Image>().sprite = emptyHeartContainer;
                disabledHearts--;
            }

        }
        //hpBarFilImage.rectTransform.sizeDelta= new Vector2(maxFill*((float)currentHealth/ (float)maxHealth), hpBarFilImage.rectTransform.sizeDelta.y);
    }
}
