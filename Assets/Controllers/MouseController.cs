using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MouseController : MonoBehaviour {

	Vector3 lastFramePosition;
	Vector3 currFramePosition;

	bool placementMode=false;
	bool movementMode=false;

	float screenRatio;
	float widthOrtho;

	//world
	World world;

	//for charge handling
	int chargeIdx;
	string pol;

	// Use this for initialization
	void Start () {
		screenRatio= (float) Screen.width / (float) Screen.height;
		widthOrtho = Camera.main.orthographicSize * screenRatio;

		world = Simulation.Instance.world;

		chargeIdx = -1;
		pol = "-";
	}
	
	// Update is called once per frame
	void Update () {
		currFramePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition);
		//move the z out of the near clipping plane
		currFramePosition.z=0;


		chargeControl ();



		//lastFramePosition = Input.mousePosition;  //Input.mousePosition is the pos of the mouse in screen coords, not world coords
		//instead, use the camera to tell mouse where it is in world coords
		//this gets the most up todate version of the posion, rather than using the currFramePosition, which is not the most up to date
		//otherwise, you'd get some jiggling
		lastFramePosition=Camera.main.ScreenToWorldPoint(Input.mousePosition); //works well with orthographic camera, only gives point on camera clip plane (not correct z)
		lastFramePosition.z=0;
	}


	void chargeControl(){
		//placement mode logic
		if (placementMode == true) {
			//float sizex=widthOrtho*2f;
			//float unitx = sizex/Simulation.Instance.world.GridX;
			//make sure mouse is on the screen
			//Debug.Log("Mouse position: "+currFramePosition.x+" "+currFramePosition.y+" "+widthOrtho);
			if (Mathf.Abs (currFramePosition.x) < widthOrtho && Mathf.Abs (currFramePosition.y) < widthOrtho){ 
				//x and y position of the mouse in the simulation
				float xpos=(currFramePosition.x+widthOrtho)*Simulation.Instance.world.GridX/(widthOrtho*2f);
				float ypos=(currFramePosition.y+widthOrtho)*Simulation.Instance.world.GridX/(widthOrtho*2f);
				//check if the mouse has moved since the last fram, if not, you don't actually have to recompute everything
				if (currFramePosition.y != lastFramePosition.y && currFramePosition.x != lastFramePosition.x || chargeIdx==-1) {
					//create new charge
					int chargeCount = Simulation.Instance.world.charges.Count;
					if (chargeIdx == -1) {
						//must place a new charge because there are none
						//FIXME need a better way to control the grid and the two coordinate systems
						chargeIdx =Simulation.Instance.addCharge (xpos, ypos, pol, 1f,currFramePosition.x,currFramePosition.y);
						world.charges [chargeIdx].Perm = false;
						world.updateWorld ();
						ArrowController.Instance.updateArrows ();

					} else {
						//have the charge follow the mouse
						world.charges [chargeIdx].Xpos = xpos;
						world.charges [chargeIdx].Ypos = ypos;
						world.updateWorld ();
						Simulation.Instance.chargeDict [chargeIdx.ToString()].transform.position = new Vector3(currFramePosition.x,currFramePosition.y,0f);
						ArrowController.Instance.updateArrows ();
					}
				}

				//left mouse button places charge
				if (Input.GetMouseButtonDown (0)) {
					world.charges [chargeIdx].Perm = true;
					chargeIdx = -1;
					placementMode = false;
				}

				//right mouse button changes polarity
				if (Input.GetMouseButtonDown (1)) {
					if (pol == "+") {
						pol = "-";
						world.charges [chargeIdx].Polarity = -1;
					} else {
						pol = "+";
						world.charges [chargeIdx].Polarity = 1;
					}
					//update the world after changing polarity
					world.updateWorld ();
					ArrowController.Instance.updateArrows ();
				}


			}
		}

		if (movementMode == true) {  //drag to move?

		}
	}


	public void setMode_Placement(){
		placementMode = true;
	}

	public void clearCharges(){
		Simulation.Instance.clearCharges ();
		world.updateWorld ();
		ArrowController.Instance.updateArrows ();
	}

	public void pauseSim(){
		bool paused=Simulation.Instance.paused;

		if (paused) {
			paused = false;
			GameObject.Find("PauseButton").GetComponentInChildren<Text>().text="Pause";
		} else {
			paused = true;
			GameObject.Find("PauseButton").GetComponentInChildren<Text>().text="Resume";
		}
		Simulation.Instance.paused = paused;
	}

	public void reset(){
		if (Simulation.Instance.paused == false) {
			pauseSim ();
		}

		MoverController.Instance.moverReset ();
	}
}
