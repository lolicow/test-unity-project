using UnityEngine;
using System.Collections;
using AssemblyCSharp;
public class start : MonoBehaviour {

	public GameObject oneball;
	private int game_status; //1=normal,2=killing,3=dropping
	private int wait_frame;
	private int randomcolor;
	private ball ballcursor;
	private int input_allowed = 0;
//	private int mouse_moved =0;
	private Vector3 mouse_was_at;
	private bool is_dragging=false;
	private ball[,] ballcontrol = new ball[6,6];
	private string showtext1;
	private string showtext2;
	private int point=0;
	private int gamestart=0;
	private int killed=0;
	private int turn=0;
	private int pointadded=0;
	private float scalar;
	private Vector3 zeropoint;
	private Vector3 rightpoint;
	private int device;
	public GameObject onecube;
	private int gameover=0;
	private float sec_passed=0;
	private GameObject wall;
	private float time_passed;
	private float time_killed=0;
	private int fresh_sec=120;
	private int current_frame=-1;

	void OnGUI(){
		GUIStyle temp = new GUIStyle();
		temp.fontSize = Screen.width/30;
	//	Debug.Log (temp.fontSize);
		if (gamestart == 0) {
			if (GUI.Button (new Rect (0, 0, Screen.width/2, Screen.width/6), "Try to stay as long as possible!\ntap here to start!",temp)) {
								gamestart = 1;
				time_killed=Time.time;
				//starttime = Time.time;
						}
				}
		if (gamestart == 1) {

						GUI.TextArea (new Rect (0, 0, 300, 150), showtext1,temp);
			GUI.TextArea (new Rect (Screen.width/2, 0, Screen.width/2,Screen.width/6), showtext2,temp);
				}
		if (gamestart == 2) {
			temp.fontSize *=2;
			if(GUI.Button (new Rect (0, 0, Screen.width/2,Screen.height/2), "game over!\ntap here to restart!\n you kept alive for\n "+time_passed+" seconds.",temp))
			{Application.LoadLevel("scene1");
				}
				}
	}
			


	// Use this for initialization
	void Start () {

		showtext1 = "game begins !";
		showtext2 = "your point now is: 0";
		int screenheight = Screen.height;
		int screenwidth = Screen.width;

		rightpoint = Camera.main.ScreenToWorldPoint (new Vector3(screenwidth,screenheight/2,0));
		zeropoint = Camera.main.ScreenToWorldPoint (new Vector3(0,0,0));
		scalar = (rightpoint.x - zeropoint.x) / 20;
		GameObject tempobject = Instantiate (onecube, new Vector3 (rightpoint.x, rightpoint.y, 0), Quaternion.identity) as GameObject;
		tempobject.GetComponent<Transform>().localScale=(new Vector3(0,100,100));
		tempobject.AddComponent<Rigidbody>();
		tempobject.AddComponent<BoxCollider>();
		tempobject.GetComponent<Rigidbody>().useGravity=false;
		//Debug.Log (tempobject.GetComponent<BoxCollider> ().attachedRigidbody);
		tempobject.GetComponent<BoxCollider> ().enabled = true;
		tempobject.GetComponent<BoxCollider> ().isTrigger = true;
		//tempobject.GetComponent<Rigidbody> ().isKinematic = true;

		wall = Instantiate (onecube, new Vector3 (zeropoint.x+6*scalar, rightpoint.y, 0), Quaternion.identity) as GameObject;
		wall.GetComponent<Transform>().localScale=(new Vector3(0,100,100));
		wall.AddComponent<Rigidbody>();
		wall.AddComponent<BoxCollider>();
		wall.GetComponent<Rigidbody>().useGravity=false;
		//Debug.Log (tempobject.GetComponent<BoxCollider> ().attachedRigidbody);
		wall.GetComponent<BoxCollider> ().enabled = true;
		wall.GetComponent<BoxCollider> ().isTrigger = true;
		wall.AddComponent<lose>();
	
		if (Application.platform == RuntimePlatform.WindowsPlayer)
						device = 1;
				else
						device = 1;
//		Debug.Log ((rightpoint.x - zeropoint.x) / 20 + "," + scalar / device);

						game_status = 1;
						wait_frame = 1;
						for (int y = 0; y < 6; y++) {
								for (int x = 0; x < 6; x++) {
										ballcontrol [x, y] = new ball ();
										ballcontrol [x, y].settheball (Instantiate (oneball, new Vector3 (scalar/(device*2)+zeropoint.x+x*scalar/device, scalar/(device*2)+zeropoint.y+y*scalar/device, 0), Quaternion.identity) as GameObject);
				ballcontrol[x,y].gettheball ().GetComponent<Transform>().localScale=(new Vector3(scalar/device,scalar/device,scalar/device));
										ballcontrol [x, y].set_empty (0);
										randomcolor = Random.Range (0, 5);
										ballcontrol [x, y].set_color (randomcolor);
										switch (randomcolor) {
										case 0:
												ballcontrol [x, y].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.white;
												break;
										case 1:
												ballcontrol [x, y].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.red;
												break;
										case 2:
												ballcontrol [x, y].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.blue;
												break;
										case 3:
												ballcontrol [x, y].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.green;
												break;
										case 4:
												ballcontrol [x, y].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.yellow;
												break;
										default:
												break;
										}
								}
						}
						input_allowed = 1;
						showtext1 = "game begins !";
						showtext2 = "your point now is: 0";
						/*	for (int y = 0; y < 6; y++) {
			for (int x = 0; x < 6; x++) {
				if (x!=5)
					ballcontrol[x,y].right=ballcontrol[x+1,y];
					else
						ballcontrol[x,y].right=null;

				if(y!=5)
					ballcontrol[x,y].bottom=ballcontrol[x,y+1];
					else
						ballcontrol[x,y].bottom=null;

			}
		} */
					

	}
	
	// Update is called once per frame
	void Update () {



				if (gamestart == 1) {
			if (wall.GetComponent<lose> ().lose_status == 1)
				gameover = 1;

						if (gameover == 0) {
				time_passed=Time.time-time_killed;
				showtext2 = "This is turn " + turn + "\n" + killed + " spheres killed last turn\n your point is now: " + point +"\n you are alive for "+ time_passed;
				current_frame++;
				if(true)
				{
					sec_passed+=1;
					float i,j,k;
					i=Screen.width;
					
					k=Random.Range(scalar/device,3*scalar/device);
					j=Random.Range(scalar/(device*2)+zeropoint.y+k/2,scalar/(device*2)+zeropoint.y+5*scalar/device-k/2);
					//l=Random.Range(sec_passed/180,2000+sec_passed/180);
					GameObject enemyobject;
					//Debug.Log (l+","+sec_passed);
					if (fresh_sec-(int)time_passed<=40)
					{
						fresh_sec=40+(int)time_passed;
					}
					if (current_frame%(fresh_sec-(int)time_passed)==0)
					{
						Random.seed=(int)Time.time;
						int randomcolor2=Random.Range (1,4);
						//Debug.Log(k);
						enemyobject	= Instantiate(onecube,new Vector3(rightpoint.x-k/2-1/1000,j,0), Quaternion.identity) as GameObject;
						switch(randomcolor2){
						case 1:
						enemyobject.GetComponent<MeshRenderer>().material.color=Color.cyan;
							break;
						case 2:
							enemyobject.GetComponent<MeshRenderer>().material.color=Color.magenta;
							break;
						case 3:
							enemyobject.GetComponent<MeshRenderer>().material.color=Color.grey;
							break;
						default:
							enemyobject.GetComponent<MeshRenderer>().material.color=Color.black;
							break;
						}
						enemyobject.GetComponent<Transform>().localScale=new Vector3(k,k,k);
						enemyobject.AddComponent<Rigidbody>();
						enemyobject.AddComponent<BoxCollider>();
						enemyobject.GetComponent<Rigidbody>().useGravity=false;
						
						//Debug.Log (tempobject.GetComponent<BoxCollider> ().attachedRigidbody);
						enemyobject.GetComponent<BoxCollider> ().enabled = true;
						enemyobject.GetComponent<BoxCollider> ().isTrigger = true;
						//enemyobject.GetComponent<Rigidbody> ().isKinematic = true;

						enemyobject.GetComponent<Rigidbody>().velocity=new Vector3(-scalar*scalar*2/k/device*(1+(time_passed)/120),0,0);
						enemyobject.GetComponent<Rigidbody>().angularVelocity= new Vector3(1,1,1);
						enemyobject.AddComponent<hp>();
					
					
					}
				}
								if (game_status == 1) {




										
										//player operation 
										//start dragging
										if (!is_dragging) {
												if (input_allowed == 1) {
					
					
														

														if (Input.GetMouseButtonDown (0) == true) {
		
																Vector3 a = Camera.main.ScreenToWorldPoint (Input.mousePosition);
												
																mouse_was_at = a;
//																Debug.Log ("mouse pressed at " + a.x + "," + a.y + "," + a.z);
//																Debug.Log ("scalar is " + scalar + " zero point is  " + zeropoint.x + "," + zeropoint.y);
																is_dragging = true;

														}
												}
										}
				//dragging
					else {
		
												if (Input.GetMouseButton (0) == true) {
	
														Vector3 a = Camera.main.ScreenToWorldPoint (Input.mousePosition);
														//float distance = ballcontrol[1,0].gettheball().GetComponent<Transform>().localPosition.x-ballcontrol[0,0].gettheball().GetComponent<Transform>().localPosition.x;
														//Debug.Log (distance);	
														//	Debug.Log ("mouse dragged to " + a.x + "," + a.y + "," + a.z);
														//Debug.Log ("x distance  travelled " +(System.Math.Abs (mouse_was_at.x - a.x)));
														if ((System.Math.Abs (mouse_was_at.x - a.x) >= scalar / device) && (System.Math.Abs (mouse_was_at.x - a.x) >= System.Math.Abs (mouse_was_at.y - a.y))) {
																//Debug.Log ("entered if loop");	
																for (int i = 0; i < 6; i++) {
																		for (int j = 0; j < 6; j++) {

																				//Debug.Log ("entered for loop");
																				if ((mouse_was_at.x >= zeropoint.x + i * scalar / device) && (mouse_was_at.x < 2 * scalar / (device * 2) + zeropoint.x + i * scalar / device) && (mouse_was_at.y >= zeropoint.y + j * scalar / device) && (mouse_was_at.y < 2 * scalar / (device * 2) + zeropoint.y + j * scalar / device)) {
																						//Destroy (ballcontrol [i, j].gettheball ());
																						//ballcontrol [i, j].set_empty (1);
									
																						//		input_allowed=0;
																						//	game_status=3;
																						int tempcolor = ballcontrol [i, j].get_color ();
																						ballcontrol [i, j].set_color (ballcontrol [i - ((mouse_was_at.x - a.x) > 0 ? 1 : -1), j].get_color ());
																						ballcontrol [i - ((mouse_was_at.x - a.x) > 0 ? 1 : -1), j].set_color (tempcolor);
																						Color colortemp = ballcontrol [i, j].gettheball ().GetComponent<MeshRenderer> ().material.color;
																						ballcontrol [i, j].gettheball ().GetComponent<MeshRenderer> ().material.color = ballcontrol [i - ((mouse_was_at.x - a.x) > 0 ? 1 : -1), j].gettheball ().GetComponent<MeshRenderer> ().material.color;
																						ballcontrol [i - ((mouse_was_at.x - a.x) > 0 ? 1 : -1), j].gettheball ().GetComponent<MeshRenderer> ().material.color = colortemp;
																						is_dragging = false;
																						input_allowed = 0;
																						wait_frame = 21;
																						turn ++;

																						killed = 0;
																				}
																		}
																}

														}

														if ((System.Math.Abs (mouse_was_at.y - a.y) >= scalar / device) && (System.Math.Abs (mouse_was_at.y - a.y) >= System.Math.Abs (mouse_was_at.x - a.x))) {
																for (int i = 0; i < 6; i++) {
																		for (int j = 0; j < 6; j++) {
																				if ((mouse_was_at.x >= zeropoint.x + i * scalar / device) && (mouse_was_at.x < 2 * scalar / (device * 2) + zeropoint.x + i * scalar / device) && (mouse_was_at.y >= zeropoint.y + j * scalar / device) && (mouse_was_at.y < 2 * scalar / (device * 2) + zeropoint.y + j * scalar / device)) {
																						//Destroy (ballcontrol [i, j].gettheball ());
																						//ballcontrol [i, j].set_empty (1);
									
																						//		input_allowed=0;
																						//	game_status=3;
																						int tempcolor = ballcontrol [i, j].get_color ();
																						ballcontrol [i, j].set_color (ballcontrol [i, j - ((mouse_was_at.y - a.y) > 0 ? 1 : -1)].get_color ());
																						ballcontrol [i, j - ((mouse_was_at.y - a.y) > 0 ? 1 : -1)].set_color (tempcolor);
																						Color colortemp = ballcontrol [i, j].gettheball ().GetComponent<MeshRenderer> ().material.color;
																						ballcontrol [i, j].gettheball ().GetComponent<MeshRenderer> ().material.color = ballcontrol [i, j - ((mouse_was_at.y - a.y) > 0 ? 1 : -1)].gettheball ().GetComponent<MeshRenderer> ().material.color;
																						ballcontrol [i, j - ((mouse_was_at.y - a.y) > 0 ? 1 : -1)].gettheball ().GetComponent<MeshRenderer> ().material.color = colortemp;
																						is_dragging = false;
																						input_allowed = 0;
																						wait_frame = 21;
																						turn ++;

																						killed = 0;
																				}
																		}
																}
						
														}

												} else
						//dragging fail
														is_dragging = false;
												input_allowed = 0;
										}
								}


								if (wait_frame == 21) {




										// normal status
										switch (game_status) {
										case 1:

				//Debug.Log ("enter normal mode");
				//fill in empty cells
												for (int y = 0; y < 6; y++) {
														for (int x = 0; x < 6; x++) {
																if (ballcontrol [x, y].get_empty () == 1) {

																		ballcontrol [x, y].settheball (Instantiate (oneball, new Vector3 (scalar / (device * 2) + zeropoint.x + x * scalar / device, scalar / (device * 2) + zeropoint.y + y * scalar / device, 0), Quaternion.identity) as GameObject);
																		ballcontrol [x, y].gettheball ().GetComponent<Transform> ().localScale = (new Vector3 (scalar / device, scalar / device, scalar / device));
																		ballcontrol [x, y].set_empty (0);
																		randomcolor = Random.Range (0, 5);
																		ballcontrol [x, y].set_color (randomcolor);
																		switch (randomcolor) {
																		case 0:
																				ballcontrol [x, y].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.white;
																				break;
																		case 1:
																				ballcontrol [x, y].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.red;
																				break;
																		case 2:
																				ballcontrol [x, y].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.blue;
																				break;
																		case 3:
																				ballcontrol [x, y].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.green;
																				break;
																		case 4:
																				ballcontrol [x, y].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.yellow;
																				break;
																		default:
																				break;
																		}
																}
														}
												}
												input_allowed = 1;
			
				//find the cells needed to be killed

						//row linked
												for (int y = 0; y < 6; y++) {
														int countball = 1;
														int ballcolor = ballcontrol [0, y].get_color ();
														for (int x = 1; x < 6; x++) {
																if (ballcolor == ballcontrol [x, y].get_color ()) 
																		countball++;
																else {
																		if (countball >= 3) {
																				for (int k=1; k<=countball; k++) {
																						ballcontrol [x - k, y].set_row_linked (1);
																						game_status = 2;
																						input_allowed = 0;
																				}
																		}
					
																		countball = 1;
																		ballcolor = ballcontrol [x, y].get_color ();
																}




														}
														if (countball >= 3) {
																for (int k=1; k<=countball; k++) {
																		ballcontrol [6 - k, y].set_row_linked (1);
																		game_status = 2;
																		input_allowed = 0;
																}
														}
												}

					//column linked
												for (int x = 0; x < 6; x++) {
														int countball = 1;
														int ballcolor = ballcontrol [x, 0].get_color ();
														for (int y = 1; y < 6; y++) {
																if (ballcolor == ballcontrol [x, y].get_color ()) 
																		countball++;
																else {
																		if (countball >= 3) {
																				for (int k=1; k<=countball; k++) {
																						ballcontrol [x, y - k].set_column_linked (1);
																						game_status = 2;
																						input_allowed = 0;
																				}
																		}
							
																		countball = 1;
																		ballcolor = ballcontrol [x, y].get_color ();
																}

						
														}
														if (countball >= 3) {
																for (int k=1; k<=countball; k++) {
																		ballcontrol [x, 6 - k].set_row_linked (1);
																		game_status = 2;
																		input_allowed = 0;
																}
														}
												}
												if (input_allowed == 1) {
														showtext1 = "move spheres to attack cubes!\ndrag a sphere to move it!\nmore and more cubes will arrive!";
														if (pointadded == 0) {
																point += killed;
																pointadded = 1;
														}
												}

												break;
						

										//killing status

										//kill
										case 2:
												showtext1 = "killing linked spheres \n do not operate.";
												pointadded = 0;
			//	Debug.Log ("enter killing mode");
												for (int y = 0; y < 6; y++) {
														for (int x = 0; x < 6; x++) {
																if ((ballcontrol [x, y].get_row_linked () + ballcontrol [x, y].get_column_linked ()) > 0) {
																		killed += 1;
																		//Destroy (ballcontrol [x, y].gettheball ());
																		ballcontrol [x, y].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.black;
																		ballcontrol [x, y].gettheball ().AddComponent<Rigidbody> ();
																		ballcontrol [x, y].gettheball ().AddComponent<BoxCollider> ();
//																		Debug.Log (ballcontrol [x, y].gettheball ().GetComponent<Collider> ().attachedRigidbody);
																		ballcontrol [x, y].gettheball ().GetComponent<Rigidbody> ().useGravity = false;
																		
																		ballcontrol [x, y].gettheball ().GetComponent<Rigidbody> ().velocity = new Vector3 (scalar / device * 30, 0, 0);
																		ballcontrol [x, y].gettheball ().GetComponent<BoxCollider> ().enabled = true;
									ballcontrol [x, y].gettheball ().GetComponent<BoxCollider> ().isTrigger = true;
																		ballcontrol [x, y].gettheball ().AddComponent <collision>();
                                                                        ballcontrol[x, y].gettheball().AddComponent<autodestroy>();
//																		Debug.Log ("add script once");
																		ballcontrol [x, y].set_empty (1);
																		ballcontrol [x, y].set_row_linked (0);
																		ballcontrol [x, y].set_column_linked (0);
																}
														}
												}
										
			//prepare for dropping


												game_status = 3;
												break;
			
										case 3:
												showtext1 = "spheres dropping \n do not operate.";
			//dropping status

				//Debug.Log ("enter dropping mode");
												int dropping = 0;
												while (true) {	
														//Debug.Log("enter dropping while loop");
														dropping = 0;
														for (int y = 1; y < 6; y++) {
																for (int x = 0; x < 6; x++) {
																		//Debug.Log("enter dropping for loop");
																		if ((ballcontrol [x, y].get_empty () == 0) && (ballcontrol [x, y - 1].get_empty () == 1)) {
																				//	Debug.Log("wocao");
																				dropping = 1;
																				ballcontrol [x, y - 1].settheball (Instantiate (oneball, new Vector3 (scalar / (device * 2) + zeropoint.x + x * scalar / device, scalar / (device * 2) + zeropoint.y + (y - 1) * scalar / device, 0), Quaternion.identity) as GameObject);
																				ballcontrol [x, y - 1].gettheball ().GetComponent<Transform> ().localScale = (new Vector3 (scalar / device, scalar / device, scalar / device));
																				ballcontrol [x, y - 1].set_empty (0);
																				ballcontrol [x, y - 1].set_color (ballcontrol [x, y].get_color ());
																				switch (ballcontrol [x, y].get_color ()) {
																				case 0:
																						ballcontrol [x, y - 1].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.white;
																						break;
																				case 1:
																						ballcontrol [x, y - 1].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.red;
																						break;
																				case 2:
																						ballcontrol [x, y - 1].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.blue;
																						break;
																				case 3:
																						ballcontrol [x, y - 1].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.green;
																						break;
																				case 4:
																						ballcontrol [x, y - 1].gettheball ().GetComponent<MeshRenderer> ().material.color = Color.yellow;
																						break;
																				default:
																						break;
																				}
																				//Debug.Log ("destroy flying cell");
																				Destroy (ballcontrol [x, y].gettheball ());	
																				ballcontrol [x, y].set_empty (1);
																		}
																}
														}
														if (dropping == 0)
																break;
												}
				
												game_status = 1;
												break;
										}
			



										wait_frame = 1;
								} else {
										wait_frame++;
								}
						}
			if (gameover==1){
				gamestart=2;

			}
				}
	
		}

}
