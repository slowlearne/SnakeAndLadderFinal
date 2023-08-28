using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public PlayerScript playerMovement;
     void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Obstacle")
        {
            Debug.Log("we hit an obstacle");
            playerMovement.enabled = false;
            Debug.Log("movement halted");
        }  
    }


}
