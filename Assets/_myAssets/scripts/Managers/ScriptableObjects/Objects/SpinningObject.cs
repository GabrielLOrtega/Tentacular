using UnityEngine;
using System.Collections;

public class SpinningObject : MonoBehaviour
{
	//public float SpinSpeed;
    public FloatContainer SpinSpeed;


    void Update () 
	{
		float amtToMove = SpinSpeed.floatVariable * Time.deltaTime;
		transform.Rotate (Vector3.up * amtToMove);
	
	}
}
