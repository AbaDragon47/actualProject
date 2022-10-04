using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(-2, 2, -3);

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
       // transform.rotate=(player.transform.Rotate*offset);
    }


}
