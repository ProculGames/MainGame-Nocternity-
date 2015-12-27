using UnityEngine;
using System.Collections;

public class cameraSystem : MonoBehaviour {
    public GameObject thirdPerson;
    public GameObject firstPerson;

    public bool toggleCamera;

	// Use this for initialization
	void Start () {
        firstPerson.gameObject.SetActive(false);
        thirdPerson.gameObject.SetActive(true);
        toggleCamera = false;
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.F5))
        {
            if (toggleCamera)
            {
                firstPerson.gameObject.SetActive(true);
                thirdPerson.gameObject.SetActive(false);


            }
            else
            {
                firstPerson.gameObject.SetActive(false);
                thirdPerson.gameObject.SetActive(true);


            }
            toggleCamera = !toggleCamera;
        }
	
	}
}
