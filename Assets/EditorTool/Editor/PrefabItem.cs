using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace PG.EditorTool.Editor
{
    public class PrefabItem
    {
        private readonly string _path;
        private readonly GameObject _prefabInstance;

        // Local
        private Text _textComponent;
        private Image _imageComponent;

        public PrefabItem(string path)
        {
            _path = path;
            _prefabInstance = PrefabUtility.LoadPrefabContents(_path);

            _textComponent = _prefabInstance.GetComponent<Text>();

            if (_textComponent == null)
            {
                _textComponent = _prefabInstance.GetComponentInChildren<Text>();
            }
            
            _imageComponent = _prefabInstance.GetComponent<Image>();

            if (_imageComponent == null)
            {
                _imageComponent = _prefabInstance.GetComponentInChildren<Image>();
            }
        }

        public bool NeedsFixing => _textComponent == null || _imageComponent == null;

        public void FixMissing()
        {
            if (_imageComponent == null)
            {
                AddImageComponent();
            }
            // Info: No need to check Text because all the prefabs are filtered based on Text component.
        }
        
        private void AddImageComponent()
        {
            if (_imageComponent == null)
            {
                _imageComponent = _prefabInstance.AddComponent<Image>();

                // If Root has a Text Component we won't be able to Add Image.
                // So adding Image to a Child.
                if (_imageComponent == null)
                {
                    AddImageChild();
                }
            }
            else
            {
                Debug.LogError("Image Component already Exists.");
            }
        }

        private void AddImageChild()
        {
            GameObject addedGameObject = new GameObject("Image", typeof(Image));
            addedGameObject.transform.SetParent(_prefabInstance.transform);

            _imageComponent = addedGameObject.GetComponent<Image>();
        }

        public string Text
        {
            set
            {
                if(_textComponent != null)
                {
                    _textComponent.text = value;
                }   
            }
        }

        public Color Color
        {
            set
            {
                if (_textComponent != null)
                {
                    _textComponent.color = value;
                }
            }
        }

        public Sprite Image
        {
            set
            {
                if (_imageComponent != null)
                {
                    _imageComponent.sprite = value;
                }
            }
        }

        public void ApplySettings()
        {
            Debug.Log("ApplySettings");
            PrefabUtility.SaveAsPrefabAsset(_prefabInstance, _path);
        }

        public void RevertSettings()
        {
            Debug.Log("RevertSettings");
            PrefabUtility.RevertPrefabInstance(_prefabInstance, InteractionMode.AutomatedAction);
        }

        public void DisposePrefabInstance()
        {
            Debug.Log("DisposePrefabInstance");
            PrefabUtility.UnloadPrefabContents(_prefabInstance);
        }
    }
}