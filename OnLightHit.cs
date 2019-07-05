using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLightHit : MonoBehaviour
{
    public Animator platformRise;

    public GameObject _camera;
    public GameObject _spotlight;
    private GameObject _game_object;

    // Use this for initialization
    void Start()
    {
        platformRise.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // structure used to get information back from a raycase
        RaycastHit _hit;

        // the ray needed to cast from the mouse in screen coordinates
        Ray _ray = _camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        // checks if a object has been detected 
        bool _picked = Physics.Raycast(_ray, out _hit);

        // if a object has been detected
        if (_picked)
        {
            if (_hit.collider.gameObject.name == "Symbol")
            {
                _game_object = _hit.collider.gameObject;

                if(_spotlight.activeSelf == true)
                {
                    Debug.Log("Light is hitting this object");
                    platformRise.enabled = true;
                }
                // play animation here
            }
        }
    }
}
