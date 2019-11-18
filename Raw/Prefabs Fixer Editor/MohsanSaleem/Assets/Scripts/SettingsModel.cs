using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SettingsModel
{
    public string description;
    public string SpritePath;
    public Vector4 Color;

    [System.NonSerialized] public Sprite _sprite;
    [System.NonSerialized] public Color _color;

 //   [System.NonSerialized] public SettingsModel _history;
 //   [System.NonSerialized] public bool _isPropertyApplied;

    public void LoadAssets()
    {
        _color = Color;
        _sprite = AssetDatabase.LoadAssetAtPath<Sprite>(SpritePath);
    }

    public void SaveAssetsData()
    {
        SpritePath = AssetDatabase.GetAssetPath(_sprite);
        Color = _color;
    }
}

[System.Serializable]
public class Settings
{
    public List<SettingsModel> SettingsList;
}