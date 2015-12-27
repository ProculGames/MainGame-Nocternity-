using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject PlayerCam;
	public GameObject Player;
	public float MoveCamBy;
	public float MousePosY;
	public float CurrentMousePosY;
	public float CurrentMousePosX;
	public float CamRotationY;
	public float CamRotationX;
	public float CamSpeed;
	public float MouseStartPointX;
	public float MinThirdPersonDist;
	public float MaxThirdPersonDist;
	public Quaternion CurrentCamRotation;
	public bool FirstPerson;
	public Transform FirstPersonPos;
	public Transform ThirdPersonPos;
	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		CamRotationY = PlayerCam.transform.rotation.y;
		CamRotationX = PlayerCam.transform.rotation.x;
		CurrentMousePosY = Input.mousePosition.y;
		CurrentMousePosX = Input.mousePosition.x;
		MouseStartPointX = Input.mousePosition.x;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Vector3.Distance (PlayerCam.transform.position, Player.transform.position).ToString ());
		Player.transform.rotation = Quaternion.Euler (Player.transform.rotation.x, CamRotationY, Player.transform.rotation.z);
		if (FirstPerson) {
			FirstPersonCam ();
		} else {
			ThirdPersonCam ();
		}
	}

	void FirstPersonCam(){
		MousePosY = Input.mousePosition.y;
		PlayerCam.transform.rotation = Quaternion.Slerp (PlayerCam.transform.rotation, Quaternion.Euler (Mathf.Clamp (CamRotationX, -90, 90), CamRotationY, PlayerCam.transform.rotation.z), CamSpeed);
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			PlayerCam.transform.position = ThirdPersonPos.position;
			FirstPerson = false;
		}

		if (Input.mousePosition.x > CurrentMousePosX) {
			CamRotationY++;
			CurrentMousePosX = Input.mousePosition.x;
		} else if (Input.mousePosition.x < CurrentMousePosX) {
			CamRotationY--;
			CurrentMousePosX = Input.mousePosition.x;
		} else {
			CamRotationY = CamRotationY;
		}

		if (Input.mousePosition.y < CurrentMousePosY) {
			CamRotationX++;
			CurrentMousePosY = Input.mousePosition.y;
		} else if (Input.mousePosition.y > CurrentMousePosY) {
			CamRotationX--;
			CurrentMousePosY = Input.mousePosition.y;
		} else {
			CamRotationX = CamRotationX;
		}

		if (Input.mousePosition.x >= 300 + MouseStartPointX) {
			CurrentCamRotation = PlayerCam.transform.rotation;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.lockState = CursorLockMode.None;
			PlayerCam.transform.rotation = CurrentCamRotation;
		} else if (Input.mousePosition.x <= MouseStartPointX - 300) {
			CurrentCamRotation = PlayerCam.transform.rotation;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.lockState = CursorLockMode.None;
			PlayerCam.transform.rotation = CurrentCamRotation;
		}
	}

	void ThirdPersonCam(){
		PlayerCam.transform.LookAt (Player.transform.position);
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) {
			if(Vector3.Distance (Player.transform.position, PlayerCam.transform.position) < MaxThirdPersonDist){
			PlayerCam.transform.Translate (PlayerCam.transform.forward * -MoveCamBy);
			}
		} else if(Input.GetAxis ("Mouse ScrollWheel") > 0){
			if(Vector3.Distance (Player.transform.position, PlayerCam.transform.position) > MinThirdPersonDist){
			PlayerCam.transform.Translate (PlayerCam.transform.forward * MoveCamBy);
			} else {
				PlayerCam.transform.position = FirstPersonPos.position;
				FirstPerson = true;
			}
		}
	}
}
