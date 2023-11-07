using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Canvas gameOverScreen;

    public static Action<int> OnScoreChanged;

    private void Start()
    {
        Game.OnGameOver += ShowGameOverScreen;
    }

    private void OnDestroy()
    {
        Game.OnGameOver -= ShowGameOverScreen;
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.gameObject.SetActive(true);
    }
}
