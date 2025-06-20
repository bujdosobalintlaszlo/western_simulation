using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
public class QuickSand : Field
{
    public QuickSand(int xIndex, int yIndex, int zIndex, string type, bool walkable) : base(xIndex, yIndex, zIndex, type, walkable)
    {
    }
    void Awake()
    {
        SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>("Sprites/QuickSand");
    }
    public override void DisplayField()
    {
        GameObject panel = GameObject.Find("Panel");
        if (panel == null)
        {
            Debug.LogError("Panel not found!");
            return;
        }
        GameObject imageGo = new GameObject("QuickSand");
        imageGo.transform.SetParent(panel.transform, false);

        Image img = imageGo.AddComponent<Image>();
        Sprite sprite = Resources.Load<Sprite>("quickSand1");

        if (sprite == null)
        {
            Debug.LogError("Sprite not found in Resources!");
            return;
        }

        img.sprite = sprite;

        RectTransform rt = imageGo.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(16, 16);
        rt.anchoredPosition = new Vector2(XIndex, YIndex);
        imageGo.transform.SetSiblingIndex(ZIndex);
    }

    public override string ToString()
    {
        return $"(QuickSand) This field has {Type}, and it's position is [x: {XIndex}, y: {YIndex}, z: {ZIndex}";
    }
}
