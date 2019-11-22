using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Server.API;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace PM.EditorTool.Editor
{
    public class PrefabsFixerWindow : EditorWindow
    {
        private EToolState _eToolState;

        private Action _fixAction;
        private Action _applyAction;
        private Action _revertAction;
        private Action _disposeAction;

        // Locals
        private bool _jsonApplied;
        private bool _fixingButton;
        private bool _fixedPrefabs;

        private string _folderPath = "";
        
        private List<PrefabItem> _prefabItems = new List<PrefabItem>();
        private Dictionary<string, GameObject> gameObjects;

        // CheckList
        private bool _applyText;
        private bool _applyColor;
        private bool _applyImage;

        private const string ResourcesFolderPath = "/1_EditorTool/Resources/";
        private const string JsonSettingsFilePath = "data.json";

        public static void OpenWindow()
        {
            GetWindow<PrefabsFixerWindow>().Show();
        }

        //-------------------------------------------------------------------------------------
        // Keeping everything in this Magic function to be safe from Function frames on Stack.
        //-------------------------------------------------------------------------------------
        void OnGUI()
        {
            GUILayout.BeginHorizontal();

            GUILayout.BeginVertical(EditorStyles.helpBox);
            GUILayout.Label("Detail View Area", EditorStyles.boldLabel, GUILayout.Width(300));

            GUILayout.Space(5);

            EditorGUILayout.BeginVertical();

            //--------------------------------
            // Folder Selection.
            //--------------------------------
            if (GUILayout.Button("Select folder", GUILayout.Width(100)))
            {
                Reset();

                _folderPath = EditorUtility.OpenFolderPanel("Select folder to Search for Prefabs",
                    "~/Assets/", "*.Prefab");

                if (!string.IsNullOrEmpty(_folderPath))
                {
                    _folderPath = "Assets" + _folderPath.Substring(Application.dataPath.Length);

                    //--------------------------------
                    // Creating Lists to Operate On.
                    //--------------------------------
                    PopulateGameObjects();
                    CreatePrefabInstances();

                    _eToolState = EToolState.FolderSelected;
                }
            }

            GUI.enabled = false;
            EditorGUILayout.TextField("Folder Path ", _folderPath);
            GUI.enabled = true;


            //--------------------------------
            // Prefabs Options.
            //--------------------------------
            if (_prefabItems.Count > 0)
            {
                //--------------------------------
                // Showing the Setting Options.
                //--------------------------------
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                EditorGUILayout.HelpBox("Choose the Setting to apply!", MessageType.Info);

                EditorGUILayout.BeginHorizontal();
                _applyText = EditorGUILayout.Toggle("Apply Text ", _applyText);
                _applyColor = EditorGUILayout.Toggle("Apply Color ", _applyColor);
                _applyImage = EditorGUILayout.Toggle("Apply Sprite ", _applyImage);
                EditorGUILayout.EndHorizontal();

                //--------------------------------
                // Apply Selected Settings Button.
                //--------------------------------
                if (_applyText || _applyColor || _applyImage)
                {
                    if (GUILayout.Button("Apply", GUILayout.Width(100)))
                    {
                        ApplyJsonSettingsInSequence();
                    }
                }
                
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                //------------------------------------------
                // Fix the Configurations of the Prefabs.
                //------------------------------------------
                if (_fixingButton)
                {
                    EditorGUILayout.HelpBox("FIX PREFABS TO APPLY SETTINGS! \n" +
                                            "If you don't fix the Component then the respective setting won't be applied.",
                        MessageType.Error);

                    if (GUILayout.Button("Fix Configurations", GUILayout.Width(120)))
                    {
                        _fixAction?.Invoke();

                        _fixingButton = false;
                        _fixedPrefabs = true;

                        _eToolState = EToolState.PrefabsFixed;
                    }
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                
                //--------------------------------
                // Showing the Applying options.
                //--------------------------------
                if (_jsonApplied || _fixedPrefabs)
                {
                    GUILayout.BeginHorizontal();
                    
                    if (GUILayout.Button("Apply", GUILayout.Width(100)))
                    {
                        ApplyEverything();
                    }
                    
                    if (GUILayout.Button("Revert", GUILayout.Width(100)))
                    {
                        RevertEverything();
                    }
                    GUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndVertical();

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
        }

        private void RevertEverything()
        {
            _revertAction?.Invoke();
            Reset();
        }

        private void ApplyEverything()
        {
            _applyAction?.Invoke();
            Reset();
        }

        private void ApplyJsonSettingsInSequence()
        {
            try
            {
                string path = Application.dataPath + ResourcesFolderPath + JsonSettingsFilePath;

                StreamReader reader = new StreamReader(path);
                string data = reader.ReadToEnd();
                reader.Close();
                
                if (!string.IsNullOrEmpty(data))
                {
                    List<PrefabSettings> settings = JsonConvert.DeserializeObject<List<PrefabSettings>>(data);

                    for (int i = 0; i < settings.Count; i++)
                    {
                        if (_applyText && i < _prefabItems.Count)
                        {
                            _prefabItems[i].Text = settings[i].text;
                        }
                        
                        if (_applyColor && i < _prefabItems.Count)
                        {
                            _prefabItems[i].Color = settings[i].GetColor();
                        }
                        
                        if (_applyImage && i < _prefabItems.Count)
                        {
                            path = "Assets" + ResourcesFolderPath + settings[i].image;

                            _prefabItems[i].Image = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                        }
                    }
                }
                else
                {
                    Debug.LogError("Json doesn't exist.");
                }
                
                _jsonApplied = true;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
            
            
            // TODO: Apply Json Settings.
        }

        void PopulateGameObjects()
        {
            gameObjects = new Dictionary<string, GameObject>();
            string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();

            foreach (var o in allAssetPaths)
            {
                if (o.StartsWith(_folderPath) && o.EndsWith(".prefab"))
                {
                    var go = AssetDatabase.LoadAssetAtPath<GameObject>(o);

                    // TODO: Use meta files for Component detection. Loading Assets and components getter are costly.
                    // For now we are just check the root for Text Component. As mentioned in task we just 
                    // select prefabs which have TextComponent.
                    if (go.GetComponent<Text>() != null || go.GetComponentInChildren<Text>() != null)
                    {
                        gameObjects.Add(o, go);
                    }
                }
            }

            Selection.objects = gameObjects.Values.ToArray();
        }

        void CreatePrefabInstances()
        {
            foreach (KeyValuePair<string, GameObject> gameObject in gameObjects)
            {
                // TODO: Check for missing component and the CheckBox selected in Editor and then
                // apply those with commands.

                // New way.
                var cmd = new PrefabItem(gameObject.Key);
                _prefabItems.Add(cmd);

                if (cmd.NeedsFixing)
                {
                    _fixAction += cmd.FixMissing;
                    _fixingButton = true;
                }

                _applyAction += cmd.ApplySettings;
                _revertAction += cmd.RevertSettings;
                _disposeAction += cmd.DisposePrefabInstance;
            }
        }

        private void Reset()
        {
            _disposeAction?.Invoke();
            
            _prefabItems.Clear();

            _folderPath = "";
            
            gameObjects?.Clear();
            gameObjects = null;

            _eToolState = EToolState.Start;

            _jsonApplied = false;
            _fixingButton = false;
            _fixedPrefabs = false;

            _applyText = true;
            _applyColor = true;
            _applyImage = true;

            _fixAction = null;
            _applyAction = null;
            _revertAction = null;
            _disposeAction = null;
        }
        
        private void OnEnable()
        {
            Reset();
        }

        public void OnDisable()
        {
            Reset();
        }
    }


    public enum EToolState
    {
        Start = 0,
        FolderSelected,
        PrefabsFixed,
        JsonApplied
    }

    [Serializable]
    public class Settings
    {
        public List<PrefabSettings> PrefabsSettings;
    }
    
    [Serializable]
    public class PrefabSettings
    {
        public string text;
        public string image;
        public string color;

        public Color GetColor()
        {
            Color c;
            ColorUtility.TryParseHtmlString(color, out c);
            
            return c;
    }
    }
}