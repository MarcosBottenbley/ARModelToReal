using UnityEngine;
using System.Collections;

public class highlight : MonoBehaviour {

	bool highlight_boole;

	// Use this for initialization
	void Start () {

		highlight_boole = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		bool C_input = Input.GetKeyDown (KeyCode.C);

		if (C_input && !highlight_boole) {
			GetComponent<Renderer> ().material.shader = Shader.Find ("ImageEffectShader");
			highlight_boole = true;
		}

		else if (C_input && highlight_boole) {
			GetComponent<Renderer> ().material.shader = Shader.Find ("Diffuse");
			highlight_boole = false;
		}


	}
}
