using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CubeController : MonoBehaviour {

	[SerializeField]
	float moveSpeed;

	[SerializeField]
	float rotateSpeed;

	float[] parsedValues;

	public Text RecieveText;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 

	{
// 		float x_input = Input.GetAxis ("Horizontal");
// 		float z_input = Input.GetAxis ("Vertical");

// 		float x_input2 = System.Convert.ToSingle(Input.GetKey (KeyCode.I)) - System.Convert.ToSingle(Input.GetKey (KeyCode.K));
// 		float z_input2 = System.Convert.ToSingle(Input.GetKey (KeyCode.L)) - System.Convert.ToSingle(Input.GetKey (KeyCode.J));

// 		float dt = Time.deltaTime;

// 		//Translation:
// //		transform.Translate (moveSpeed * x_input * dt, 0f, moveSpeed* z_input * dt);
// 		transform.Translate (0f, 0f, moveSpeed * z_input * dt);
	
// 		//Rotation:
// 		//transform.Rotate(rotateSpeed * parsedValues[2] * dt, rotateSpeed * parsedValues[1] * dt, rotateSpeed * parsedValues[3] * dt);
	}

}
