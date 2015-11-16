using UnityEngine;
using System.Collections;

public class RigidBodyPhys : MonoBehaviour {

	public Vector3 position;
	public Vector3 velocity;
	public Vector3 force;
	public float bounciness = 1.0f;
	public float staticFriction = 0.8f;
	public float kineticFriction = 0.5f;
	public float mass = 1;

	private float dt;

	// Use this for initialization
	void Start () {
	
		position = gameObject.transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		dt = Time.deltaTime;

		float weight = mass * 9.81f;

		Vector3 frictionForce;

		if(velocity.magnitude == 0) //static object, use static friction coefficient
		{
			frictionForce = -force.normalized * staticFriction * weight;

			if(force.magnitude < frictionForce.magnitude)
			{
				//if the force cant overcome the frictional force
				force = new Vector3(0,0,0);
			}
			else
			{
				AddForce(frictionForce);
			}
		}
		else //moving
		{
			frictionForce = -velocity.normalized * kineticFriction * weight;
	
			if(GetAcceleration(frictionForce).magnitude >= velocity.magnitude) //check if friction force is enough to stop the object
			{
				velocity = new Vector3(0,0,0);
			}
			else
			{
				AddForce(frictionForce); //if friction cant stop the object, add force to slow down./
			}
		}

		Integrate();
	}


	public Vector3 GetAcceleration(Vector3 f)
	{
		return (f / mass) * dt;
	}

	public void Integrate()
	{
		velocity += GetAcceleration(force);
		position += velocity * dt;

		force = new Vector3(0,0,0);

		gameObject.transform.position = position;
	}

	public void AddForce(Vector3 f)
	{
		force += f;
	}
}
