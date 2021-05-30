using Gamedev.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController player;

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        player=FindObjectOfType<PlayerController>();
    }
    public void GameOver()
    {
        player.enabled = false;
        Debug.Log("You lose! Git gud!");
    }
}
