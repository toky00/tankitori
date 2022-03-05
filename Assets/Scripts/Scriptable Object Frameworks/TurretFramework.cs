using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Turret", menuName = "ScriptableObjects/TurretFramework", order = 1)]
public class TurretFramework : ScriptableObject
{
    [Header("Stats")]
    public float turretArmor = 8f;
    public float turretHP = 100f;
    public float turretRotationSpeed = 2f;
    public float turretProjectileSpeedModifier = 1f;
    public float turretReloadTime = 5f;
    public float turretAccuracy = 1.5f; // the higher it is the more accurate it will be
    [Header("FX")]
    public ParticleSystem turretMuzzleFX;
    /*[Header("SFX")]
    public AudioClip shootSFX;
    public AudioClip shockwaveSFX;
    public AudioClip turningLoopSFX;
    public AudioClip reloadingDoneSFX;
    public AudioClip reloadingStartSFX;*/
    [Header("Graphics")]
    public Sprite turretSprite;
    public Color spriteColor;
}
