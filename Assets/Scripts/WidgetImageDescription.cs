using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class ImageDescription
{
    public string Main;
    public string Left;
    public string Right;
}

public class WidgetImageDescription : MonoBehaviour
{

    public TextMeshProUGUI MainText;
    public TextMeshProUGUI LeftText;
    public TextMeshProUGUI RightText;

    public bool IsShown;

    public int Focus = 1;

    public string Selected;

    public UnityEvent OnFocusChanged;

    private void Start()
    {
        Clear();
    }

    public void Show(string description)
    {
        Clear();
        MainText.text = description;
        Selected = MainText.text;
    }

    public void Show(ImageDescription description)
    {
        MainText.text = description.Main ?? "";
        LeftText.text = description.Left ?? "";
        RightText.text = description.Right ?? "";
    }

    public void Clear(int textBox = -2)
    {
        if (textBox == 0)
        {
            if (MainText.text == Selected)
                Selected = null;
            MainText.text = "";
        }
        else if (textBox == 1)
        {
            if (RightText.text == Selected)
                Selected = null;
            RightText.text = "";
        }
        else if (textBox == -1)
        {
            if (LeftText.text == Selected)
                Selected = null;
            LeftText.text = "";
        }
        else
        {
            for (int i = -1; i <= 1; i++)
            {
                Clear(i);
            }
            Selected = null;
        }
    }

    public enum RollDirections { Left, Right }

    /// <summary>
    /// True rolls right and False rolls left
    /// </summary>
    /// <param name="direction"></param>
    public void RollFocus(bool direction)
    {
        void Swap(TextMeshProUGUI a, TextMeshProUGUI b)
        {
            var tmp = a.text;
            a.text = b.text;
            b.text = tmp;
        }


        if (direction)
        {
            Swap(RightText, MainText);
        }
        else
        {
            Swap(LeftText, MainText);
        }

        Selected = MainText.text;

        OnFocusChanged?.Invoke();
    }

}
