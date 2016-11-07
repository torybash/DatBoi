using UnityEngine;
using System.Collections;

public static class PhysicsExtends {

	public static void SetMotorSpeed(this HingeJoint2D joint, float motorSpeed) {
		var motor = joint.motor;
		motor.motorSpeed = motorSpeed;
		joint.motor = motor;
	}
}

public static class VectorExtends {

	public static void SetX(this Vector3 vec, float x) { vec.x = x; }
	public static void SetY(this Vector3 vec, float y) { vec.y = y; }
	public static void SetZ(this Vector3 vec, float z) { vec.z = z; }

	public static void SetX(this Vector2 vec, float x) { vec.x = x; }
	public static void SetY(this Vector2 vec, float y) { vec.y = y; }
}

public static class ArrayExtends {

	public static string ToStr<T>(this T[] arr) {
		if (arr == null) return "NULL";
		var str = "";
		for (int i = 0; i < arr.Length; i++) {
			str += arr[i];
			if (i != arr.Length - 1) str += ", ";
		}
		return str;
	}
}