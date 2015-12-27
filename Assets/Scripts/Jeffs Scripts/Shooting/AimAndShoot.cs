using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AimAndShoot : MonoBehaviour {
	[Header("Crosshairs")]
	[Header("      The top crosshair")]
	public Image CrosshairTop;
	[Header("      The bottom crosshair")]
	public Image CrosshairBottom;
	[Header("      The left crosshair")]
	public Image CrosshairLeft;
	[Header("      The right crosshair")]
	public Image CrosshairRight;
	[Header("Crosshair moving data")]
	public float ChangeByWhenRunning;
	[Header("      The positon the crosshair moves to while running")]
	public float ChangeToWhenRunning;
	public float ChangeByWhenWalking;
	[Header("      The position the crosshair moves to when walking")]
	public float ChangeToWhenWalking;
	[Header("      The position the crosshair moves to when stopped")]
	public float ChangeToWhenStopped;
	[Header("Others")]
	public bool Walking;
	public bool Running;
	public Camera MyCam;
	[Header("")]
	[Header("Shooting")]
	public bool SingleShot;
	public int FullAmmo;
	public float CurrentAmmo;
	public GameObject Bullet;
	public bool Reloading;
	public Transform BulletSpawnPoint;
	public float BulletSpeed;
	public PlayerMovement PlayerMoveScript;
	// Use this for initialization
	void Start () {
		ChangeToWhenStopped = CrosshairTop.rectTransform.anchoredPosition.y;
		ChangeToWhenWalking = ChangeToWhenStopped + ChangeByWhenWalking;
		ChangeToWhenRunning = ChangeToWhenStopped + ChangeByWhenRunning;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (PlayerMoveScript.MoveLeft)) {
			if (Input.GetKey (PlayerMoveScript.SprintKey)) {
				RunningCrosshair ();
			} else {
				WalkingCrosshair ();
			}
		} else if (Input.GetKey (PlayerMoveScript.MoveRight)) {
			if (Input.GetKey (PlayerMoveScript.SprintKey)) {
				RunningCrosshair ();
			} else {
				WalkingCrosshair ();
			}
		}else if(Input.GetKey (PlayerMoveScript.MoveForward)){
			if (Input.GetKey (PlayerMoveScript.SprintKey)) {
				RunningCrosshair ();
			} else {
				WalkingCrosshair ();
			}
		} else if(Input.GetKey (PlayerMoveScript.MoveBackward)){
			if (Input.GetKey (PlayerMoveScript.SprintKey)) {
				RunningCrosshair ();
			} else {
				WalkingCrosshair ();
			}
		} else {
			StoppedCrosshair ();
		}

		if (Input.GetMouseButtonDown (0)) {
			Shoot ();
		}

		if (Reloading) {
			if(CurrentAmmo < FullAmmo){
				CurrentAmmo += Time.deltaTime;
			} else {
				CurrentAmmo = 10;
			}
			if(CurrentAmmo == 10){
				Reloading = false;
			}
		}
	}

	//The position for the crosshairs when you arent moving
	public void StoppedCrosshair(){
		CrosshairTop.rectTransform.anchoredPosition =  new Vector2 (0, ChangeToWhenStopped);
		CrosshairBottom.rectTransform.anchoredPosition = new Vector2 (0, -ChangeToWhenStopped);
		CrosshairRight.rectTransform.anchoredPosition = new Vector2 (ChangeToWhenStopped, 0);
		CrosshairLeft.rectTransform.anchoredPosition = new Vector2 (-ChangeToWhenStopped, 0);
	}

	//The position for the crosshairs when you are walking
	public void WalkingCrosshair(){
		CrosshairTop.rectTransform.anchoredPosition =  new Vector2 (0, ChangeToWhenWalking);
		CrosshairBottom.rectTransform.anchoredPosition = new Vector2 (0, -ChangeToWhenWalking);
		CrosshairRight.rectTransform.anchoredPosition = new Vector2 (ChangeToWhenWalking, 0);
		CrosshairLeft.rectTransform.anchoredPosition = new Vector2 (-ChangeToWhenWalking, 0);
	}

	//The positions for the crosshairs when you are running
	public void RunningCrosshair(){
		CrosshairTop.rectTransform.anchoredPosition = new Vector2 (0, ChangeToWhenRunning);
		CrosshairBottom.rectTransform.anchoredPosition = new Vector2 (0, -ChangeToWhenRunning);
		CrosshairRight.rectTransform.anchoredPosition = new Vector2 (ChangeToWhenRunning, 0);
		CrosshairLeft.rectTransform.anchoredPosition = new Vector2 (-ChangeToWhenRunning, 0);
	}

	void Shoot(){
		if (!Reloading) {
			if (CurrentAmmo > 0) {
				GameObject.CreatePrimitive (PrimitiveType.Capsule).AddComponent<BulletScript> ();
				CurrentAmmo--;
			} else {
				Reloading = true;
			}
		}
	}
}
