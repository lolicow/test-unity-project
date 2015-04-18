using UnityEngine;
using System.Collections;


public class lose : MonoBehaviour {
	public int lose_status=0;
	
	Vector3 rightpoint = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width,Screen.height/2,0));
	Vector3 zeropoint = Camera.main.ScreenToWorldPoint (new Vector3(0,0,0));
	float scalar;
	void OnTriggerEnter(Collider collision) {

		if (collision.gameObject.GetComponent<Rigidbody> ().velocity.x <= 0)
			
						lose_status = 1;
	}
	// Use this for initialization
}
