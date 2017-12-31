using UnityEngine;
using System.Collections;

public class Mover {

	float mass;

	Vector3 acceleration;
	Vector3 velocity;
	Vector3 position;

	public void init(){
		position.x=1f;
		position.y=10f;
		mass = 0.1f;
	}


	public void applyForce(Vector3 force){
		acceleration = acceleration + force / mass;   //F=ma
		//maximum acceleration
		//if (acceleration.magnitude > 0.25f) {
		//	acceleration.Normalize ();
		//	acceleration = acceleration * 0.25f;
		//}
	}


	public void update(){

		position = position + Time.fixedDeltaTime * (velocity + Time.fixedDeltaTime*acceleration/2f ); //update position based on velocity

		velocity = velocity + Time.fixedDeltaTime*acceleration; //update velocity based on acceleration
		//if (velocity.magnitude > 3f) {
		//	velocity.Normalize ();
		//	velocity = velocity * 3f;
		//}

		acceleration = new Vector3 (0f, 0f, 0f);  //zero out the acceleration
		checkBoundry();
	}

	void checkBoundry(){
		if (Mathf.Abs(position.x) > 25f || Mathf.Abs(position.y) > 25f) {
			position.x = 1f;
			position.y = 10f;
			velocity.x = 0f;
			velocity.y = 0f;
		}
	}


	public void reset(){
		position.x = 1f;
		position.y = 10f;
		velocity.x = 0f;
		velocity.y = 0f;
	}







	public Vector3 Position {
		get {
			return position;
		}
	}
}
