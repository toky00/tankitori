using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    public MasterEntityBase _eb;
    public Transform turretBarrelEnd;
    public ParticleSystem muzzleFX;
    public ParticleSystem shockwaveFX;

    private void FixedUpdate()
    {
        if (_eb.isPlayer && !_eb.isDestroyed)
        {
            TurretAiming();

            if (Input.GetMouseButtonDown(0))
                TurretShoot();
        }
    }
    void TurretAiming()
    {
        var dir =_eb.objectReferences.turretAimPoint.transform.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var targetRot = Quaternion.AngleAxis(angle, Vector3.forward);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, _eb.objectReferences.turretData.turretRotationSpeed / 10);
    }

    void TurretShoot()
    {
        GameObject shell = Instantiate(_eb.objectReferences.shellData[_eb.currentLoadedShell].shellPrefab, turretBarrelEnd.position, turretBarrelEnd.transform.rotation);
        Vector2 recoilDir = -shell.transform.up;
        Rigidbody2D shellRb = shell.GetComponent<Rigidbody2D>();

        muzzleFX.Play();
        shockwaveFX.Play();
        shellRb.AddForce(shell.transform.up * _eb.objectReferences.shellData[_eb.currentLoadedShell].propulsion, ForceMode2D.Impulse);
        _eb.objectReferences.entityRigidbody.AddForce(recoilDir * _eb.objectReferences.shellData[_eb.currentLoadedShell].propulsion / 10, ForceMode2D.Impulse);
    }
}
