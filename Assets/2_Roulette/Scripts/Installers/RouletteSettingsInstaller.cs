using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[CreateAssetMenu(menuName = "MP/Roulette Settings")]
public class RouletteSettingsInstaller : ScriptableObjectInstaller<RouletteSettingsInstaller>
{
    public Settings RouletteSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(RouletteSettings).IfNotBound();
    }
    
    [Serializable]
    public class Settings
    {
        [Tooltip("Time for spinning in Seconds.")]
        public float SpinTime;
        
        [Tooltip("Time for showing the result in Seconds.")]
        public float ResultVisibilityTime;
        
        [Tooltip("Roulette Speed")]
        public float RouletteSpeed;

        [Tooltip("Speed at which Roullete stop.")]
        public float MinSpeed;

        [Tooltip("Resistence when slowing.")]
        public float Resistence;

        public List<int> Multipliers;
    }
}
