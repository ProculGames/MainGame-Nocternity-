using UnityEngine;
using System.Collections;

public class CloudMovement : MonoBehaviour {
	public Rigidbody CloudRigid;
	public float CloudSpeed;
	public Vector3 DestroyCloud;
	public Vector3 CreateAt;
	public GameObject CloudPrefab;
	// Use this for initialization
	void Start () {
		CloudRigid = this.gameObject.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		CloudRigid.velocity = Vector3.forward * CloudSpeed * Time.deltaTime;
		if (this.transform.position.z >= DestroyCloud.z) {
			Instantiate (CloudPrefab, CreateAt, Quaternion.Euler (0, 0, 0)).name = "Cloud";
			Destroy (this.gameObject);
		}
	}
}
