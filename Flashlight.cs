using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour {

    private bool batterys_collected;
    public GameObject batterys;
    public GameObject batterys_text;

    public Camera _camera;
 
    private GameObject _game_object;
    private bool _light;
    public GameObject light;

    private bool collected;

    private int timer;

    private AudioSource _source;
    public AudioClip _flashlightOn;
    public AudioClip _flashlightOff;

    public GameObject flinstructions;

	// Use this for initialization
    void Start ()
    {
	timer = 0;

	flinstructions.SetActive(false);

	_source = GetComponent<AudioSource>();
        _light = false;     
	collected = false;
	batterys_collected = false;
    	batterys_text.SetActive(true);
    }
	
	// Update is called once per frame
    void Update ()
    {
	// structure used to get information back from a raycase
        RaycastHit _hit;

        // the ray needed to cast from the mouse in screen coordinates
        Ray _ray = _camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            // checks if a object has been detected 
            bool _picked = Physics.Raycast(_ray, out _hit);

            // if a object has been detected
            if (_picked)
            {
		if (_hit.collider.gameObject.name == "Batterys")
                {
                    _game_object = _hit.collider.gameObject;

		    batterys_collected = true;

                    Destroy(_game_object);
                }
            }
        }

	if(batterys_collected == true) 
	{
		timer++;
		batterys_text.SetActive(false);	
		flinstructions.SetActive(true);
	}

	if(timer >= 300)
	{
		timer = 400;
		flinstructions.SetActive(false);
	}

        if (_light == false)
        {
            light.SetActive(false);
        }
        else if (_light == true && batterys_collected == true)
        {
            light.SetActive(true); 
        }

        if (Input.GetMouseButtonDown(0)) 
	{
		_light = true;
		if(batterys_collected == true) _source.PlayOneShot(_flashlightOn, 5.0f);	
	}
        if (Input.GetMouseButtonDown(1)) 
	{
		_light = false;
		if(batterys_collected == true) _source.PlayOneShot(_flashlightOff, 5.0f);
	}
    }
}
