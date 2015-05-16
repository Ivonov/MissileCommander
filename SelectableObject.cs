using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;

public class SelectableObject : MonoBehaviour {

    public bool selected = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (GetComponent<Renderer>().isVisible && Input.GetMouseButtonUp(0)) {
            Vector3 camPos = GameObject.Find("Main Camera").GetComponent<Camera>().transform.position;
            camPos.y = PlayerCameraControler.invertMouseY(camPos.y);
            selected = PlayerCameraControler.selection.Contains(camPos);
        }

        if (selected) {
            GetComponent<Renderer>().material.color = Color.red;
        } else {
            GetComponent<Renderer>().material.color = Color.white;
        }
	}
}
