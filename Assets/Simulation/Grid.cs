using UnityEngine;
using System.Collections;

public class Grid {

	//this is the grid class,
	//this stores the grid points on which the electric field
	//will be calculated

	int gridDimX;
	int gridDimY;

	float[,] Efield;
	Vector3[,] Edir;


	//initialize the grid with xdim,ydim points and 
	//set all Efield grid points to zero
	public void initGrid(int xdim, int ydim){
		this.gridDimX = xdim;
		this.gridDimY = ydim;

		this.Efield = new float[xdim, ydim];
		this.Edir = new Vector3[xdim, ydim];

		//initalize all to zero
		for (int i = 0; i < xdim; i++) {
			for (int j = 0; j < ydim; j++) {
				this.Efield [i, j] = 0f;
				this.Edir [i, j] = new Vector3 (0f,0f,0f);
			}
		}
	}

	//getter and setter
	//TODO and updater maybe?

	public float getEfield(int x, int y){
		return Efield [x, y];
	}

	public Vector3 getEdir(int x, int y){
		return Edir [x, y];
	}

	public void setEfield(int x, int y, float field){
		this.Efield [x, y] = field;
	}

	public void setEdir(int x, int y, Vector3 dir){
		this.Edir [x, y] = dir;
	}

	public int GridDimX {
		get {
			return this.gridDimX;
		}
	}

	public int GridDimY {
		get {
			return this.gridDimY;
		}
	}
}
