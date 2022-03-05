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

        if (hasPenetrated)
        {
            if (gameObject.name.StartsWith("F."))
            {
                // damage front components
            }
            else if (gameObject.name.StartsWith("B."))
            {
                // damage back components
            }
            else if (gameObject.name.StartsWith("BR"))
            {
                // damage back right
            }
            else if (gameObject.name.StartsWith("MR"))
            {
                // damage middle right
            }
            else if (gameObject.name.StartsWith("FR"))
            {
                // damage front right
            }
            else if (gameObject.name.StartsWith("BL"))
            {
                // damage back left
            }
            else if (gameObject.name.StartsWith("ML"))
            {
                // damage middle left
            }
            else if (gameObject.name.StartsWith("FL"))
            {
                // damage front left
            }
        }
        else
        {
            // damage chassis with raw damage and explode shell on the outside
        }
    }
}
