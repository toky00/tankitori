using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterEntityBase : MonoBehaviour
{
    [Header("Turret References")]
    public bool hasTurret = true;
    [ConditionalHide("hasTurret", true)] public GameObject turretPrefab;
    [ConditionalHide("hasTurret", true)] public Transform turretPosition;
    [ConditionalHide("hasTurret", true)] public TurretFramework _turretData;
}
