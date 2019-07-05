using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

	private int timer;

	// Use this for initialization
	void Start () {
		timer = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		timer++;

		if(timer >= 500)
			SceneManager.LoadScene("Mansion1stFloor");
	}
}
