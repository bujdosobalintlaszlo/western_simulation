using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class BaseTerrain : Field
{
    public BaseTerrain(int xIndex, int yIndex, int zIndex, string type,bool walkable) : base(xIndex, yIndex, zIndex, type,walkable)
    {
    }
    void Awake()
    {
        SpriteRenderer renderer = gameObject.AddComponent<SpriteRenderer>();
        renderer.sprite = Resources.Load<Sprite>("Sprites/BaseTerrain");
    }

    public override string ToString()
    {
        return $"(Base Terrain)This field has {Type}, and it's position is [x: {XIndex}, y: {YIndex}, z: {ZIndex}";
    }

    public override void DisplayField()
    {
        GameObject panel = GameObject.Find("Panel");
        if (panel == null)
        {
            Debug.LogError("Panel not found!");
            return;
        }

        GameObject imageGO = new GameObject("baseTerrain");
        imageGO.transform.SetParent(panel.transform, false);

        Image img = imageGO.AddComponent<Image>();
        Sprite sprite = null;
        if (ZIndex == 0) {
            sprite = Resources.Load<Sprite>("baseTerrain1");
        } else if (ZIndex == 1) {
            sprite = Resources.Load<Sprite>("baseTerrain2");
        } else if (ZIndex == 2) {
            sprite = Resources.Load<Sprite>("baseTerrain3");
        } else if (ZIndex == 3) {
            sprite = Resources.Load<Sprite>("baseTerrain4");
        } else if (ZIndex == 4) {
            sprite = Resources.Load<Sprite>("baseTerrain5");
        }
        else {
            Debug.Log("Unexpected error in BaseTerrain");
        }

        img.sprite = sprite;

        RectTransform rt = imageGO.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(16, 16);
        rt.anchoredPosition = new Vector2(XIndex, YIndex);

        // --- Use ZIndex to control render order ---
        imageGO.transform.SetSiblingIndex(ZIndex);
    }

}
