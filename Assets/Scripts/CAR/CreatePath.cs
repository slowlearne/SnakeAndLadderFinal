using System.Collections.Generic;
using UnityEngine;

public class CreatePath : MonoBehaviour
{
    public List<Vector3> waypoints = new List<Vector3>();


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        for (int i = 0; i < waypoints.Count - 1; i++)
        {
            Gizmos.DrawLine(waypoints[i], waypoints[i + 1]);
        }
    }
}



