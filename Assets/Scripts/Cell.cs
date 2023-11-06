using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using IInitializable = Zenject.IInitializable;

public class Cell : MonoBehaviour, IInitializable
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private ColorHolder colorHolder;
    
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Value { get; private set; }
    public int Number => IsEmpty == true ? 0 : (int)Math.Pow(2, Value);
    public bool IsEmpty => Value == 0;
    public float GetSize => background.rectTransform.sizeDelta.x;
    
    [Inject] 
    public void Construct(ColorHolder holder)
    {
        this.colorHolder = holder;
    }
    
    public void Initialize()
    {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetTile(int x, int y, int value)
    {
        X = x;
        Y = y;
        Value = value;

        UpdateTile();
    }

    public void SetTile(int value)
    {
        Value = value;
        UpdateTile();
    }

    private void UpdateTile()
    {
        text.text = IsEmpty ? "" : Number.ToString();
        
        text.color = Value <= 2 ? colorHolder.BrightColorText : colorHolder.DarkColorText;

        background.color = colorHolder[Value];
    }

}
