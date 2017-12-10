using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInPlace : MonoBehaviour {

    public Vector3 rotationSpeed;

	void Start () {
		
	}
	
	void Update () {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
