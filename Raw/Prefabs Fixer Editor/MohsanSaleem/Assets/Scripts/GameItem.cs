using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameItem : MonoBehaviour
{
    public Text description;
    public Image icon;
    public Color color;

    private SettingsModel _history;
    public bool _isPropertyApplied;
    public int selectedProperty;
    bool _isApplyText, _isApplyImage, _isApplyColor;


    public void ApplySetting(SettingsModel model, bool isApplyText, bool isApplyImage, bool isApplyColor)
    {
        _isApplyText = isApplyText;
        _isApplyImage = isApplyImage;
        _isApplyColor = isApplyColor;
        if (isApplyText) description.text = model.description;
        if(isApplyImage) icon.sprite = model._sprite;
        if(isApplyColor) icon.color = model._color;

        _isPropertyApplied = true;
    }



    public void Undo()
    {
        ApplySetting(_history, _isApplyText, _isApplyImage, _isApplyColor);
        _isPropertyApplied = false;
    }



    public void SaveHistory()
    {
        _history = GetSettingProperties();
    }



    public SettingsModel GetHistory()
    {
        return _history;
    }



    private SettingsModel GetSettingProperties()
    {
        return new SettingsModel()
        {
            description = this.description?.text,
            _color = this.color,
            _sprite = icon?.sprite
        };
    }
}
