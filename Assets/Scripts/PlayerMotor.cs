using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {
    private Animator anim;
    private Rigidbody rig;
    private bool ground;
    public float JumpHeight = 3;
    public float MoveSpeed = 5; 
	void Start ()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
	}
	
	
	void Update ()
    {
        
        anim.SetFloat("Blend", rig.velocity.magnitude);
        anim.SetFloat("Strafe", Input.GetAxis("Horizontal"));
        //anim.SetBool("Ground", ground);
        Move();
	}

    void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rig.velocity = new Vector3(transform.forward.x * MoveSpeed, rig.velocity.y, transform.forward.z * MoveSpeed);
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            rig.velocity = (new Vector3(transform.right.x * MoveSpeed, rig.velocity.y, transform.right.z));
        }
        if (Input.GetKey(KeyCode.A))
        {
            rig.velocity = new Vector3(transform.right.x * -MoveSpeed, rig.velocity.y, transform.right.z * -MoveSpeed);
        }
        if (Input.GetButtonDown("Jump") && ground)
        {
            rig.velocity = new Vector3(rig.velocity.x, JumpHeight, rig.velocity.z);
            ground = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rig.velocity = new Vector3(transform.forward.x * -MoveSpeed, rig.velocity.y, transform.forward.z * -MoveSpeed);
        }
    
       
    }
    void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag == "Ground")
        {
            ground = true;
        }
    }
}
