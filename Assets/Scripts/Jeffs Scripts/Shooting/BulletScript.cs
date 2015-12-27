using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public float MaxShotDist = 100;
	public GameObject Player;
	public Vector3 BulletSpawnPoint;
	public Rigidbody BulletRigid;
	public float BulletSpeed = 20;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find ("ThirdPerson");
		this.gameObject.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
		this.gameObject.transform.rotation = Player.transform.rotation;
		BulletRigid = this.gameObject.AddComponent<Rigidbody> ();
		BulletRigid.useGravity = false;
		BulletSpawnPoint = GameObject.Find ("BulletSpawnPoint").transform.position;
		this.gameObject.transform.position = BulletSpawnPoint;
		BulletRigid.velocity = Vector3.forward * BulletSpeed;
	}
	
	// Update is called once per frame
	void Update () {

	}
}
