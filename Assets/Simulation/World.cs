using UnityEngine;
using System.Collections.Generic;

public class World {

	public Grid worldGrid;

	public List<Charge> charges;

	public List<Mover> movers;


	int gridX=20;
	int gridY=20;

	public void initWorld(){
		//initialize the field
		//create the grid
		this.worldGrid=new Grid();
		this.worldGrid.initGrid(gridX,gridY);
		charges = new List<Charge> ();
		movers = new List<Mover> ();

		//create the main mover and place it on the grid
		movers.Add(new Mover());
		movers [0].init ();



		//createCharge (new Charge (10f, 10f, "+", 1f));
		//createCharge (new Charge (5f, 5f, "-", 1f));
		//createCharge (new Charge (12f, 3f, "+", 1f));

		//charges [0].Perm = true;
		//charges [1].Perm = true;
		//charges [2].Perm = true;

		//remove charge test
		//Debug.Log("positions: "+" "+charges[0].ListPos+" "+charges[1].ListPos+" "+charges[2].ListPos);
		//removeCharge(0);
		//
		//Debug.Log("positions: "+" "+charges[0].ListPos+" "+charges[1].ListPos);
		//removeCharge (0);
	}

	public void updateWorld(){
		Field.computeField (worldGrid, charges);
		//Debug.Log("Efield at 4,3: "+worldGrid.getEfield(4,3)+" N/mC");
		//Debug.Log ("Efield Dir at 4,3: " + worldGrid.getEdir (4, 3));
	}

	public int createCharge(Charge charge){
		int idx = charges.Count;
		charges.Add (charge);
		charge.ListPos = idx;
		return charge.ListPos;
	}

	public void removeCharge(int idx){
		//remove item from list at position idx
		charges.RemoveAt (idx);
		//shift the list position of each charge with greater than the
		//removed index down by one. This way the charge still knows
		//it's correct position in the queue
		for (int c = 0; c < charges.Count; c++) {
			if (charges [c].ListPos > idx) {
				charges [c].ListPos -= 1;
			}
		}
	}

	public void clearCharges(){
		charges.Clear ();
	}

	public int GridX {
		get {
			return this.gridX;
		}
	}

	public int GridY {
		get {
			return this.gridY;
		}
	}
}
