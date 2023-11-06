using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHolder : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private Color brightColorText;
    [SerializeField] private Color darkColorText;

    public Color BrightColorText => brightColorText;
    public Color DarkColorText => darkColorText;
    public Color this[int index] => colors[index];
}
