using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "MP/Minesweeper Settings")]
public class MinesweeperSettingsInstaller : ScriptableObjectInstaller<MinesweeperSettingsInstaller>
{
    public Settings MineSweeperSettings;

    public override void InstallBindings()
    {
        // Use IfNotBound to allow overriding for eg. from play mode tests
        Container.BindInstance(MineSweeperSettings).IfNotBound();
    }
    
    [Serializable]
    public class Settings
    {
        public uint MinesCount;

        public uint SizeX;
        public uint SizeY;
    }
}
