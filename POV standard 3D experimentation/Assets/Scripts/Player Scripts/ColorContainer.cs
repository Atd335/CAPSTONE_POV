using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorContainer : MonoBehaviour
{
    public Color _red;
    public Color _yellow;
    public Color _orange;
    public Color _purple;
    public Color _blue;
    public Color _white;
    public Color _black;
    public Color _green;
    public Color _doorColor;
    public Color _buttonColor;

    public static Color red;
    public static Color blue;
    public static Color white;
    public static Color black;
    public static Color green;
    public static Color orange;
    public static Color purple;
    public static Color yellow;
    public static Color doorColor;
    public static Color buttonColor;

    void Awake()
    {
        red = _red;
        blue = _blue;
        white = _white;
        black = _black;
        green = _green;
        orange = _orange;
        purple = _purple;
        yellow = _yellow;
        doorColor = _doorColor;
        buttonColor = _buttonColor;
    }

}
