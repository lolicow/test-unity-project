using UnityEngine;
using System.Collections;

public class collision : MonoBehaviour {
	Vector3 rightpoint = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width,Screen.height/2,0));
	Vector3 zeropoint = Camera.main.ScreenToWorldPoint (new Vector3(0,0,0));
   
	float scalar;
	void OnTriggerEnter(Collider collision) {
        
		scalar = (rightpoint.x - zeropoint.x) / 24;
        //Debug.LogError("rightpoint£º" + rightpoint.x + "zeropoint" + zeropoint.x + "scalar" + scalar);
        if ((collision.gameObject.GetComponent<Rigidbody>().velocity.x <= 0) && (collision.gameObject.GetComponent<Transform>().position.x > 0))
        {
            //Debug.LogError("X SPEED:" + collision.gameObject.GetComponent<Rigidbody>().velocity.x + "X POSITION:" + collision.gameObject.GetComponent<Transform>().position.x + "required position" + (zeropoint.x + 7 * scalar));
            Destroy(gameObject);
        }
	}
}
