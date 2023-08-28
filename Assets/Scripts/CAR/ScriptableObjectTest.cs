using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "bhaihalxani", menuName = "j ni hunxa")]
public class ScriptableObjectTest : ScriptableObject
{
   public Vector3[] Points = new Vector3[4] {

         new Vector3(0f, 0f, 0f),
         new Vector3(2f, 0f, 0f),
        new Vector3(4f, 2f, 0f),
        new Vector3(6f, 2f, 0f)
};

}

    

