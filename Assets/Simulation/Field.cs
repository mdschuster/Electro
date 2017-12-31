using UnityEngine;
using System.Collections.Generic;

public static class Field {

	static float coulomb=8.987552f;  //coulomb's constant in mili coulombs


	public static Vector3 computePoint(Vector3 pos,List<Charge> charges){
		int chargeCount = charges.Count;

		float[] cx = new float[chargeCount];
		float[] cy = new float[chargeCount];
		float[] radius = new float[chargeCount];
		float[] field = new float[chargeCount];
		float[] angle = new float[chargeCount];
		float[] vecx = new float[chargeCount];
		float[] vecy = new float[chargeCount];


		for (int c = 0; c < chargeCount; c++) {
			cx [c] = charges [c].Xpos;
			cy [c] = charges [c].Ypos;
			//if charge is right on a display grid point, set to zero
			//distance from grid point to charge
			radius[c] = Mathf.Sqrt ((cx[c] - pos.x) * (cx[c] - pos.x) + (cy[c] - pos.y) * (cy[c] - pos.y));
			//don't allow for the field to be computed at actual strength closer than this:
			if (radius [c] < 2f) {
				radius [c] = 2f;
			}

			//compute field magnitude
			field[c] = coulomb * charges[c].Magnitude / (radius[c] * radius[c]);
			//grid.setEfield (i, j, field);
			float x = 0f;
			float y = 0f;
			//if (charges[c].Polarity==1){

			//vector from test point to charge
			x = cx[c]-pos.x;
			y = cy[c]-pos.y;
			//}
			//if (charges[c].Polarity==-1){
			//	x = i-cx[c];
			//	y = j-cy[c];
			//}
			float factor = 0f;
			//compute field vector
			if (charges[c].Polarity == 1) {
				factor = 180f;
			}
			//angle[c] = Mathf.Atan2 (y, x) * 180f / Mathf.PI;
			//angle [c] = (angle [c]+factor) * Mathf.PI / 180f;//back to radians
			//vecx[c]=Mathf.Cos(angle[c])*charges[c].Polarity;
			//vecy[c]=Mathf.Sin(angle[c])*charges[c].Polarity;

			vecx [c] = x*charges[c].Polarity*-1;
			vecy [c] = y*charges[c].Polarity*-1;

			//normalize
			float norm=Mathf.Sqrt(vecx[c]*vecx[c]+vecy[c]*vecy[c]);
			vecx [c] = vecx [c] / norm*Mathf.Abs(field[c]);
			vecy [c] = vecy [c] / norm*Mathf.Abs(field[c]);

			//float vecx = Mathf.Cos (angle) * field;
			//float vecy = Mathf.Sin (angle) * field;
			//Debug.Log (i+" "+j+" "+vecx+" "+vecy+" "+angle*(180/3.1415927));
			//Vector3 vec = new Vector3 (0, 0, angle);
			//grid.setEdir (i, j, vec);

		}

		//final reduction of the field and vector
		float totField = 0f;
		float totAngle = 0f;
		float totvecX = 0f;
		float totvecY = 0f;
		for (int c = 0; c < chargeCount; c++) {
			totField = totField + field [c];
			totvecX = totvecX + vecx [c];
			totvecY = totvecY + vecy [c];
		}
		Vector3 finalField=new Vector3(totvecX,totvecY,0f);
		finalField = finalField * totField;
		return finalField;

	}








	public static void computeField(Grid grid, List<Charge> charges){
		//computes the efield at each grid point for all charges in the list of charges
		//FIXME only one charge at the moment

		int chargeCount = charges.Count;

		//Debug.Log (chargeCount);

		//compute distance from grid point to charges
		//FIXME allow for more than one charge
		float[] cx = new float[chargeCount];
		float[] cy = new float[chargeCount];
		float[] radius = new float[chargeCount];
		float[] field = new float[chargeCount];
		float[] angle = new float[chargeCount];
		float[] vecx = new float[chargeCount];
		float[] vecy = new float[chargeCount];
		int dimx = grid.GridDimX;
		int dimy = grid.GridDimY;

		for (int i = 0; i < dimx; i++) {
			//float x=cx-i;
			for (int j = 0; j < dimy; j++) {


				for (int c = 0; c < chargeCount; c++) {
					cx [c] = charges [c].Xpos;
					cy [c] = charges [c].Ypos;
					//if charge is right on a display grid point, set to zero
					if (cx[c] == i && cy[c] == j) {
						//grid.setEfield (i, j, 0f);
						//grid.setEdir (i, j, new Vector3 (0, 0, 0));
						//Debug.Log ("skipping " + i + " " + j+" "+grid.getEfield(i,j));
					} else {
						//distance from grid point to charge
						radius[c] = Mathf.Sqrt ((cx[c] - i) * (cx[c] - i) + (cy[c] - j) * (cy[c] - j));

						//compute field magnitude
						field[c] = coulomb * charges[c].Magnitude / (radius[c] * radius[c]);
						//grid.setEfield (i, j, field);
						float x = 0f;
						float y = 0f;
						//if (charges[c].Polarity==1){

						//vector from test point to charge
							x = cx[c]-i;
							y = cy[c]-j;
						//}
						//if (charges[c].Polarity==-1){
						//	x = i-cx[c];
						//	y = j-cy[c];
						//}
						float factor = 0f;
						//compute field vector
						if (charges[c].Polarity == 1) {
							factor = 180f;
						}
						//angle[c] = Mathf.Atan2 (y, x) * 180f / Mathf.PI;
						//angle [c] = (angle [c]+factor) * Mathf.PI / 180f;//back to radians
						//vecx[c]=Mathf.Cos(angle[c])*charges[c].Polarity;
						//vecy[c]=Mathf.Sin(angle[c])*charges[c].Polarity;

						vecx [c] = x*charges[c].Polarity*-1;
						vecy [c] = y*charges[c].Polarity*-1;

						//normalize
						float norm=Mathf.Sqrt(vecx[c]*vecx[c]+vecy[c]*vecy[c]);
						vecx [c] = vecx [c] / norm*Mathf.Abs(field[c]);
						vecy [c] = vecy [c] / norm*Mathf.Abs(field[c]);

						//float vecx = Mathf.Cos (angle) * field;
						//float vecy = Mathf.Sin (angle) * field;
						//Debug.Log (i+" "+j+" "+vecx+" "+vecy+" "+angle*(180/3.1415927));
						//Vector3 vec = new Vector3 (0, 0, angle);
						//grid.setEdir (i, j, vec);
					}
				}
				//final reduction of the field and vector
				float totField = 0f;
				float totAngle = 0f;
				float totvecX = 0f;
				float totvecY = 0f;
				for (int c = 0; c < chargeCount; c++) {
					totField = totField + field [c];
					totvecX = totvecX + vecx [c];
					totvecY = totvecY + vecy [c];
					//if (i == 7 && j == 8) {
					//	Debug.Log ("7,8: " + c + " " + vecx [c]+" "+ vecy [c]+" "+field[c]+" "+charges[c].Polarity);
					//}
				}
				totAngle = Mathf.Atan2 (totvecY, totvecX) * 180f / Mathf.PI;
				grid.setEfield (i, j, totField);
				Vector3 vec = new Vector3 (0, 0, totAngle);
				grid.setEdir (i, j, vec);
			}
			
		}
	}

}
