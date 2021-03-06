using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterEntityBase : MonoBehaviour
{
    [Header("Core Stats")]
    public bool isPlayer;
    public string alliedFaction = "Ravenbane";
    public List<List<string>> componentPositions;
    [System.Serializable]
    public class ObjectReferences
    {
        [Header("Turret References")]
        public bool hasTurret = true;
        [ConditionalHide("hasTurret", true)] public GameObject turretObject;
        [ConditionalHide("hasTurret", true)] public TurretFramework turretData;
        [ConditionalHide("hasTurret", true)] public GameObject turretAimPoint;
        [ConditionalHide("hasTurret", true)] public TurretControl turretControl;
        [Header("Chassis References")]
        public GameObject chassisObject;
        public ChassisFramework chassisData;
        [Header("Crew References")]
        public CrewFramework crewData;
        [Header("Ammo References")]
        public ShellFramework[] shellData;
        [Header("Hitbox References")]
        public GameObject[] hitboxes;
        [Header("Core")]
        public Rigidbody2D entityRigidbody;
        [Header("Graphics")]
        public TrailRenderer[] trackOrTireMarks; // cus i can
    }
    public ObjectReferences objectReferences;

    [System.Serializable]
    public class Crew
    {
        public float commanderHealth;
        public float gunnerHealth;
        public float loaderHealth;
        public float driverHealth;
    }

    [Header("Dynamic Stats")]
    public Crew crewStats;
    public float chassisHealth;
    public float turretHealth;
    public float engineHealth;
    public float ammoHealth;
    public float fuelHealth;
    public float leftTrackHealth;
    public float rightTrackHealth;
    public float leftFrontWheelHealth;
    public float rightFrontWheelHealth;
    public float leftBackWheelHealth;
    public float rightBackWheelHealth;
    [ConditionalHide("objectReferences.hasTurret", true)] public float currentTurretRotationSpeed;
    [ConditionalHide("objectReferences.hasTurret", true)] public float currentTurretAccuracy;

    [Header("Dynamic Resources")]
    public float maxFuel;
    public float maxAmmo;

    [Header("Dynamic States")]
    public int currentLoadedShell;
    public bool isStuck;
    public bool isDestroyed;

    [Header("Dynamic Movement")]
    public float currentTopSpeed;
    public float currentTopTurnSpeed;

    public void CalculateDamageInitial(float apValue, string hitboxName, float kineticDamage, float explosiveDamageTotal, float armorOfHitbox, float rawDamage)
    {
        int counterIdexOfHitbox = 0;
        foreach (GameObject hitbox in objectReferences.hitboxes)
        {
            if(hitbox.name == hitboxName)
            {
                bool hasPenetrated = (armorOfHitbox < apValue) ? true : false; // perfomance determines lines and characters really, so the more you have of both, yes, cus binary
                int maxComponentCrewDamage = Random.Range(2, 3); //MAX AMOUNT OF COMPONENTS AND CREW TO DAMAGE

                chassisHealth -= rawDamage;

                if (hasPenetrated)
                {
                    int countComponentDamage = 0;
                    foreach (string component in componentPositions[counterIdexOfHitbox])
                    {
                        int rando = Random.Range(1, 100);
                        if (rando >= 50 && component != null)
                        {
                            countComponentDamage++;
                            Debug.Log("Penetration into "+component);
                            if (countComponentDamage == maxComponentCrewDamage)
                            {
                                //less damage
                                CalculateDamageFinal(component, kineticDamage / 2, explosiveDamageTotal / 2, apValue - armorOfHitbox);
                            }
                            else if (countComponentDamage <= maxComponentCrewDamage-1)
                            {
                                CalculateDamageFinal(component, kineticDamage, explosiveDamageTotal, apValue - armorOfHitbox);
                            }
                        }
                        if(countComponentDamage >= maxComponentCrewDamage)
                            break;
                    }
                }
                else
                {
                    Debug.Log("No penetration!");
                    if (componentPositions[counterIdexOfHitbox][0].Contains("Track") || componentPositions[counterIdexOfHitbox][0].Contains("Wheel"))
                        CalculateDamageFinal(componentPositions[counterIdexOfHitbox][0], kineticDamage, explosiveDamageTotal, apValue - armorOfHitbox); // zero is track or wheel
                }
                break;
            }
            counterIdexOfHitbox++;
        }
    }

    void CalculateDamageFinal(string name, float kineticDamage, float explosiveDamageTotal, float apValue)
    {
        if (name == "Ammo")
            ammoHealth -= kineticDamage / 2 + explosiveDamageTotal;
        else if (name == "Engine")
            engineHealth -= kineticDamage + explosiveDamageTotal / 3;
        else if (name == "Fuel")
            fuelHealth -= kineticDamage / 2 + explosiveDamageTotal;
        else if (name == "Left Track")
            leftTrackHealth -= kineticDamage / 2 + explosiveDamageTotal;
        else if (name == "Right Track")
            rightTrackHealth -= kineticDamage / 2 + explosiveDamageTotal;
        else if (name == "Turret")
        {
            if (apValue > objectReferences.turretData.turretArmor)
                turretHealth -= kineticDamage + explosiveDamageTotal / 3;
        }
        else if (name == "Gunner")
            crewStats.gunnerHealth -= kineticDamage / 3 + explosiveDamageTotal;
        else if (name == "Driver")
            crewStats.driverHealth -= kineticDamage / 3 + explosiveDamageTotal;
        else if (name == "Loader")
            crewStats.loaderHealth -= kineticDamage / 3 + explosiveDamageTotal;
        else if (name == "Left Front Wheel")
            leftFrontWheelHealth -= kineticDamage + explosiveDamageTotal;
        else if (name == "Right Front Wheel")
            rightFrontWheelHealth -= kineticDamage + explosiveDamageTotal;
        else if (name == "Left Back Wheel")
            leftBackWheelHealth -= kineticDamage + explosiveDamageTotal;
        else if (name == "Right Back Wheel")
            rightBackWheelHealth -= kineticDamage + explosiveDamageTotal;
    }

    private void OnEnable()
    {
        #region Crew
        //CREW INITIALIZATION
        if (objectReferences.chassisData.hasCommander)
            crewStats.commanderHealth = objectReferences.crewData.health;
        if (objectReferences.chassisData.hasGunner)
            crewStats.gunnerHealth = objectReferences.crewData.health;
        if (objectReferences.chassisData.hasLoader)
            crewStats.loaderHealth = objectReferences.crewData.health;
        if (objectReferences.chassisData.hasDriver)
            crewStats.driverHealth = objectReferences.crewData.health;
        //CREW INITIALIZATION
        #endregion

        #region Components
        //COMPONENT INITIALIZATION
        if (objectReferences.hasTurret)
            turretHealth = objectReferences.turretData.turretHP;
        chassisHealth = objectReferences.chassisData.chassiesHP;
        engineHealth = objectReferences.chassisData.engineHP;
        ammoHealth = objectReferences.chassisData.ammoHP;
        fuelHealth = objectReferences.chassisData.fuelHP;
        //CREW INITIALIZATION
        #endregion

        #region Resources
        //RESOURCES INITIALIZATION
        maxAmmo = objectReferences.chassisData.maxAmmo;
        maxFuel = objectReferences.chassisData.maxFuel;
        //RESOURCES INITIALIZATION
        #endregion

        #region Movement
        //MOVEMENT INITIALIZATION
        currentTopSpeed = objectReferences.chassisData.speed;
        currentTopTurnSpeed = objectReferences.chassisData.turnSpeed;
        if (objectReferences.chassisData.hasTracks) {
            leftTrackHealth = objectReferences.chassisData.leftTrackHP;
            rightTrackHealth = objectReferences.chassisData.rightTrackHP;
        } else if (objectReferences.chassisData.hasWheels) {
            leftFrontWheelHealth = objectReferences.chassisData.leftFrontWheelHP;
            rightFrontWheelHealth = objectReferences.chassisData.rightFrontWheelHP;
            leftBackWheelHealth = objectReferences.chassisData.leftBackWheelHP;
            rightBackWheelHealth = objectReferences.chassisData.rightBackWheelHP;
        }
        //MOVEMENT INITIALIZATION
        #endregion

        #region Armor
        //ARMOR INTIALIZATION
        foreach (GameObject hitbox in objectReferences.hitboxes)
        {
            HitboxFramework hf = hitbox.GetComponent<HitboxFramework>(); // temp variable that gets the HItboxFramework
            if(hitbox.name.StartsWith("F."))
            {
                hf.armor = objectReferences.chassisData.frontArmor; // the HitboxFramework has an armor value which is 0 at the start, to make it work with the modular shit, we call the armor variable and assign the armor value from the scriptable object
            }
            else if(hitbox.name.StartsWith("B."))
            {
                hf.armor = objectReferences.chassisData.backArmor;
            }
            else if (hitbox.name.StartsWith("BR") || 
                     hitbox.name.StartsWith("MR") ||
                     hitbox.name.StartsWith("FR") ||
                     hitbox.name.StartsWith("BL") ||
                     hitbox.name.StartsWith("ML") ||
                     hitbox.name.StartsWith("FL"))
            {
                hf.armor = objectReferences.chassisData.sideArmor;
            }
        }
        //ARMOR INTIALIZATION
        #endregion
        //yes tru na
        #region Component Placement
        componentPositions = new List<List<string>>() 
        { 
            objectReferences.chassisData.componentPositions.leftFront,
            objectReferences.chassisData.componentPositions.front,
            objectReferences.chassisData.componentPositions.rightFront,
            objectReferences.chassisData.componentPositions.leftMiddle,
            objectReferences.chassisData.componentPositions.rightMiddle,
            objectReferences.chassisData.componentPositions.leftBack,
            objectReferences.chassisData.componentPositions.back,
            objectReferences.chassisData.componentPositions.rightBack
        };
        #endregion
    }

    private void FixedUpdate()
    {
        if (isPlayer && !isDestroyed)
            MovementUpdates();
    }

    void MovementUpdates()
    {
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.01f)
        {
            objectReferences.entityRigidbody.AddTorque(-Input.GetAxis("Horizontal") * currentTopTurnSpeed);
        }

        if (Mathf.Abs(Input.GetAxis("Vertical")) > 0.01f)
        {
            objectReferences.entityRigidbody.AddForce((objectReferences.entityRigidbody.transform.right * Input.GetAxis("Vertical")) * currentTopSpeed, ForceMode2D.Force);
        }
    }
}
