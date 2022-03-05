using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingReticle : MonoBehaviour
{
    public GameObject turret;
    public Transform playerPosition;
    public SpriteRenderer sr;
    public GameObject accuracyMarker;
    public GameObject barrelEndMarker;
    public float accuracyModifier;

    private float timerForMoving = 1f;
    float count;
    private float accuracyMarkerMoveSpeed = 0.2f;
    bool timerStarted;
    private Vector2 moveAccuracyMarkerTo;

    private void OnEnable()
    {
        gameObject.transform.parent = null;
        accuracyModifier = turret.gameObject.GetComponent<TurretControl>()._eb.objectReferences.turretData.turretAccuracy;
    }

    private void FixedUpdate()
    {
        var dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10))) - playerPosition.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        var targetRot = Quaternion.AngleAxis(angle, Vector3.forward);
        gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 180);
        gameObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        barrelEndMarker.transform.localPosition = new Vector2(Vector3.Distance(playerPosition.position, accuracyMarker.transform.position), 0f);

        float sizeY = Vector3.Distance(playerPosition.position, accuracyMarker.transform.position)/accuracyModifier;
        sizeY = Mathf.Clamp(sizeY, 0.15f, Mathf.Infinity);

        sr.size = new Vector2(0.15f, sizeY);

        MoveAccuracyMarker();

    }

    public void MoveAccuracyMarker()
    {
        accuracyMarker.transform.localPosition = Vector2.MoveTowards(accuracyMarker.transform.localPosition, moveAccuracyMarkerTo, accuracyMarkerMoveSpeed * Time.fixedDeltaTime);
        //accuracyMarker.transform.localPosition = moveAccuracyMarkerTo;

        if (!timerStarted)
        {
            timerForMoving = Random.Range(5f, 10f);
            moveAccuracyMarkerTo = new Vector2(0, Random.Range((-sr.size.y/2f)-0.1f, (sr.size.y/2f)-0.1f));
            timerStarted = true;
        }
        else
        {
            count+=0.1f;
            if(count > timerForMoving)
            {
                timerStarted = false;
                count = 0;
            }
        }
    }
}
