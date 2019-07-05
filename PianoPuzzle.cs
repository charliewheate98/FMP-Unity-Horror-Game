using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PianoPuzzle : MonoBehaviour
{

    private bool _pkey1_active;
    private bool _pkey2_active;
    private bool _pkey3_active;

    public GameObject _camera;
    private GameObject _game_object;

    private GameObject _cinematic1_trig;

    private AudioSource _source;
    public AudioClip _clip_tune_01;
    public AudioClip _clip_tune_02;
    public AudioClip _clip_tune_03;
    public AudioClip _paper;
    public AudioClip _bgm;

    private GameObject entry1;

    private GameObject teddy;
    private GameObject recipeDisplay;

    public Animator unlockPainting;

    public Text _text;

    private int timer;

    private bool noteCollected;

    public AudioClip _notification;

    public GameObject storyUpdate;
    private int storyDuration;

    public GameObject ElevatorKey;

    // Use this for initialization
    void Start()
    {
        ElevatorKey.SetActive(false);

        storyDuration = 0;
        timer = 0;

        storyUpdate.SetActive(false);

        noteCollected = false;
        _source = GetComponent<AudioSource>();

        unlockPainting.enabled = false;

        _text.text = "";

        entry1 = GameObject.Find("Entry1");
        entry1.SetActive(false);

        teddy = GameObject.Find("Teddy");
        recipeDisplay = GameObject.Find("RecipeDisplay");
        recipeDisplay.SetActive(false);

        _pkey1_active = false;
        _pkey2_active = false;
        _pkey3_active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (recipeDisplay.activeSelf)
        {
            if (Input.GetKey(KeyCode.E))
                recipeDisplay.SetActive(false);
        }

        // structure used to get information back from a raycase
        RaycastHit _hit;

        // the ray needed to cast from the mouse in screen coordinates
        Ray _ray = _camera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        if (noteCollected == true && recipeDisplay.activeSelf == false)
        {
            timer++;
        }

        if (storyDuration >= 300)
        {
            storyUpdate.SetActive(false);
        }

        if (timer == 400)
        {
                _source.PlayOneShot(_notification, 0.3f);

                ElevatorKey.SetActive(true);

                storyDuration++;
                storyUpdate.SetActive(true);

                _text.text = "";
                _source.PlayOneShot(_paper, 5.0f);
        }

        if (entry1.activeSelf)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                entry1.SetActive(false);
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            // checks if a object has been detected 
            bool _picked = Physics.Raycast(_ray, out _hit);

            // if a object has been detected
            if (_picked)
            {
                if (_hit.collider.gameObject.name == "Recipe_Note")
                {
                    noteCollected = true;

                    recipeDisplay.SetActive(true);
                    _source.PlayOneShot(_paper, 5.0f);

                    teddy.transform.position = new Vector3(980.786f, 440.447f, 2.033f);
                }

                if (_hit.collider.gameObject.name == "PKey_01")
                {
                    Debug.Log("Key 1 Pressed");

                    _pkey1_active = true;

                    _game_object = _hit.collider.gameObject;

                    if (_pkey2_active == true)
                        _source.PlayOneShot(_clip_tune_01, 1.0f);

                    Debug.Log("Object selected!");
                }

                if (_hit.collider.gameObject.name == "PKey_02")
                {
                    Debug.Log("Key 2 Pressed");

                    _pkey2_active = true;

                    _game_object = _hit.collider.gameObject;

                    _source.PlayOneShot(_clip_tune_02, 1.0f);

                    Debug.Log("Object selected!");
                }
                if (_hit.collider.gameObject.name == "PKey_03")
                {
                    Debug.Log("Key 3 Pressed");

                    _pkey3_active = true;

                    _game_object = _hit.collider.gameObject;

                    if (_pkey2_active == true && _pkey1_active == true)
                    {
                        unlockPainting.enabled = true;

                        Destroy(GameObject.Find("Secret_Door"));
                        _source.PlayOneShot(_clip_tune_03, 1.0f);
                        //_source.PlayOneShot(_bgm, 5.0f);
                    }

                    Debug.Log("Object selected!");
                }
            }
        }
    }
}
