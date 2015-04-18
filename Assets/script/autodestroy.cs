using UnityEngine;
using System.Collections;

public class autodestroy : MonoBehaviour {

	void Start(){

		}

	// Update is called once per frame
	void Update () {

		if (gameObject.GetComponent<Transform> ().position.x + gameObject.GetComponent<Transform> ().position.y >= 300)
						Destroy (gameObject);
	}
}
