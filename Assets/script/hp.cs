using UnityEngine;
using System.Collections;

public class hp : MonoBehaviour {

	void OnTriggerEnter(Collider collision) {
		float i,j,k;
		float a, b;
		a = Random.Range (1, 60);
		b = Random.Range (20, 40);
		//int lola;
		//int lolb;

		//lolb = Random.Range (1, 5/2);


		//if (lolb == 1)
				//		b = -b;

		Vector3 rightpoint = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width,Screen.height/2,0));
		Vector3 zeropoint = Camera.main.ScreenToWorldPoint (new Vector3(0,0,0));
		float scalar = (rightpoint.x - zeropoint.x) / 24;
		if (collision.gameObject.GetComponent<Rigidbody> ().velocity.x > 0) {
			//Debug.Log ("speed down for "+ scalar*scalar*2 / gameObject.GetComponent<Transform> ().localScale.x/gameObject.GetComponent<Transform> ().localScale.x/gameObject.GetComponent<Transform> ().localScale.x);

			i = gameObject.GetComponent<Rigidbody> ().velocity.x + (scalar*scalar*2 / gameObject.GetComponent<Transform> ().localScale.x/gameObject.GetComponent<Transform> ().localScale.x/gameObject.GetComponent<Transform> ().localScale.x);
			//Debug.Log ("speed is now " + i);			
			j = gameObject.GetComponent<Rigidbody> ().velocity.y;
						k = gameObject.GetComponent<Rigidbody> ().velocity.z;
						gameObject.GetComponent<Rigidbody> ().velocity = new Vector3 (0, j, k);
						if (gameObject.GetComponent<Rigidbody> ().velocity.x >= 0) 
			{
				gameObject.GetComponent<Rigidbody>().velocity=new Vector3(a,b,0);
			gameObject.GetComponent<MeshRenderer>().material.color=Color.black;
			Destroy(gameObject.GetComponent<BoxCollider>());
				gameObject.AddComponent<autodestroy>();
				      
			}
				}
	}

}

