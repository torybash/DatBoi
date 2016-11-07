using UnityEngine;
using System.Collections;



[RequireComponent(typeof(Rigidbody2D))]
public class DatMovement : MonoBehaviour {

	private Rigidbody2D rigiB;
	private Rigidbody2D RigiB {
		get {
			if (rigiB == null) rigiB = GetComponent<Rigidbody2D>();
			return rigiB;
		}
	}

	[SerializeField] Transform[] parts;

	[SerializeField] private float wheelSpeed = 1000;
	[SerializeField] private float partsMotorSpeed = 100;
	[SerializeField] private float partsResetMotorSpeed = 50;




	void FixedUpdate() {
		var input = GetInput();
		RigiB.AddTorque(-input.x * wheelSpeed);

		for (int i = 0; i < parts.Length; i++) {
			var part = parts[i];
			var joint = part.GetComponent<HingeJoint2D>();
			
			//Debug.Log("part.rotation: " + part.localRotation.eulerAngles.z + ", Mathf.Sign(Mathf.DeltaAngle(part.localRotation.eulerAngles.z, 0)): "+ Mathf.Sign(Mathf.DeltaAngle(part.localRotation.eulerAngles.z, 0)));
			float motorSpeed = 0;
			if (Mathf.Abs(input.y) > 0) motorSpeed = input.y * partsMotorSpeed;
			else motorSpeed = Mathf.Clamp(Mathf.DeltaAngle(part.localRotation.eulerAngles.z, 0), -1, 1) * partsResetMotorSpeed;
			joint.SetMotorSpeed(motorSpeed);
		}
	}


	private Vector2 GetInput() {
		var vec = Vector2.zero;
		Debug.Log("GetInput - Input.acceleration: " + Input.acceleration + ", moustPos dir: "+ (Input.mousePosition.x > Screen.width / 2f));
		if (Input.touchCount > 0) Debug.Log("GetInput - touches: " + Input.touchCount + ", 1st pos: "+ Input.touches[0].position + ", touch dir: "+ (Input.touches[0].position.x > Screen.width / 2f));
		

		if (Application.isMobilePlatform) {
			vec.x = (Screen.width / 2f - Input.mousePosition.x) / Screen.width / 2f;
			vec.y = Input.acceleration.x;
		} else {
			vec.x = Input.GetAxis("Horizontal");
			vec.y = Input.GetAxis("Vertical");
		}
		

		return vec;
	}
}


