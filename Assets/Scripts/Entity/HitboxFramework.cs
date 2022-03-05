using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxFramework : MonoBehaviour
{
    public MasterEntityBase _mb;
    public float armor;

    private void OnEnable()
    {
        _mb = gameObject.transform.parent.GetComponentInParent<MasterEntityBase>();
    }

    public void DamageCalculate(float apValue, float rawDamage)
    {
        bool hasPenetrated = (armor > apValue) ? true : false;
        int maxComponentCrewDamage = Random.Range(1, 4); //MAX AMOUNT OF COMPONENTS AND CREW TO DAMAGE
    }
}
