using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class PrefabFixerWindowEditor : EditorWindow
{
    private static Settings settingsObj;
    private enum ViewState { NoDisplay, CreateSettingItem, GlobalSetting, Inspection }

    ViewState view_state = ViewState.NoDisplay;
    private const string File_Name = "SettingsFile";

    private static bool s_applyText = true, s_applyColor = true, s_applySprite = true;

    [MenuItem("Tools/PrefabFixer")]
    public static void Init()
    {
        PrefabFixerWindowEditor window = EditorWindow.GetWindow<PrefabFixerWindowEditor>();
        window.titleContent = new GUIContent("Inspector", "Prefab Fixer");
        window.Show();
        LoadSettingFromJson(File_Name);
    }

    #region Save/Load
    private static void LoadSettingFromJson(string fileName)
    {
        var json = Resources.Load(fileName) as TextAsset;
        if (json != null) settingsObj = JsonUtility.FromJson<Settings>(json.text);
        settingsObj.SettingsList.ForEach(itm => itm.LoadAssets());
        Debug.Log(settingsObj.SettingsList.Count);
    }


    private void SaveSettingsAsJson()
    {
        var tobeSaved = new Settings();
        tobeSaved = settingsObj;
        tobeSaved.SettingsList.ForEach(item=>item.SaveAssetsData());
        string path = Application.dataPath + "/Editor/Resources/" + File_Name + ".json";
        Debug.Log(path);
        string json = JsonUtility.ToJson(tobeSaved, true);
        Debug.Log(json);
        File.WriteAllText(path,json);
    }
    #endregion


    #region Unity Methods

    void OnEnable()
    {
        LoadSettingFromJson(File_Name);
    }


    private void OnGUI()
    {
        DisplayMainView();
    }
    #endregion

    void DisplayMainView()
    {
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical(EditorStyles.helpBox);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Global Settings", GUILayout.Width(125)))
        {
            view_state = ViewState.GlobalSetting;
        }
        if (GUILayout.Button("Inspection", GUILayout.Width(100)))
        {
            view_state = ViewState.Inspection;
        }
        GUILayout.EndHorizontal();
        GUILayout.Label("List of Setting Items", EditorStyles.boldLabel);
        ShowList();

        GUILayout.Space(15);

        EditorGUILayout.HelpBox("Save Settings to avoid data lose.", MessageType.Warning);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save Settings", GUILayout.Width(150)))
        {
            SaveSettingsAsJson();
            Debug.Log("saving settings");
        }
        if (GUILayout.Button("Add Setting Entry", GUILayout.Width(150)))
        {
            view_state = ViewState.CreateSettingItem;
            _settingsButtonLable = "Add";
            _selectedItem = new SettingsModel() { description = "here add description" };
        }
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        GUILayout.BeginVertical(EditorStyles.helpBox);
        GUILayout.Label("Detail View Area", EditorStyles.boldLabel, GUILayout.Width(300));

        GUILayout.Space(25);

        switch (view_state)
        {
            case ViewState.NoDisplay:
                break;
            case ViewState.CreateSettingItem:
                DisplaySettingItemView();
                break;
            case ViewState.GlobalSetting:
                DisplayGlobalSettingView();
                break;
            case ViewState.Inspection:
                DisplayInspectionView();
                break;
        }


        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }


    Vector2 scrollCharacters;
    void ShowList()
    {
        //return;
        scrollCharacters = GUILayout.BeginScrollView(scrollCharacters, GUILayout.Width(350));

        for (int iList = 0; iList < settingsObj.SettingsList.Count; iList++)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button(settingsObj.SettingsList[iList].description , GUILayout.Width(200)))
            {
                view_state = ViewState.CreateSettingItem;
                _selectedItem = settingsObj.SettingsList[iList];
                _settingsButtonLable = "Update";
            }
            if (GUILayout.Button("-", GUILayout.Width(25)))
            {
                // delete record
                settingsObj.SettingsList.Remove(settingsObj.SettingsList[iList]);

            }
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();

    }



    SettingsModel _selectedItem = null;
    string _settingsButtonLable = "Update";


    private void DisplaySettingItemView()
    {
        // description
        // color
        // sprite
        GUILayout.BeginVertical();

        _selectedItem.description = EditorGUILayout.TextField("Description ",_selectedItem.description);
        _selectedItem._color = EditorGUILayout.ColorField("Color ", _selectedItem._color);
        _selectedItem._sprite = (Sprite)EditorGUILayout.ObjectField("Sprite ",_selectedItem._sprite, typeof(Sprite));

        GUILayout.EndVertical();
        if (GUILayout.Button(_settingsButtonLable))
        {
            view_state = ViewState.NoDisplay;

            if (_settingsButtonLable == "Add")
            {
                settingsObj.SettingsList.Add(_selectedItem);
            }
        }
    }


    private void DisplayGlobalSettingView()
    {
        // applyText toggle
        // applyColor toggle
        // applySprite toggle
        EditorGUILayout.BeginVertical();
        s_applyText = EditorGUILayout.Toggle("Apply Text ", s_applyText);
        s_applyColor = EditorGUILayout.Toggle("Apply Color ", s_applyColor);
        s_applySprite = EditorGUILayout.Toggle("Apply Sprite ", s_applySprite);
        EditorGUILayout.EndVertical();
    }


    string lookUpFolderPath;
    Object obj = null;
    List<GameItem> prefabs = new List<GameItem>();
    private static bool s_goodToGo = false;


    private void DisplayInspectionView()
    {
        // path to look [text field]
        // search button
        // list of found components [along with fix button]
        EditorGUILayout.BeginVertical();
        obj = EditorGUILayout.ObjectField("Select Folder", obj, typeof(Object));
        GUI.enabled = false;
        lookUpFolderPath = EditorGUILayout.TextField("Folder Path ", AssetDatabase.GetAssetPath(obj));
        GUI.enabled = true;
        if (!string.IsNullOrEmpty(lookUpFolderPath))
        {
            if (GUILayout.Button("Search Prefabs"))
            {
                string path = Application.dataPath + lookUpFolderPath.Substring("Assets".Length);
                var allAssets = AssetDatabase.LoadAllAssetsAtPath(path).ToList();
                Debug.Log(allAssets.Count + "  -- " + Application.dataPath + lookUpFolderPath.Substring("Assets".Length));

                string[] aGameItemsFiles = Directory.GetFiles(path, "*.prefab", SearchOption.AllDirectories);
                foreach (string gameItem in aGameItemsFiles)
                {
                    string assetPath = "Assets" + gameItem.Replace(Application.dataPath, "").Replace('\\', '/');
                    var prefab = (GameItem)AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameItem));
                    prefabs.Add(prefab);
                }

                //prefabs = allAssets.FindAll(gI=>((GameObject)gI).GetComponent<GameItem>() != null).Cast<GameItem>().ToList();
                Debug.Log(prefabs.Count);
               
            }
        }
        if (prefabs != null && prefabs.Count > 0)
        {
            // here display  list of Game items
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            s_goodToGo = true;

            EditorGUILayout.HelpBox("GLOBAL SETTINGS!", MessageType.Info);

            DisplayGlobalSettingView();

            EditorGUILayout.HelpBox("FIX PREFABS TO APPLY SETTINGS!", MessageType.Error);

            EditorGUILayout.BeginVertical();
            
            
            for (int i = 0; i < prefabs.Count; i++)
            {
                DisplayGameItemSlot(prefabs[i], i);
            }

           
            EditorGUILayout.EndVertical();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
           

            GUI.enabled = s_goodToGo;

            //if (GUILayout.Button("Apply Settings"))
            //{

            //}
            GUI.enabled = true;
            
        }
        EditorGUILayout.EndVertical();
       
    }


    List<string> propertiesToChooseFrom = new List<string>();
    string[] objects;

    private void DisplayGameItemSlot(GameItem item, int indexId)
    {
        //
        EditorGUILayout.BeginHorizontal();
        item = (GameItem)EditorGUILayout.ObjectField(item, typeof(GameItem),GUILayout.Width(350));
        bool enabled = true;
        GUI.enabled = enabled;
        if (item.icon == null)
        {
            enabled = false;
            if (GUILayout.Button("Fix Image"))
            {
                fixImage(item, indexId);
                enabled = true;
            }
            s_goodToGo = false;
            GUI.enabled = enabled;
        }
        if (item.description == null)
        {
            enabled = false;
            if (GUILayout.Button("Fix Text"))
            {
                fixText(item, indexId);
                enabled = true;
            }
            s_goodToGo = false;
        }
       
        if (enabled)
        {
            propertiesToChooseFrom.Clear();
            settingsObj.SettingsList.ForEach(ob => propertiesToChooseFrom.Add(ob.description));
            objects = propertiesToChooseFrom.ToArray();
            
            item.selectedProperty = EditorGUILayout.Popup("", item.selectedProperty, objects);

            GUI.enabled = !item._isPropertyApplied;
            if (GUILayout.Button("Apply"))
            {
                var pFab = GameObject.Find(item.name);
                var orignal = pFab == null ? PrefabUtility.InstantiatePrefab(item) : pFab;

                var instiantedGameItem = GameObject.Find(orignal.name).GetComponent<GameItem>();
                prefabs[indexId] = instiantedGameItem;

                instiantedGameItem.SaveHistory();
                instiantedGameItem.ApplySetting(settingsObj.SettingsList[instiantedGameItem.selectedProperty], s_applyText, s_applySprite, s_applyColor);
                EditorUtility.SetDirty(instiantedGameItem);
                try
                {
                    PrefabUtility.ApplyPrefabInstance((GameObject)orignal, InteractionMode.UserAction);
                }
                catch (System.Exception e)
                {

                }
                
            }
            GUI.enabled = item._isPropertyApplied;
            if (GUILayout.Button("Undo"))
            {
                var pFab = GameObject.Find(item.name);
                var orignal = pFab == null ? PrefabUtility.InstantiatePrefab(item) : pFab;

                var instiantedGameItem = GameObject.Find(orignal.name).GetComponent<GameItem>();
                prefabs[indexId] = instiantedGameItem;
                instiantedGameItem.Undo();
                EditorUtility.SetDirty(instiantedGameItem);
                PrefabUtility.ApplyPrefabInstance(instiantedGameItem.gameObject, InteractionMode.UserAction);
            }
            GUI.enabled = true;
        }

        EditorGUILayout.EndHorizontal();
        GUI.enabled = true;
    }


    private void fixText(GameItem item, int index)
    {
        var pFab = GameObject.Find(item.name);
        var orignal = pFab == null ? PrefabUtility.InstantiatePrefab(item) : pFab;
        var instiantedGameItem = GameObject.Find(orignal.name).GetComponent<GameItem>();
        prefabs[index] = instiantedGameItem;
        try
        {
            var text = PrefabUtility.InstantiatePrefab(Resources.Load("description"), instiantedGameItem.transform);
            prefabs[index].description = ((GameObject)text).GetComponent<Text>();
            PrefabUtility.UnpackPrefabInstance((GameObject)text, PrefabUnpackMode.Completely, InteractionMode.UserAction);
            PrefabUtility.ApplyPrefabInstance((GameObject)orignal, InteractionMode.UserAction);
        }
        catch (System.Exception e)
        {

        }
        
      //  PrefabUtility.ConnectGameObjectToPrefab((GameObject)text, item.gameObject);
      //  PrefabUtility.ApplyAddedGameObject((GameObject)text, AssetDatabase.GetAssetPath(item), InteractionMode.UserAction);

        //       PrefabUtility.SavePrefabAsset(item.gameObject);
        //      PrefabUtility.RevertPrefabInstance(item.gameObject, InteractionMode.UserAction);

       // EditorUtility.SetDirty(item);
        AssetDatabase.SaveAssets();

    }


    private void fixImage(GameItem item, int index)
    {
        var orignal = PrefabUtility.InstantiatePrefab(item);
        var instiantedGameItem = GameObject.Find(orignal.name).GetComponent<GameItem>();
        prefabs[index] = instiantedGameItem;
        var image = PrefabUtility.InstantiatePrefab(Resources.Load("icon"), instiantedGameItem.transform);
        try
        {
            prefabs[index].icon = ((GameObject)image).GetComponent<Image>();

            prefabs[index].icon.transform.localPosition = new Vector3(-50, prefabs[index].icon.transform.localPosition.y, prefabs[index].icon.transform.localPosition.z);
            PrefabUtility.UnpackPrefabInstance((GameObject)image, PrefabUnpackMode.Completely, InteractionMode.UserAction);
            PrefabUtility.ApplyPrefabInstance((GameObject)orignal, InteractionMode.UserAction);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
        }
      
        AssetDatabase.SaveAssets();
    }

    private void ApplySettings(GameItem prefab, SettingsModel model)
    {
        prefab.ApplySetting(model,s_applyText, s_applySprite, s_applyColor);

    }
}
