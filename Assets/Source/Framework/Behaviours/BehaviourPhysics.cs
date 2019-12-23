using UnityEngine;
using System.Collections;

///<summary>
///Physic methods for Rigidibody 2D and 3D
///</summary>
public static class BehaviourPhysics
{
	///<summary>
	///@obj object will moved
	///@dir directions will be set speed
	///@spd speed will set
	///</summary>
	public static void Move(GameObject obj, Vector3 dir, float spd)
	{
		var rb = obj.GetComponent<Rigidbody>();
		var v3 = dir;

		v3 = new Vector3 ((dir.x == 0) ? rb.velocity.x : spd * dir.x,
						  (dir.y == 0) ? rb.velocity.y : spd * dir.y,
						  (dir.z == 0) ? rb.velocity.z : spd * dir.z);

		rb.velocity = v3;
	}

	///<summary>
	///@obj object will forced
	///@dir directions will be set force
	///@frc force will set
	///</summary>
	public static void Force(GameObject obj, Vector3 dir, float frc)
	{
		obj.GetComponent<Rigidbody>().AddForce(dir * frc, ForceMode.Impulse);
	}

	public static void AddTorque(GameObject obj, Vector3 dir,float frc)
	{
		obj.GetComponent<Rigidbody>().AddTorque(dir * frc);
	}

	///<summary>
	///@obj object will be moved
	///@dir directions will be set speed
	///@spd speed will set
	///</summary>
	public static void Move2D(GameObject obj, Vector2 dir, float spd)
    {
		var rb = obj.GetComponent<Rigidbody2D>();
		var v2 = dir;

		v2 = new Vector2 ((dir.x == 0) ? rb.velocity.x : spd * dir.x,
						  (dir.y == 0) ? rb.velocity.y : spd * dir.y);

		rb.velocity = v2;
    }

	///<summary>
	///@obj object will forced
	///@dir directions will be set force
	///@frc force will set
	///</summary>
	public static void Force2D(GameObject obj, Vector2 dir, float frc)
    {
		obj.GetComponent<Rigidbody2D>().AddForce(dir * frc, ForceMode2D.Impulse);
    }

	///<summary>
	///@obj object will torqued
	///@frc force will set
	///</summary>
	public static void AddTorque2D(GameObject obj, float frc)
    {
		obj.GetComponent<Rigidbody2D>().AddTorque(frc);
    }
}
