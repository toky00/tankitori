using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    [Header("Test")]
    public float turretRotationSpeed = 2f; //MAKE A SCRIPTABLE OBJECT STATS FRAMEWORK
    public float armorValue = 8f;
    public float projectileSpeed = 5f;
    public Transform turretBarrelEnd;
    public EntityBaseOLD _EB;

    private void Start()
    {
        _EB = gameObject.GetComponentInParent<EntityBaseOLD>();
    }

    private void Update()
    {
        if (_EB.Main.isPlayer && !_EB.Main.isDead)
        {
            TurretAiming();

            if (Input.GetMouseButtonDown(1))
                TurretShoot();
        }
    }

    void TurretAiming()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        var dir = Input.mousePosition - pos;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, turretRotationSpeed / 10);
    }

    void TurretShoot()
    {
        GameObject shell = Instantiate(_EB.Components.shell, turretBarrelEnd.position, turretBarrelEnd.transform.rotation); // MAKE A SCRIPTABLE OBJECT FOR SHELLS FRAMEWORK
        Rigidbody2D shellRb = shell.GetComponent<Rigidbody2D>();

        _EB.VFX.shootFX.Play();
        _EB.VFX.dirtFX.Play();
        shellRb.AddForce(shell.transform.up * projectileSpeed, ForceMode2D.Impulse);
        _EB.rb.AddForce(-shell.transform.up * projectileSpeed / 2, ForceMode2D.Impulse);
    }
}
