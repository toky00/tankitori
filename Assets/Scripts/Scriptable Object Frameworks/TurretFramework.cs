using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Turret", menuName = "ScriptableObjects/TurretFramework", order = 1)]
public class TurretFramework : ScriptableObject
{
    [Header("Stats")]
    public float armor = 8f;
    public float health = 100f;
    public float rotationSpeed = 2f;
    public float projectileSpeedModifier = 1f;
    public float reloadTime = 5f;
    public float accuracy = 1; // no clue how to calculate it yet
    [Header("FX")]
    public ParticleSystem muzzleFX;
    /*[Header("SFX")]
    public AudioClip shootSFX;
    public AudioClip shockwaveSFX;
    public AudioClip turningLoopSFX;
    public AudioClip reloadingDoneSFX;
    public AudioClip reloadingStartSFX;*/
    [Header("Graphics")]
    public GameObject turretPrefab;
}
