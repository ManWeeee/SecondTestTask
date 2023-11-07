using System;
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
    [SerializeField] private CellAnimator _animator;
    [SerializeField] private int cellMergeCost = 1;
    private CellAnimation _currentAnimation;
    
    public int X { get; private set; }
    public int Y { get; private set; }
    public int Value { get; private set; }
    public int Number => IsEmpty == true ? 0 : (int)Math.Pow(2, Value);
    public bool IsEmpty => Value == 0;
    public bool HasMerged { get; private set; }
    public Image Background => background;
    public TextMeshProUGUI Text => text;
    public float GetSize => background.rectTransform.sizeDelta.x;
    public CellAnimator Animator => _animator;
    
    [Inject] 
    public void Construct(ColorHolder holder, CellAnimator cellAnimator)
    {
        this.colorHolder = holder;
        _animator = cellAnimator;
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
        
    }

    public void ResetFlag()
    {
        HasMerged = false;
    }

    public void MergeWithCell(Cell cell)
    {
        _animator.SmoothMerging(this, cell, true);
        UIController.OnScoreChanged.Invoke(cellMergeCost);
        cell.IncreaseValue();
        SetTile(0);
        
    }

    public void MoveToCell(Cell target)
    {
        _animator.SmoothMerging(this, target, false);
        target.SetTile(Value, false);
        SetTile(0);
        
    }
    
    public void SetTile(int x, int y, int value)
    {
        X = x;
        Y = y;
        Value = value;

        UpdateTile();
    }

    public void SetTile(int value, bool updateUI = true)
    {
        Value = value;
        if(updateUI)
            UpdateTile();
    }

    public void UpdateTile()
    {
        text.text = IsEmpty ? "" : Number.ToString();
        
        text.color = Value <= 2 ? colorHolder.BrightColorText : colorHolder.DarkColorText;

        background.color = colorHolder[Value];
    }

    public void SetAnimation(CellAnimation animation)
    {
        _currentAnimation = animation;
    }

    public void CancelAniamtion()
    {
        if (_currentAnimation)
            _currentAnimation.Destroy();
        
    }
}
