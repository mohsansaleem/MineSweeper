using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace PM.EditorTool.Editor
{
    public class PrefabsFixerWindow : EditorWindow
    {
        /*private LevelCreator _levelCreator;

        private LevelCreatorValues _levelCreatorValues;
        private LevelsMeta _levelsMeta;
        private DefaultMetaData _defaultMetaData;


        public List<LevelMeta> Levels;*/

        private Action _executeCommandsAction;
        private Action _applyAction;
        private Action _revertAction;
        private Action _disposeCommandAction;


        private string _folderPath = "";
        private List<PrefabChangeCommand> _commands = new List<PrefabChangeCommand>();
        
        public string myString = "Hello World";

        public bool groupEnabled;

        public bool myBool = true;
        public float myFloat = 1.23f;
        
        public static void OpenWindow()
        {
            GetWindow<PrefabsFixerWindow>().Show();
        }

        private void OnEnable()
        {
            ResetCommands();
            
            _folderPath = "";
            // TODO: Set Other values.
            
//            string objectPath = @"Assets/Resources/managers/levelsgeneration/LevelCreatorValues.asset";
//            _levelCreatorValues =
//                AssetDatabase.LoadAssetAtPath(objectPath, typeof(LevelCreatorValues)) as LevelCreatorValues;
//
//            objectPath = @"Assets/Resources/managers/levelsgeneration/LevelsMeta.asset";
//            _levelsMeta =
//                AssetDatabase.LoadAssetAtPath(objectPath, typeof(LevelsMeta)) as LevelsMeta;
//
//            objectPath = @"Assets/Resources/DefaultMetaData.asset";
//            _defaultMetaData =
//                AssetDatabase.LoadAssetAtPath(objectPath, typeof(DefaultMetaData)) as DefaultMetaData;
        }

        public void OnDisable()
        {
            ResetCommands();
        }

        private void ResetCommands()
        {
            if (_commands.Count > 0)
            {
                foreach (PrefabChangeCommand command in _commands)
                {
                    command.Dispose();
                }
                
                _commands.Clear();
            }
        }

        public void LoadScriptableObject()
        {
            groupEnabled = !groupEnabled;
        }

        public void PopulateScriptableObject()
        {
            groupEnabled = !groupEnabled;
        }

//    [TableList]
//    public SomeType[] SomeTableData;

        void OnGUI()
        {
            //base.OnGUI();
/*

        if (GUILayout.Button("Toggle"))
        {
            groupEnabled = !groupEnabled;
        }

        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        EditorGUILayout.Popup("What You Want to Select", 0, new[] {"Yes", "No"});


        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();*/

            GenerationPanel();

            //CopyFirstLevel();
            //SetTutorialData();
            
            //ShowLevels();
        }

        /*public void SetTutorialData()
        {
            GUILayout.BeginHorizontal();

            //NumOfLevels = EditorGUILayout.IntField("Level: ", NumOfLevels);

            if (GUILayout.Button("Set Tutorial Data"))
            {
                string tutorial =
                    @"{""BubblesLifetime"":9,""DarkTouchDamage"":10,""DarkStayDamage"":4,""InAirJumpDamage"":12,""DarkSpeed"":2,""ElementStayDamage"":1,""StartingElementalType"":2,""CameraSize"":12,""StageLength"":106,""WallDistance"":6.2,""TimeStep"":1,""VerticalRangeMin"":6,""VerticalRangeMax"":9,""ForceLimit"":800,""InAirForceLimit"":800,""Hurdles"":[{""HurdleType"":2,""Position"":{""x"":6.2,""y"":39,""z"":0},""Id"":""""},{""HurdleType"":2,""Position"":{""x"":6.2,""y"":47,""z"":0},""Id"":""""},{""HurdleType"":0,""Position"":{""x"":0,""y"":52,""z"":0},""Id"":""""},{""HurdleType"":0,""Position"":{""x"":0,""y"":76,""z"":0},""Id"":""""},{""HurdleType"":2,""Position"":{""x"":6.2,""y"":83,""z"":0},""Id"":""""}],""Allies"":[{""AllyType"":2,""ShapeType"":4,""ElementalType"":5,""Position"":{""x"":0,""y"":0,""z"":0},""Id"":""""},{""ShapeType"":8,""ElementalType"":2,""Position"":{""x"":0.115211964,""y"":7,""z"":0},""Id"":""""},{""ShapeType"":11,""ElementalType"":2,""Position"":{""x"":-1.649131,""y"":14,""z"":0},""Id"":""""},{""ShapeType"":5,""ElementalType"":2,""Position"":{""x"":-2.037887,""y"":20,""z"":0},""Id"":""""},{""ShapeType"":6,""ElementalType"":2,""Position"":{""x"":2.35668087,""y"":27,""z"":0},""Id"":""""},{""ShapeType"":1,""ElementalType"":3,""Position"":{""x"":3.467946,""y"":34,""z"":0},""Id"":""""},{""ShapeType"":3,""ElementalType"":3,""Position"":{""x"":1.82924271,""y"":42,""z"":0},""Id"":""""},{""ShapeType"":0,""ElementalType"":1,""Position"":{""x"":-3.11871719,""y"":49,""z"":0},""Id"":""""},{""ShapeType"":4,""ElementalType"":1,""Position"":{""x"":2.96781588,""y"":55,""z"":0},""Id"":""""},{""ShapeType"":5,""ElementalType"":4,""Position"":{""x"":-3.19554424,""y"":61,""z"":0},""Id"":""""},{""ShapeType"":3,""ElementalType"":4,""Position"":{""x"":1.53339291,""y"":70,""z"":0},""Id"":""""},{""ShapeType"":6,""ElementalType"":2,""Position"":{""x"":-2.794654,""y"":79,""z"":0},""Id"":""""},{""ShapeType"":6,""ElementalType"":2,""Position"":{""x"":-0.221124172,""y"":87,""z"":0},""Id"":""""},{""ShapeType"":7,""ElementalType"":2,""Position"":{""x"":-1.51551723,""y"":93,""z"":0},""Id"":""""},{""ShapeType"":1,""ElementalType"":2,""Position"":{""x"":3.19968867,""y"":102,""z"":0},""Id"":""""}],""Id"":""0"",""ActiveVerticalRange"":18,""ActiveHorizontalRange"":12.4}";
                _defaultMetaData.MetaData.Tutorial = JsonConvert.DeserializeObject<LevelData>(tutorial);

                EditorUtility.SetDirty(_levelsMeta);
                EditorUtility.SetDirty(_defaultMetaData);
                AssetDatabase.SaveAssets();

                AssetDatabase.Refresh();
            }
            
            GUILayout.EndHorizontal();
        }

        private void CopyFirstLevel()
        {
            GUILayout.BeginHorizontal();

            //NumOfLevels = EditorGUILayout.IntField("Level: ", NumOfLevels);

            if (GUILayout.Button("Copy First Level"))
            {
                TextEditor te = new TextEditor();

                if (_defaultMetaData.MetaData.Levels.Count > 0)
                {
                    te.text = JsonConvert.SerializeObject(_defaultMetaData.MetaData.Levels[0], Formatting.Indented);
                    te.SelectAll();
                    te.Copy();
                }
                else
                {
                    Debug.LogError("First Level Not Available");
                }
            }

            GUILayout.EndHorizontal();
        }
        */
        
        private void GenerationPanel()
        {
            

            //NumOfLevels = EditorGUILayout.IntField("Level: ", NumOfLevels);
            GUILayout.BeginVertical();
            
            EditorGUILayout.LabelField("Prefabs Folder", EditorStyles.boldLabel);
            
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Select folder"))
            {
                _folderPath = EditorUtility.OpenFolderPanel("Select folder to Search for Prefabs",  "~/Assets/", "");
                
                _folderPath=  "Assets" + _folderPath.Substring(Application.dataPath.Length);
            }

            EditorGUILayout.TextArea(_folderPath, EditorStyles.largeLabel);

            GUILayout.EndHorizontal();

            if (!string.IsNullOrEmpty(_folderPath))
            {
                if (GUILayout.Button("Search"))
                {
                    Dictionary<string, GameObject> gameObjects = new Dictionary<string, GameObject>();
                    
                    var objects = AssetDatabase.GetAllAssetPaths();

                    if (objects != null)
                    {
                        Debug.LogError(objects.Length);
                        foreach (var o in objects)
                        {
                            if (o.StartsWith(_folderPath) && o.EndsWith(".prefab"))
                            {
                                //Parent
                                //GameObject go = (GameObject)PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath<Object>(o));
                                
                                
                                var go = AssetDatabase.LoadAssetAtPath<GameObject>(o);
                                
                                Debug.LogError(go);

                                if (go.GetComponent<Text>() != null) //|| go.GetComponentInChildren<Text>())
                                {
                                    gameObjects.Add(o, go);
                                    
                                    // TODO: Select these Assets in hierarchy.
                                }
                                else
                                {
                                    Debug.LogError($"{go.name} don't have a text component.");
                                    GameObject.Destroy(go);
                                }
                                
                                //EditorGUIUtility.sele
                                //Selection.gameObjects.SetValue(go, 0);
                                
                                //var cmd = new AddComponentCommand(o, go, typeof(Image));
                                
                                
                                
                                //PrefabUtility.ApplyAddedGameObject(tmp, o, InteractionMode.AutomatedAction);
                                //AssetDatabase.AddObjectToAsset(tmp, go);
                                //PrefabUtility.SavePrefabAsset(go);
                                
                                //PrefabUtility.ApplyAddedGameObject();
                                
                                //EditorUtility.SetDirty(go);
                                //PrefabUtility.RecordPrefabInstancePropertyModifications(go);
                                                                
                                //EditorUtility.ResetToPrefabState(go);
                            }
                        }

                         //gameObjects.ToArray();

                        //AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate );
                        //AssetDatabase.SaveAssets();

                        if (GUILayout.Button("Apply Json"))
                        {
                            foreach (KeyValuePair<string,GameObject> gameObject in gameObjects)
                            {
                                // TODO: Check for missing component and the CheckBox selected in Editor and then
                                // apply those with commands.
                                
                                // New way.
                                var cmd = new AddComponentCommand(gameObject.Key, typeof(Image));
                                _commands.Add(cmd);

                                _executeCommandsAction += cmd.Execute;
                                _applyAction += cmd.Apply;
                                _revertAction += cmd.Revert;
                                _disposeCommandAction += cmd.Dispose;
                                //cmd.Execute();
                                //cmd.Apply();
                                    
                                    
                                Text text = gameObject.Value.GetComponent<Text>();
                                text.text = "Hello2";    
                            }
                            
                            _executeCommandsAction?.Invoke();
                            
                            //if (GUILayout.Button("Apply "))
                        }
                    }
                    else
                    {
                        Debug.LogError("Null");
                    }
                }   
            }

            GUILayout.EndVertical();
        }
        
        /*

        Vector2 scrollPos;

        void ShowLevels()
        {
            GUILayout.Space(10);
            EditorGUILayout.LabelField("Levels Meta", EditorStyles.boldLabel);

            scrollPos = GUILayout.BeginScrollView(scrollPos);

            GUILayout.BeginVertical();

            for (int i = 0; i < _levelsMeta.LevelMetaList.Count; i++)
            {
                var levelMeta = _levelsMeta.LevelMetaList[i];

                GUILayout.BeginVertical();


                EditorGUILayout.LabelField(i + ":", EditorStyles.boldLabel);

                var str = JsonConvert.SerializeObject(levelMeta);
                EditorGUILayout.TextField(str, EditorStyles.miniLabel);

                GUILayout.EndVertical();

                GUILayout.Space(5);
            }

            GUILayout.EndVertical();

            GUILayout.EndScrollView();
        }*/
    }

    public abstract class PrefabChangeCommand
    {
        private readonly string _path;
        //private readonly GameObject _asset;
        protected readonly GameObject TmpGameObject;
        

        protected PrefabChangeCommand(string path)//, GameObject asset)
        {
            _path = path;
            //_asset = asset;
            TmpGameObject = PrefabUtility.LoadPrefabContents(_path);
        }
        
        public abstract void Execute();
        
        public virtual void Apply()
        {
            Debug.LogError("Apply");
            //PrefabUtility.ApplyPrefabInstance(_tmpGameObject, InteractionMode.AutomatedAction);
            
            // New way.
            PrefabUtility.SaveAsPrefabAsset(TmpGameObject, _path);
        }

        public virtual void Revert()
        {
            Debug.LogError("Revert");
            //PrefabUtility.RevertPrefabInstance();
            
            // New way
            PrefabUtility.RevertPrefabInstance(TmpGameObject, InteractionMode.AutomatedAction);
        }

        public virtual void Dispose()
        {
            Debug.LogError("Dispose");
            //GameObject.DestroyImmediate(_tmpGameObject);
            
            // New way
            PrefabUtility.UnloadPrefabContents(TmpGameObject);
        }
    }

    public class AddComponentCommand : PrefabChangeCommand
    {
        private Type _type;
        private GameObject _addedGameObject;

        //public AddComponentCommand(string path, GameObject asset, Type type) : base(path, asset)
        public AddComponentCommand(string path, Type type) : base(path)
        {
            _type = type;
        }

        public override void Execute()
        {
            _addedGameObject = new GameObject("Image", _type);
            _addedGameObject.transform.SetParent(TmpGameObject.transform);   
        }
    }
}