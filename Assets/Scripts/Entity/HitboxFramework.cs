using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxFramework : MonoBehaviour
{
    public MasterEntityBase _mb;
    public ChassisFramework _mb_cf;
    public float armor;

    private void OnEnable()
    {
        _mb = gameObject.transform.parent.GetComponentInParent<MasterEntityBase>();
        _mb_cf = _mb.gameObject.GetComponent<ChassisFramework>();
    }

    void CheckName(string name, float kineticDamage, float explosiveDamageTotal)
    {
        if (name == "Ammo")
            _mb.ammoHealth -= kineticDamage/2 + explosiveDamageTotal;
        else if (name == "Engine")
            _mb.engineHealth -= kineticDamage;
    }

    public void DamageCalculate(float apValue, float rawDamage, float kineticDamage, float explosiveDamageTotal)
    {
        bool hasPenetrated = (armor > apValue) ? true : false;
        int maxComponentCrewDamage = Random.Range(1, 3); //MAX AMOUNT OF COMPONENTS AND CREW TO DAMAGE

        if (hasPenetrated)
        {
            /*foreach (GameObject hitbox in _mb.objectReferences.hitboxes)
            {
                if(hitbox.name == gameObject.name)
                int count = 0;
                foreach (string component in _mb_cf.componentPositions.front)
                {
                    count++;
                    int rando = Random.Range(1, 100);
                    if (rando >= 50)
                    {
                        if (count == 3)
                        {
                            //less damage
                            CheckName(component, kineticDamage / 2, explosiveDamageTotal / 2);
                        }
                        else if (count >= 2)
                        {
                            CheckName(component, kineticDamage, explosiveDamageTotal);
                        }
                    }
                }
            }*/
            if (gameObject.name.StartsWith("F."))
            {
                
                
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
