using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class serilizationTest : MonoBehaviour
{
    string json;
   [ContextMenu("serailze")]
    void Start()
    {
        SettingsModel item = new SettingsModel() { description = "Potion of Mana" , SpritePath =  "/data/filename", Color = new Vector4(1,0,1,1) };

        Settings setin = new Settings();
        setin.SettingsList = new List<SettingsModel>();
        setin.SettingsList.Add(item);
        json = JsonUtility.ToJson(setin);

        Debug.Log(json);
        string path = "Assets/Prefab";
        Debug.Log(path.Substring("Assets".Length));
    }

    [ContextMenu("deseralize")]
    void Update()
    {
        var myObject = JsonUtility.FromJson<SettingsModel>(json);
        Debug.Log(myObject.description);
    }
}
