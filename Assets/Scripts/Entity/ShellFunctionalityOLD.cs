using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellFunctionalityOLD : MonoBehaviour
{
    public LayerMask environment;
    public LayerMask destructable;
    public LayerMask penetrable;
    public int ricochetChance = 30;
    public int ricochetMultiplierOn45 = 3;
    public float projectileAPValue;
    public float projectileDMG;
    public float explosionRadius;
    public ParticleSystem explodeFX;
    Rigidbody2D rig;
    private int bounceCount = 0;
    private float curVel = 0;
    private Vector3 curVelVec;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        curVel = rig.velocity.magnitude;
        Debug.Log(curVel);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 0.8f, environment + destructable);
        if (hit && bounceCount <= 2 && curVel >= 2f)
        {
            CheckRicochetChance(hit);
        }
    }

    private void FixedUpdate()
    {
        Debug.Log(rig.velocity + " 1");
        curVelVec = rig.velocity;
        curVelVec = new Vector3(Mathf.Clamp(rig.velocity.x, -140f, 140f), Mathf.Clamp(rig.velocity.y, -140f, 140f), 0);
        rig.velocity = curVelVec;
        Debug.Log(rig.velocity + " 2");

        Debug.DrawRay(transform.position, transform.up * 0.8f, Color.red);
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
        int finalChance = (Vector2.Angle(transform.up, hit.normal) > 145) ? ricochetChance : ricochetChance * ricochetMultiplierOn45;
        if (chance > finalChance)
        {
            TriggerProjectile(hit);
        }
        else
        {
            Ricochet();
        }
    }

    private void TriggerProjectile(RaycastHit2D hit)
    {
        //Debug.Log("HIT! Bounced: " + bounceCount + " times.");
        ParticleSystem ps = Instantiate(explodeFX, gameObject.transform.position, Quaternion.LookRotation((Vector2)transform.up));
        Destroy(ps, 5f);
        Destroy(gameObject);
    }

    private void Ricochet()
    {
        bounceCount++;
        //Debug.Log("RICOCHET! Bounced: " + bounceCount + " times.");
    }

    private bool CheckPenetrationChance()
    {
        return true;
    }
}
