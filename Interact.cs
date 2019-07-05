using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Interact : MonoBehaviour {

    public Text _itemInfo;
    
    public GameObject _camera;
    public float  _distance;

    private GameObject _game_object;
    private bool       _carry;

    public Animator door_anim;

    public GameObject recipe;
    public GameObject TV;

    void carry(GameObject obj)
    {
        // use rigid body physics to pickup the object when selected
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.GetComponent<Rigidbody>().transform.position = _camera.transform.position + _camera.transform.forward * _distance;
    }

    void drop(GameObject obj)
    {
        // reset the FOV
        _camera.GetComponent<Camera>().fieldOfView = 75.5f;

        // use rigid body physics to drop the object on key press
        _carry = false;
        obj.GetComponent<Rigidbody>().isKinematic = false;  
    }

    void enableZoomFunctionality(Ray ray, GameObject obj)
    {
        // the amount of zoom is based on the movement of the mouse scroll wheel
        float zoom_speed = 525.0f;
        float zoomDistance = zoom_speed * Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime;

        // Zoom in the camera by incrementing its field of view
        _camera.GetComponent<Camera>().fieldOfView -= zoomDistance;
    }

    void enableRotationControls(GameObject obj)
    {
        float rotation_speed = 30.0f;

        // the following key binds are temporary
        if (Input.GetKey(KeyCode.Z)) { obj.transform.Rotate(Vector3.forward * rotation_speed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.X)) { obj.transform.Rotate(Vector3.up * rotation_speed * Time.deltaTime); }
        if (Input.GetKey(KeyCode.C)) { obj.transform.Rotate(Vector3.right * rotation_speed * Time.deltaTime); }
    }

    // Use this for initialization
    void Start ()
    {
        recipe.SetActive(false);

        // get the main camera in the scene
        _camera = GameObject.FindGameObjectWithTag("MainCamera");

        door_anim.enabled = false;
    }

	// Update is called once per frame
	void Update ()
    {
        string _pickup_tag = "Interactable";

        // structure used to get information back from a raycase
        RaycastHit _hit;

        // the ray needed to cast from the mouse in screen coordinates
        Ray _ray = _camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (Input.GetKey(KeyCode.E))
        {
            // checks if a object has been detected 
            bool _picked = Physics.Raycast(_ray, out _hit);

            // if a object has been detected
            if (_picked)
            {
                if (_hit.collider.gameObject.tag == _pickup_tag)
                {
                    _game_object = _hit.collider.gameObject;

                    _carry = true;
         
                    Debug.Log("Object selected!");
                }
            }
        }

        // Draw the ray to test
       // Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
       // Debug.DrawRay(_ray.origin, _ray.direction, Color.red); 

        if (_carry == true)
        {
            if(_game_object.name == "SecretRoom_Lever")
            {
                door_anim.enabled = true;
            }

            carry(_game_object);

            enableRotationControls(_game_object);
            enableZoomFunctionality(_ray, _game_object);

	    if(_game_object.name == "Recipe")
	    {
		if(Input.GetKey(KeyCode.R))
		{
			recipe.SetActive(true);
			Destroy(GameObject.Find("Recipe"));
		}
	    }

            if (Input.GetKey(KeyCode.I))
            {
                _itemInfo.enabled = true;

                switch (_game_object.name)
                {
                    case "Teddy":
                        SceneManager.LoadScene("Forest");
                        break;
                    case "SecretCode":
                        _itemInfo.text = "Note that reads, '213'";
                        break;
                    case "Batterys":
                        _itemInfo.text = "Batterys for a flashlight";
                        break;
                    case "Bear":
                        _itemInfo.text = "Seems like this teddy belongs to a previous owner. \n Likely the missing family.";
                        break;
                    case "Globe":
                        _itemInfo.text = "Old Dusty Globe. Not used for ages";
                        break;
                    case "DeskLamp":
                        _itemInfo.text = "Lamp desk with a broken bulb. \n Wonder if something happened.";
                        break;
                    case "BlackwoodMansionBook":
                        _itemInfo.text = "A book about out of body experiences. \n Maybe Jones was trying connect with te spirits \n in another demension";
                        break;
                    case "Key":
                        _itemInfo.text = "Key for the elevator";
                        break;
                    case "Newspaper":
                        _itemInfo.text = "Newspaper headline reads... 'Mystery at Blackwood, Truth or Lie?'";
                        break;
                    case "Briefcase":
                        _itemInfo.text = "Briefcase, most likely belongs to Howard'";
                        break;
                    default:
                        break;
                }
            }
            if(Input.GetKey(KeyCode.Q))
            {
                drop(_game_object);
                _itemInfo.enabled = false;
            }
        }
	}
}
