using UnityEngine;
using System.Collections;

public class Charge {

	int polarity;
	float magnitude;

	float xpos;
	float ypos;

	int listPos;
	public bool perm;

	public Charge (float xpos, float ypos,string pol,float mag){
		this.magnitude = mag;
		this.xpos = xpos;
		this.ypos = ypos;
		this.perm = false;   //default to not static charge (can be removed by mouse control)
		if (pol == "+") {
			this.polarity = 1;
		} else if (pol == "-") {
			this.polarity = -1;
		} else {
			this.polarity = 0;
			Debug.Log ("Neutral Particle?");
		}

		//Debug.Log ("Charge Created at " + xpos + " " + ypos);

	}

	public int Polarity {
		get {
			return polarity;
		}
		set {
			polarity = value;
		}
	}


	public float Magnitude {
		get {
			return magnitude;
		}
	}

	public float Xpos {
		get {
			return xpos;
		}
		set {
			xpos = value;
		}
	}

	public float Ypos {
		get {
			return ypos;
		}
		set {
			ypos = value;
		}
	}

	public int ListPos {
		get {
			return listPos;
		}
		set {
			listPos = value;
		}
	}

	public bool Perm {
		get {
			return perm;
		}
		set {
			perm = value;
		}
	}
}
