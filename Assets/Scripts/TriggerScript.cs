using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour {

	bool select_var = false;
	ArrayList modeList = new ArrayList();
	int modeInt;
	public float scaleSpeed = 5;
	Vector3 temp;

	// Needed to work with different modes:
	void Start () {
		modeInt = 0;
		modeList.Add("Translation");
		modeList.Add("Scale");
	}

	// Needed to possibly change mode:
	void Update () {
		//Updating modeInt if needed:
		if (Input.GetKeyDown (KeyCode.P)) {
			if (modeInt == 1) {
				modeInt = 0;
			} else
				modeInt = modeInt + 1;

		}

	}

	void OnTriggerEnter(Collider other) 
	{

		//other.gameObject.SetActive (false);
		other.GetComponent<Renderer> ().material.shader = Shader.Find ("ImageEffectShader");


	}

	void OnTriggerStay(Collider other)
	{

		//Updating scaling for Scaling mode:
		if (select_var && modeInt==1) {

			float scaleDirection = System.Convert.ToSingle (Input.GetKey (KeyCode.Q)) - System.Convert.ToSingle (Input.GetKey (KeyCode.E));

			float dt = Time.deltaTime;
			temp.x = other.transform.localScale.x + scaleDirection * scaleSpeed * dt * 1.0f;
			temp.y = other.transform.localScale.x + scaleDirection * scaleSpeed * dt * 1.0f;
			temp.z = other.transform.localScale.x + scaleDirection * scaleSpeed * dt * 1.0f;
			if (temp.x < 0.7) {
				temp.x = 0.7f;
			}

			if (temp.x > 4) {
				temp.x = 4.0f;
			}

			if (temp.x > 4) {
				temp.x = 4.0f;
			}

			if (temp.x > 4) {
				temp.x = 4.0f;
			}

			if (temp.x > 4) {
				temp.x = 4.0f;
			}

			if (temp.x < 0.7) {
				temp.x = 0.7f;
			}


			other.transform.localScale = temp;

//			other.transform.localScale.x += scaleDirection * scaleSpeed * 1.0f;
//			other.transform.localScale.y += scaleDirection * scaleSpeed * 1.0f;
//			other.transform.localScale.z += scaleDirection * scaleSpeed * 1.0f;
		}
		
		//Updating selection variable:
		if (Input.GetKeyDown (KeyCode.C)) {
			select_var = !select_var;
		}

		//Updating position if selected:
		if (select_var) {
			other.transform.position = transform.position;
		}



	}


	void OnTriggerExit(Collider other) 
	{

		//other.gameObject.SetActive (false);
		other.GetComponent<Renderer> ().material.shader = Shader.Find ("Diffuse");
	}


}
