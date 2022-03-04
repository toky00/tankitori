using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBaseOLD : MonoBehaviour
{
    [System.Serializable]
    public class MainStats
    {
        public bool isPlayer = true;
        public bool isDead = false;
        [System.Serializable]
        public class FactionRelations
        {
            public string allignedFaction = "Ravenbane";
            public List<string> factionNames = new List<string>() { "Ravenbane", "IronStriders", "Independent", "Civilian" };
            public List<int> factionRelations = new List<int>() { 100, 0, 50, 50 };
        }
        public FactionRelations FactionSettings;
    }
    public MainStats Main;
    [System.Serializable]
    public class MovementStats //MAKE ARMOR, MOVEMENT AND CREW A SCRIPTABLE OBJECT
    {
        public float speed = 2f;
        public float turnSpeed = 4f;
        public bool stuckInMud = false;
    }
    public MovementStats Movement; //MAKE ARMOR, MOVEMENT AND CREW A SCRIPTABLE OBJECT
    [System.Serializable]
    public class ArmorStats //MAKE ARMOR, MOVEMENT AND CREW A SCRIPTABLE OBJECT
    {
        public float frontArmor = 10f;
        public float sideArmor = 4f;
        public float backArmor = 2f;
        [Tooltip("DO NOT MODIFY!")]public float turretArmor = 8f;
    }
    public ArmorStats Armor; //MAKE ARMOR, MOVEMENT AND CREW A SCRIPTABLE OBJECT
    [System.Serializable]
    public class VisualFX
    {
        public ParticleSystem shootFX;
        public ParticleSystem dirtFX;
    }
    public VisualFX VFX;
    [System.Serializable]
    public class ComponentsStats {
        public GameObject turret;
        public GameObject chassies;
        public GameObject shell;
        public bool fuelExploded = false;
        public bool ammoExploded = false;
        public bool engineDisabled = false;
        public bool turretDisabled = false;
        public bool leftTrackDisabled = false;
        public bool righTrackDisabled = false;
    }
    public ComponentsStats Components;
    [System.Serializable]
    public class CrewStats //MAKE ARMOR, MOVEMENT AND CREW A SCRIPTABLE OBJECT
    {
        public bool driverDead = false;
        public float driverHP = 100f;
        public bool gunnerDead = false;
        public float gunnerHP = 100f;
        public bool loaderDead = false;
        public float loaderHP = 100f;
    }
    public CrewStats Crew; //MAKE ARMOR, MOVEMENT AND CREW A SCRIPTABLE OBJECT
    [System.Serializable]
    public class ChanceToHitStats //MAKE ARMOR, MOVEMENT AND CREW A SCRIPTABLE OBJECT 
    {
        public float cthDriver;
        public float cthGunner;
        public float cthLoader;
        public float cthFuel;
        public float cthAmmo;
        public float cthEngine;
        public float cthTurret;
        public float cthTrackLeft;
        public float cthTrackRight;
    }
    public ChanceToHitStats ChanceToHit; //MAKE ARMOR, MOVEMENT AND CREW A SCRIPTABLE OBJECT
    [Header("Debug")]
    private float cSpeed = 2f;
    private float cTurnSpeed = 4f;
    private float cDriverHP = 100f;
    private float cGunnerHP = 100f;
    private float cLoaderHP = 100f;
    private float turretHP = 100f;
    private float chassiesHP = 1000f;
    private float engineHP = 100f;
    private float ammoHP = 10f;
    private float fuelHP = 10f;
    private TurretBase turret;
    [HideInInspector] public Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        turret = Components.turret.gameObject.GetComponent<TurretBase>();
        Armor.turretArmor = turret.armorValue;
    }

    private void FixedUpdate()
    {
        if (Main.isPlayer)
            MovementUpdates();
    }

    void MovementUpdates()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f)
        {
            rb.AddTorque(-Input.GetAxis("Horizontal") / Movement.turnSpeed);
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f)
        {
            rb.AddForce((rb.transform.right * Input.GetAxis("Vertical")) * Movement.speed, ForceMode2D.Force);
        }
    }
}
