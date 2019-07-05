using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {

    private int timer;
    public int delay;

    public string _scene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer++;

        if(timer >= delay)
        {
            SceneManager.LoadScene(_scene);
        }
	}
}
