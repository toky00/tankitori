using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellFunctionality : MonoBehaviour
{
    [Header("Collision Layers")]
    public LayerMask environment;
    public LayerMask destructable;

    [Header("Data")]
    [HideInInspector]public ShellFramework shellData; // assign this from the turret once instantiated

    [Header("Debug")]
    Rigidbody2D rig;
    int bounceCount = 0;
    float curVel = 0;
    Vector3 curVelVec;

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
        if (hit && bounceCount <= 2 && curVel >= 2f)
        {
            CheckRicochetChance(hit);
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
        curVelVec = new Vector3(Mathf.Clamp(rig.velocity.x, -140f, 140f), Mathf.Clamp(rig.velocity.y, -140f, 140f), 0);
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

    private void CheckRicochetChance(RaycastHit2D hit)
    {
        int chance = Random.Range(0, 100);
        float finalChance = (Vector2.Angle(transform.up, hit.normal) > 145) ? shellData.ricochetChance : shellData.ricochetChance * shellData.ricochetOver45Modifier;
        if (chance > finalChance)
        {
            TriggerProjectile(hit); // if not ricochet, calculate penetration //// deal damage to internals if penetration and chassis with correct sounds ( not done in here ) // deal damage to chassis and explode shell outside with correct fx and sounds ( not done in here ) 
        }
        else
        {
            Ricochet();
        }
    }

    private void TriggerProjectile(RaycastHit2D hit)
    {
        ParticleSystem ps = Instantiate(shellData.explosionFX, gameObject.transform.position, Quaternion.LookRotation((Vector2)transform.up));
        MasterEntityBase _mb = hit.collider.gameObject.GetComponent<HitboxFramework>()._mb;
        HitboxFramework _hf = hit.collider.gameObject.GetComponent<HitboxFramework>();
        _mb.CalculateDamageInitial(shellData.apMod * rig.velocity.magnitude / 10, hit.collider.gameObject.name, shellData.kineticDamage,
            shellData.innerExplosiveDamage + shellData.outerExplosiveDamage, _hf.armor, shellData.rawDamage);
        // play explosion sound, instantiate it
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
