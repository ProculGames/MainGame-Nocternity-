using UnityEngine;
using System.Collections;

public class ThirdPersonCam : MonoBehaviour {
    public GameObject Cam;
    public GameObject Player;
    public GameObject CameraRotator;
    public float MaxY;
    public float MinY;
    float CurrentXPos;
    public float RotateSpeed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Cam.transform.RotateAround(Player.transform.position, Vector3.up, (Input.GetAxis("Mouse X") / Time.deltaTime) * 2 * Time.deltaTime);
        Cam.transform.RotateAround(Player.transform.position, Vector3.right, (Input.GetAxis("Mouse Y") / Time.deltaTime) * 2 * Time.deltaTime);
	}
}
