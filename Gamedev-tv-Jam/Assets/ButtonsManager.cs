using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject options;
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenCredits()
    {
        credits.SetActive(true);
    }
    public void CloseCredits()
    {
        credits.SetActive(false);
    }

    public void OpenOptions()
    {
        options.SetActive(true);
    }
    public void CloseOptions()
    {
        options.SetActive(false);
    }
}
