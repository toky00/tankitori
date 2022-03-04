using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Shell", menuName = "ScriptableObjects/ShellFramework", order = 1)]
public class ShellFramework : ScriptableObject
{
    [Header("Stats")]
    public float weight = 0.1f;
    public float propulsion = 10f; // THINK ABOUT PROPULSION DURATION
    public float ricochetChance = 15f;
    [Header("Damage")]
    public float kineticDamage = 6f;
    public float innerExplosiveDamage = 2f;
    public float outerExplosiveDamage = 2f;
    public float rawDamage = 10f;
    public float innerExplosiveRadius = 1f;
    public float outerExplosiveRadius = 2f;
    [Header("Modifiers")]
    public float apMod = 1.15f;
    public float recoilMod = 1f;
    /*[Header("FX")]
    public ParticleSystem propulsionFX;
    public ParticleSystem ricochetFX;
    public ParticleSystem penetrationFX;
    public ParticleSystem explosionFX;
    [Header("SFX")]
    public AudioClip ricochetSFX;
    public AudioClip flybyLoopSFX;
    public AudioClip explosionSFX;
    public AudioClip penetrationSFX;*/
    [Header("Graphics")]
    public GameObject shellPrefab;
    [Header("Debug")]
    public static float maxSpeed = 140f;
}
