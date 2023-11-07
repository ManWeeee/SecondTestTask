using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointText;
    private int _points;

    private void Start()
    {
        UIController.OnScoreChanged += AddPoint;
    }

    public void AddPoint(int point)
    {
        _points += point;
        SetPoints();
    }
    
    private void SetPoints()
    {
        pointText.text = _points.ToString();
    }
}
