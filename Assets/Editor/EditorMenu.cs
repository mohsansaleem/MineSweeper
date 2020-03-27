using UnityEngine;
using UnityEditor;

using JetBrains.Annotations;
using UnityEditor.SceneManagement;

namespace PG.Editor
{
    public class EditorMenu
    {
        [MenuItem("Project/Editor Tool &1", false, 200)]
        static void OpenEditorTool()
        {
            EditorTool.Editor.PrefabsFixerWindow.OpenWindow();
        }
        
        [MenuItem("Project/Roulette &2", false, 300)]
        static void OpenRoulette()
        {
            OpenScene("Assets/Roulette/Roulette.unity");
        }
        
        [MenuItem("Project/Minesweeper &3", false, 400)]
        static void OpenMinesweeper()
        {
            OpenScene("Assets/Minesweeper/Minesweeper.unity");
        }

        private static void OpenScene(string scene)
        {
            EditorSceneManager.OpenScene(scene);
        }
    }
}