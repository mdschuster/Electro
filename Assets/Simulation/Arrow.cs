using UnityEngine;
using System.Collections;

public class Arrow {

	float scale;
	float angle;

	int xpos;
	int ypos;


	public void init(int x, int y, float s, float a){
		xpos = x;
		ypos = y;
		scale = s;
		angle = a;
	}

	public float Scale {
		get {
			return scale;
		}
		set {
			scale = value;
		}
	}

	public float Angle {
		get {
			return angle;
		}
		set {
			angle = value;
		}
	}

	public int Xpos {
		get {
			return xpos;
		}
		set {
			xpos = value;
		}
	}

	public int Ypos {
		get {
			return ypos;
		}
		set {
			ypos = value;
		}
	}
}
