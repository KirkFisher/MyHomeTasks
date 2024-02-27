using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class GroundItem : MonoBehaviour, ISerializationCallbackReceiver
{
    public ItemsObject item;

    public void OnAfterDeserialize()
    {

    }

    /*public void OnBeforeSerialize()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = item.uiDisplay;
        EditorUtility.SetDirty(GetComponentInChildren<SpriteRenderer>());
    }*/
    public void OnBeforeSerialize()
    {
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (spriteRenderer != null && item != null)
        {
            spriteRenderer.sprite = item.uiDisplay;
        }
    }
}
