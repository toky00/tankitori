using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Crew", menuName = "ScriptableObjects/CrewFramework", order = 1)]
public class CrewFramework : ScriptableObject
{
    public float health = 100;

    [System.Serializable, ExecuteInEditMode]
    public class Commander
    {
        public float viewDistanceModifier = 1;
    }
    public Commander commander;

    [System.Serializable, ExecuteInEditMode]
    public class Driver
    {
        public float turnSpeedModifier = 1;
        public float speedModifier = 1;
    }
    public Driver driver;

    [System.Serializable, ExecuteInEditMode]
    public class Gunner
    {
        public float accuracyModifier = 1;
        public float turretRotationSpeedModifier = 1;
    }
    public Gunner gunner;

    [System.Serializable, ExecuteInEditMode]
    public class Loader
    {
        public float reloadSpeedModifier = 1;
    }
    public Loader loader;
}
