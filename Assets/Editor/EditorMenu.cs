using UnityEngine;
using UnityEditor;

using JetBrains.Annotations;
using UnityEditor.SceneManagement;

namespace PM.Editor
{
    public class EditorMenu
    {
        [MenuItem("PM Test/Editor Tool &1", false, 200)]
        static void OpenEditorTool()
        {
            EditorTool.Editor.PrefabsFixerWindow.OpenWindow();
        }
        
        [MenuItem("PM Test/Roulette &2", false, 300)]
        static void OpenRoulette()
        {
            OpenScene("Assets/2_Roulette/Roulette.unity");
        }
        
        [MenuItem("PM Test/Minesweeper &3", false, 400)]
        static void OpenMinesweeper()
        {
            OpenScene("Assets/3_Minesweeper/Minesweeper.unity");
        }

        private static void OpenScene(string scene)
        {
            EditorSceneManager.OpenScene(scene);
        }
    }
}