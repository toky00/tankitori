using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellFunctionality : MonoBehaviour
{
    [Header("Collision Layers")]
    public LayerMask environment;
    public LayerMask destructable;

    [Header("Data")]
    public ShellFramework shellData; // assign this from the turret once instantiated

    [Header("Debug")]
    Rigidbody2D rig;
    int bounceCount = 0;
    float curVel = 0;
    Vector3 curVelVec;
    RaycastHit2D hitStored;

    void OnEnable() // once spawned assign all the values from the framework here
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) // on trigger get velocity
    {
        curVel = rig.velocity.magnitude;
    }

    private void OnCollisionEnter2D(Collision2D collision) // on collision raycast and check for ricochet
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.8f, environment + destructable);
        if (hit && bounceCount <= 2 && curVel >= 2f && hit.collider.gameObject != gameObject)
        {
            hitStored = hit;
            CheckRicochetChance();
        }
    }

    private void FixedUpdate()
    {
        SpeedLimitShell();
        CheckIfStopped();
    }

    private void SpeedLimitShell()
    {
        curVelVec = rig.velocity;
        curVelVec = new Vector3(Mathf.Clamp(rig.velocity.x, -100f, 100f), Mathf.Clamp(rig.velocity.y, -100f, 100f), 0);
        rig.velocity = curVelVec;
    }

    private void CheckIfStopped()
    {
        if (rig.velocity.magnitude <= 2)
        {
            Collider2D col = GetComponent<Collider2D>();
            col.enabled = false;
            Destroy(gameObject, 5f);
        }
    }

    private void CheckRicochetChance()
    {
        Debug.Log("Check Ricochet.");
        int chance = Random.Range(0, 100);
        float finalChance = (Vector2.Angle(transform.up, hitStored.normal) > 145) ? shellData.ricochetChance : shellData.ricochetChance * shellData.ricochetOver45Modifier;
        if (chance > finalChance)
        {
            TriggerProjectile(); // if not ricochet, calculate penetration //// deal damage to internals if penetration and chassis with correct sounds ( not done in here ) // deal damage to chassis and explode shell outside with correct fx and sounds ( not done in here ) 
        }
        else
        {
            Ricochet();
        }
    }

    private void TriggerProjectile()
    {
        Debug.Log("Trigger projectile.");
        ParticleSystem ps = Instantiate(shellData.explosionFX, gameObject.transform.position, Quaternion.LookRotation((Vector2)transform.up));
        HitboxFramework _hf = hitStored.collider.gameObject.GetComponent<HitboxFramework>();
        if (_hf != null)
        {
            MasterEntityBase _mb = _hf._mb;
            Debug.Log(shellData.kineticDamage * rig.velocity.magnitude / 2);
            _mb.CalculateDamageInitial(shellData.apMod * rig.velocity.magnitude, hitStored.collider.gameObject.name, shellData.kineticDamage*rig.velocity.magnitude/2,
                shellData.innerExplosiveDamage + shellData.outerExplosiveDamage, _hf.armor, shellData.rawDamage);
            // play explosion sound, instantiate it
        }
        Destroy(ps, 5f);
        Destroy(gameObject);
    }

    private void Ricochet()
    {
        bounceCount++;
        Debug.Log("Ricochet!");
        // play ricochet vfx and sound
    }
}
