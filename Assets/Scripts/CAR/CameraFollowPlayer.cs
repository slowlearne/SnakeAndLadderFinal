using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    public Vector3 cameraPositionDuringPlayerMove;
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("player position" + player.position);
        transform.position = player.position + cameraPositionDuringPlayerMove;
        
    }
}
