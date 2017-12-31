using UnityEngine;
using System.Collections.Generic;

public class ArrowController : MonoBehaviour {

	public static ArrowController Instance { get; protected set; }

	//public Sprite arrow;
	public GameObject arrowPrefab;

	//public Arrow[,] arrows;
	Dictionary<string,GameObject> arrowDict;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
	
	}

	public void setInstance(){
		Instance = this;
		arrowDict = new Dictionary<string,GameObject> ();
	}
		

	public void drawArrows(){

		//find largest efield to get the correct normalization scale
		float norm=1f;
		float field = 0f;
		for (int i = 0; i < Simulation.Instance.world.GridX; i++) {
			for (int j = 0; j < Simulation.Instance.world.GridY; j++) {
				if (Mathf.Abs(Simulation.Instance.world.worldGrid.getEfield (i, j)) > Mathf.Abs(field)) {
					field = Simulation.Instance.world.worldGrid.getEfield (i, j);
				}
			}
		}
		norm = field/10f;
		//if norm is zero for some reason, set to 1 to avoid NAN
		if (norm == 0f) {
			norm = 1f;
		}
		Vector3 pos;
		for (int i = 0; i < Simulation.Instance.world.GridX; i++) {
			for (int j = 0; j < Simulation.Instance.world.GridY; j++) {
				float xpos=Simulation.Instance.getGridMap(i);
				float ypos=Simulation.Instance.getGridMap(j);
				pos=new Vector3(xpos,ypos,0f);
				GameObject arrow_go=(GameObject) Instantiate (arrowPrefab, pos, transform.rotation);
				arrow_go.name = "Arrow_" + i + "_" + j;
				arrow_go.transform.SetParent (this.transform, true);
				arrow_go.GetComponent<SpriteRenderer> ().sortingOrder = 1;
				//fill dictionary map
				arrowDict.Add(arrow_go.name,arrow_go);

				//float zangle = Mathf.Atan2 (this.world.worldGrid.getEdir (i, j).y, this.world.worldGrid.getEdir (i, j).x)
				arrow_go.transform.localEulerAngles = new Vector3 (0,0,Simulation.Instance.world.worldGrid.getEdir(i,j).z-90); // -90 because arrow graphic starts pointing up (at 90)
				float scale=Simulation.Instance.world.worldGrid.getEfield(i,j)/norm;
				if (Mathf.Abs(scale) > 0.5f) {
					scale = 0.5f;
				} else if (Mathf.Abs(scale) < 0.1f) {
					scale = 0.1f;
				}

				//scale arrows to a size based on their efield magnitude
				arrow_go.transform.localScale=new Vector3(scale,scale,1f);
			}
		}
	}


	public void updateArrows(){

		float norm=computeSmallestField ();

		GameObject arrow_go;

		for (int i = 0; i < Simulation.Instance.world.GridX; i++) {
			for (int j = 0; j <Simulation.Instance.world.GridY; j++) {
				arrow_go = arrowDict ["Arrow_" + i + "_" + j];
				//float zangle = Mathf.Atan2 (this.world.worldGrid.getEdir (i, j).y, this.world.worldGrid.getEdir (i, j).x)
				arrow_go.transform.localEulerAngles = new Vector3 (0,0,Simulation.Instance.world.worldGrid.getEdir(i,j).z-90); // -90 because arrow graphic starts pointing up (at 90)
				float scale=0f;
				if (norm < 0.01) {
					scale = 0f;
				} else if (norm > 10f) {
					scale = 1f;
				}else{
					scale = Simulation.Instance.world.worldGrid.getEfield (i, j);
				}
				if (Mathf.Abs(scale) > 0.5f) {
					scale = 0.5f;
				} else if (Mathf.Abs(scale) < 0.1f) {
					scale = 0.1f;
				}

				//scale arrows to a size based on their efield magnitude
				arrow_go.transform.localScale=new Vector3(scale,scale,1f);
			}
		}

	}


	float computeSmallestField(){
		//find largest efield to get the correct normalization scale
		float norm=1f;
		float field = 10000f;
		for (int i = 0; i < Simulation.Instance.world.GridX; i++) {
			for (int j = 0; j < Simulation.Instance.world.GridY; j++) {
				if (Mathf.Abs(Simulation.Instance.world.worldGrid.getEfield (i, j)) < Mathf.Abs(field)) {
					field = Simulation.Instance.world.worldGrid.getEfield (i, j);
				}
			}
		}
		norm = field;
		return norm;
	}
}
