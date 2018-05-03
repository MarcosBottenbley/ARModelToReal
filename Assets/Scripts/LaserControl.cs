using UnityEngine;
using System.Collections;

public class LaserControl : MonoBehaviour {

	ArrayList modeList = new ArrayList();
	int modeInt;

	public float laserSpeed = 10f;
	Vector3 temp;

	// Use this for initialization
	void Start () {
		modeInt = 0;
		modeList.Add("Translation");
		modeList.Add("Scale");
	}
	
	// Update is called once per frame
	void Update () {
		//Updating modeInt if needed:
		if (Input.GetKeyDown (KeyCode.P)) {
			if (modeInt == 1) {
				modeInt = 0;
			} else
				modeInt = modeInt + 1;
		
		}

		//Only modify laser if in translation mode:
		if (modeInt == 0) {

			// float l_input = joystick_y - 630f;

			// temp = transform.localScale;

			// temp.z += l_input * laserSpeed * Time.deltaTime;

			// if (temp.z < 0.2f) {
			// 	temp.z = 0.2f;
			// }

			// if (temp.z > 4) {
			// 	temp.z = 4;
			// }

	
			// transform.localScale = temp;

		}

	}
}
