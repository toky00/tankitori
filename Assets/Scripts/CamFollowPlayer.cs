using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public Vector3 offset = new Vector3(0, 0, -10);
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position + offset) > 0.5f)
        {
            transform.position = Vector3.Slerp(transform.position, player.transform.position + offset, speed * Time.deltaTime);
        }
    }
}
