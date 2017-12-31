using UnityEngine;
using System.Collections.Generic;

public class Simulation : MonoBehaviour {

	public static Simulation Instance { get; protected set; }

	//public ArrowController arrowController;
	public World world { get; protected set; }
	float[] gridMap;

	//FIXME move all of the charge prefab stuff to it's own contorller
	public GameObject chargePrefab;
	public Dictionary<string,GameObject> chargeDict;


	public bool paused;

	// Use this for initialization
	void Start () {

		paused = true;
		Instance = this;
		world = new World ();
		world.initWorld ();
		world.updateWorld ();
		gridMap = new float[world.GridX];
		createMap ();


		//getting the instance of the arrowcontroller class from the controller gameobject
		//arrowController = GameObject.Find ("Controllers").GetComponent<ArrowController>(); 
		//make sure that the arrowcontroller gets it's instance set before calling anything else
		//I guess this routine has become the defacto starter for everything
		GameObject.Find ("Controllers").GetComponent<ArrowController>().setInstance();
		ArrowController.Instance.drawArrows();

		chargeDict = new Dictionary<string,GameObject> ();

		GameObject.Find ("Controllers").GetComponent<MoverController>().setInstance();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//place where the map from sim coords to world coords is setup
	void createMap(){
		float screenRatio= (float) Screen.width / (float) Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;

		//widthOrtho is the width of the screen to the center
		float sizex=widthOrtho*2f;
		float unitx = sizex/world.GridX;
		float sizey=widthOrtho*2f;
		float unity = world.GridY/sizey;
		//Debug.Log (unitx+" "+sizex);
		for (int i = 0; i < world.GridX; i++) {
			gridMap[i]=-widthOrtho+i*unitx;
		}
	}

	public float getGridMap(int x){
		return gridMap [x];
	}

	public int addCharge(float x, float y, string pol, float mag,float curx, float cury){
		int idx=world.createCharge (new Charge (x, y, pol, mag));
		//instantiate
		Vector3 pos = new Vector3(curx,cury,0f);
		GameObject charge_go=(GameObject) Instantiate (chargePrefab, pos, transform.rotation);
		charge_go.transform.SetParent (this.transform, true);
		charge_go.GetComponent<SpriteRenderer> ().sortingOrder = 5;
		chargeDict.Add (idx.ToString(),charge_go);
		return idx;

	}

	public void deleteCharge(int idx){
		world.removeCharge (idx);
		//deinstantiate
		Destroy(chargeDict[idx.ToString()]);
	}

	public void clearCharges(){
		world.clearCharges ();
		//deinstantiate
		foreach (KeyValuePair<string,GameObject> charge in chargeDict) {
			Destroy (charge.Value);
		}
		//remove all items from dictionary
		chargeDict.Clear ();

	}

}
