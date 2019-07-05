using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {

    private int timer;

    public Camera _camera01;
    public Camera _camera02;

    public int delay;

    // Use this for initialization
    void Start () {
        _camera01.enabled = true;
        _camera02.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        timer++;

        if(timer >= delay)
        {
            _camera01.enabled = false;
            _camera02.enabled = true;
        }
	}
}
