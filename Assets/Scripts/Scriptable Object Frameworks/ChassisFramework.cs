using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Default Chassis", menuName = "ScriptableObjects/ChassisFramework", order = 1)]
public class ChassisFramework : ScriptableObject
{
    [Header("Armor")]
    public float frontArmor = 10f;
    public float sideArmor = 4f;
    public float backArmor = 2f;

    [Header("Resources")]
    public float maxFuel = 100f;
    public int maxAmmo = 41;

    [Header("Crew")]
    public bool hasDriver = true;
    public bool hasGunner = true;
    public bool hasCommander = true;
    public bool hasLoader = true;
    public enum TransportCrewAmount { NoCrew, OneSquad, TwoSquads };
    public TransportCrewAmount transportCrewAmount = 0;

    [Header("Health Points")]
    public float chassiesHP = 1000f;
    public float engineHP = 10f;
    public float ammoHP = 10f;
    public float fuelHP = 10f;

    [Header("Movement")]
    public float speed = 2f;
    public float turnSpeed = 4f;

    [Header("Tracked")]
    public bool hasTracks = true;
    public float leftTrackHP = 50f;
    public float rightTrackHP = 50f;

    [Header("Wheeled")]
    public bool hasWheels = false;
    public float leftFrontWheelHP = 5f;
    public float rightFrontWheelHP = 5f;
    public float leftBackWheelHP = 5f;
    public float rightBackWheelHP = 5f;

    /*[Header("SFX")]
    public AudioClip idleLoopSFX;
    public AudioClip driveLoopSFX;
    public AudioClip damagedEngineLoopSFX;
    public AudioClip fireLoopSFX;
    public AudioClip gearsGrindingSFX;
    public AudioClip engineStartSFX;
    public AudioClip[] ricochetSFX;
    public AudioClip[] penetrationSFX;
    public AudioClip[] internalDamageSFX;
    public AudioClip[] crewHitSFX;
    public AudioClip[] ammoHitSFX;
    public AudioClip[] fuelHitSFX;
    public AudioClip[] engineHitSFX;
    public AudioClip[] turretHitSFX;*/
    [Header("FX")]
    public ParticleSystem shockwaveFX;
    [Header("Graphics")]
    public Sprite chassisSprite;
    public Color spriteColor;
    public TrailRenderer tiremarksTrail;

}