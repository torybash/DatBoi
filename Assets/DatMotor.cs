using UnityEngine;
using System.Collections;

public class DatMotor : MonoBehaviour {

	[SerializeField] float motorSpeed = 50f;
	[SerializeField] float leanForce = 50f;

	[SerializeField] Rigidbody2D datBoiRB;
	[SerializeField] Transform datBoi;

	[SerializeField] bool DBG_testMobileInput;

	Vector2 inputVec = Vector2.zero;


	private HingeJoint2D hinge;
	private HingeJoint2D Hinge {
		get {
			if (hinge == null) hinge = GetComponent<HingeJoint2D>();
			return hinge;
		}
	}

	void FixedUpdate() {
		inputVec = GetInput();
		float newSpeed = Mathf.Lerp(Hinge.motor.motorSpeed, inputVec.x * motorSpeed, 0.1f);
		Hinge.SetMotorSpeed(newSpeed);
		//var motor = Hinge.motor; motor.motorSpeed = inputVec.x * motorSpeed; Hinge.motor = motor;
		//if (inputVec.x == 0) Hinge.useMotor = false;
		//Hinge.GetComponent<Rigidbody2D>().angularVelocity = -vec.x * motorSpeed;
		//Debug.Log("Hinge torque: " + Hinge.GetMotorTorque(Time.fixedDeltaTime));

		//datBoiRB.AddRelativeForce(new Vector2(vec.y * leanForce, 0));
		datBoiRB.AddTorque(-inputVec.y * leanForce);
		datBoi.localRotation = Quaternion.Euler(0, 0, -40f * inputVec.y);
		//if (vec.y > 0) datBoi.rotation = Quaternion.Euler(0, 0, -40f * vec.y);
		//else if (vec.y < 0) datBoi.rotation = Quaternion.Euler(0, 0, 40f);
		//else datBoi.rotation = Quaternion.Euler(0, 0, 0);
	}

	private Vector2 GetInput() {


		if (Application.isMobilePlatform || DBG_testMobileInput) {
			if (Input.GetMouseButton(0)) inputVec.x = (Input.mousePosition.x - Screen.width / 2f) / (Screen.width / 2f);
			else inputVec.x = 0;
			inputVec.y = Mathf.Lerp(inputVec.y, Mathf.Clamp(Input.acceleration.x * 2f, -1f, 1f), 0.1f); ;
		} else {
			inputVec.x = Input.GetAxisRaw("Horizontal");
			inputVec.y = Input.GetAxisRaw("Vertical");
		}
		
		Debug.Log("GetInput - vec: "+ inputVec + ", Input.acceleration: " + Input.acceleration + ", moustPos dir: " + (Input.mousePosition.x > Screen.width / 2f) + ", mousePos.x: "+ Input.mousePosition.x + ", Screen.width: "+ Screen.width);
		if (Input.touchCount > 0) Debug.Log("GetInput - touches: " + Input.touchCount + ", 1st pos: " + Input.touches[0].position + ", touch dir: " + (Input.touches[0].position.x > Screen.width / 2f));


		return inputVec;
	}
}
