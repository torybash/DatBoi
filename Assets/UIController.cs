using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {

	[SerializeField] Text gravText;

	void OnEnable() {
		gravText.text = "Gravity: " + Mathf.Abs(Physics2D.gravity.y);
	}

	public void ClickedRestart() {
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	public void SetGravity(float val) {
		Debug.Log("SetGravity - val: " + val);
		//Physics2D.gravity.SetY(val); ;
		Physics2D.gravity = new Vector2(0, val);
		gravText.text = "Gravity: " + Mathf.Abs(Physics2D.gravity.y);
	}
}
