using UnityEngine;
using System.Collections.Generic;

public class MoverController : MonoBehaviour {


	public static MoverController Instance { get; protected set; }

	//public Sprite arrow;
	public GameObject moverPrefab;

	//public Arrow[,] arrows;
	Dictionary<string,GameObject> moverDict;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){

		if (Simulation.Instance.paused == false) {
			updateMovers ();
		}
	}


	void updateMovers(){
		//compute foce of electric field at position of mover
		Vector3 pos=new Vector3();
		Vector2 force = new Vector3 ();
		pos = Simulation.Instance.world.movers [0].Position;

		force=Field.computePoint (pos, Simulation.Instance.world.charges);

		Simulation.Instance.world.movers [0].applyForce (force);
		Simulation.Instance.world.movers [0].update ();

		//update graphics
		pos=Simulation.Instance.world.movers [0].Position;


		float screenRatio= (float) Screen.width / (float) Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;

		//widthOrtho is the width of the screen to the center
		float sizex=widthOrtho*2f;
		float unitx = sizex/Simulation.Instance.world.GridX;
		float sizey=widthOrtho*2f;
		float unity = Simulation.Instance.world.GridY/sizey;

		moverDict ["Mover"].transform.position = pos*unitx-new Vector3(widthOrtho,widthOrtho,0f);
	}


	public void setInstance(){
		Instance = this;
		moverDict = new Dictionary<string,GameObject> ();
		createMover ();
	}

	public void createMover(){

		float screenRatio= (float) Screen.width / (float) Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;

		//widthOrtho is the width of the screen to the center
		float sizex=widthOrtho*2f;
		float unitx = sizex/Simulation.Instance.world.GridX;
		float sizey=widthOrtho*2f;
		float unity = Simulation.Instance.world.GridY/sizey;

		GameObject mover_go=(GameObject) Instantiate (moverPrefab, Simulation.Instance.world.movers[0].Position*unitx-new Vector3(widthOrtho,widthOrtho,0f) , transform.rotation);
		mover_go.name = "Mover";
		mover_go.transform.SetParent (this.transform, true);
		mover_go.GetComponent<SpriteRenderer> ().sortingOrder = 3;
		//fill dictionary map
		moverDict.Add(mover_go.name,mover_go);

		//scale arrows to a size based on their efield magnitude
		mover_go.transform.localScale=new Vector3(0.2f,0.2f,1f);
	}


	public void moverReset(){
		float screenRatio= (float) Screen.width / (float) Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;
		float sizex=widthOrtho*2f;
		float unitx = sizex/Simulation.Instance.world.GridX;
		float sizey=widthOrtho*2f;
		float unity = Simulation.Instance.world.GridY/sizey;

		Simulation.Instance.world.movers [0].reset ();
		moverDict ["Mover"].transform.position = Simulation.Instance.world.movers[0].Position*unitx-new Vector3(widthOrtho,widthOrtho,0f);
	}

}
