using UnityEngine;
public class aayena : MonoBehaviour
{
    public Vector3[] pathPoints;
    public ScriptableObjectTest scriptableObject;

    private void Start()
    {
        // Check if the scriptable object is assigned
        if (scriptableObject != null)
        {
            // Assign the Vector3 points from the scriptable object to the pathPoints array
            pathPoints = scriptableObject.Points;
        }
        else
        {
            Debug.LogError("ScriptableObjectTest is not assigned!");
        }
         

            // Create the path using the pathPoints array
        }

    private void OnDrawGizmos()
    {
        if (pathPoints == null || pathPoints.Length < 2)
            return;

        Gizmos.color = Color.white;

        // Draw lines between consecutive points in the path
        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            Gizmos.DrawLine(pathPoints[i], pathPoints[i + 1]);
            

        }
    }
}
