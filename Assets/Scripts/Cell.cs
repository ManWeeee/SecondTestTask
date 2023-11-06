using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

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
    public bool HasMerged { get; private set; }
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

    public void IncreaseValue()
    {
        Value++;
        HasMerged = true;
        
        //TODO make some changes to score through some object
        
        UpdateTile();
    }

    public void ResetFlag()
    {
        HasMerged = false;
    }

    public void MergeWithCell(Cell cell)
    {
        cell.IncreaseValue();
        SetTile(0);
        
        UpdateTile();
    }

    public void MoveToCell(Cell target)
    {
        target.SetTile(Value);
        SetTile(0);
        
        UpdateTile();
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
