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

	[SerializeField] private float wheelSpeed = 10;

	private float xIn;

	
	void Update () {
		InputMovement();
	}

	void FixedUpdate() {
		RigiB.AddTorque(-xIn * wheelSpeed);
	}

	private void InputMovement() {

		xIn = Input.GetAxis("Horizontal");

		
	}
}
