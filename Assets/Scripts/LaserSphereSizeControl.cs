using UnityEngine;
using System.Collections;

public class LaserSphereSizeControl : MonoBehaviour {

	Vector3 temp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		temp = transform.localScale;
//		temp.x = System.Convert.ToSingle(temp.x);
//		temp.y = System.Convert.ToSingle(temp.y);
//		temp.z = System.Convert.ToSingle(temp.z);

		temp.x = 0.5f;
		temp.y = 0.5f;
		temp.z = 0.5f;

	}
}
