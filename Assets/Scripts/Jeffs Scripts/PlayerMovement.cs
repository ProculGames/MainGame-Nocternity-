using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	[Header ("The gameobject to be controlled by this script")]
	public GameObject Player;
	[Header ("The Players camera")]
	public GameObject PlayerCam;
	[Header ("The position the camera should go to when changing to First Person")]
	public Transform FirstPersonPos;
	[Header ("The position the camera should go to when changing to Third Person")]
	public Transform ThirdPersonPos;
	[Header ("Speed of Player when walking")]
	public float WalkSpeed;
	[Header ("Speed of Player when Running")]
	public float SprintSpeed;
	[Header ("Maximum time Player is able to sprint")]
	public float MaxSprintTime;
	[Header ("Key to press to move right")]
	public KeyCode MoveRight;
	[Header ("Key to press to move left")]
	public KeyCode MoveLeft;
	[Header ("Key to press to move forward")]
	public KeyCode MoveForward;
	[Header ("Key to press to move backward")]
	public KeyCode MoveBackward;
    [Header ("Key to press to Jump.")]
    public KeyCode JumpKey;
	[Header ("Key to press to change camera position")]
	public KeyCode ChangeCam;
	[Header ("Key to press to sprint")]
	public KeyCode SprintKey;
	public AimAndShoot AimShootScript;
    public Animator Anim;
    public float ForwardSpeed;
    public float JumpHeight;

	//Private
	public Rigidbody PlayerRigid;
	public float MoveSpeed;
	public float SprintTime;
	public bool FirstPersonActive;
	public bool RestPeriod;
    public bool OnGround;
	public float PlayerYRotation;

	// Use this for initialization
	void Start () {
		//Make PlayerRigid = the Rigidbody Component on the Player Gameobject
		PlayerRigid = Player.GetComponent<Rigidbody> ();
		AimShootScript = Player.GetComponent<AimAndShoot> ();
		MoveSpeed = WalkSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        //Animation Controls
        Anim.SetFloat("Forward", PlayerRigid.velocity.magnitude);
        Anim.SetFloat("Turn", Input.GetAxis("Mouse X") / Time.deltaTime);
        Anim.SetBool("OnGround", OnGround);
		//Run movement in the Update function
		Movement ();
		}


	void Movement(){
		//If the player is not resting
		if (!RestPeriod) {
			//if the sprint time is less than the max sprint time
			if (SprintTime < MaxSprintTime) {
				//if the player holds the sprint key down
				if (Input.GetKey (SprintKey)) {
					//make the move speed equal the sprint speed variable
					MoveSpeed = SprintSpeed;
					//and make the sprint time go up
					SprintTime += Time.deltaTime;
					//when the player releases the sprint key
				} else if (Input.GetKeyUp (SprintKey)) {
					//make move speed equal the walk speed variable
					MoveSpeed = WalkSpeed;
					
				}
				//otherwise
			} else {
				//make move speed equal the walk speed variable
				MoveSpeed = WalkSpeed;
				
				//make the rest period start
				RestPeriod = true;
			}
			//otherwise
		} else {
			//make move speed equal the walk speed variable
			MoveSpeed = WalkSpeed;
			
		}

		//If move speed equals walk speed
		if (MoveSpeed == WalkSpeed) {
			//if sprint time is greater than zero
			if(SprintTime > 0){
				//take time off of the sprint time
				SprintTime -= Time.deltaTime;
				//if sprint time is less than zero
			} else if(SprintTime < 0){
				//make sprint time equal zero
				SprintTime = 0;
				//otherwise
			} else {
				//make rest period false
				RestPeriod = false;
			}
		}

		//If the player presses the move forward key
		if (Input.GetKey (MoveForward)) {
			
			//make the Player Gameobject go forward at a speed set by Move Speed
            PlayerRigid.velocity = new Vector3(transform.forward.x * MoveSpeed, PlayerRigid.velocity.y, transform.forward.z * MoveSpeed);
			//if the player presses the move left key
			if (Input.GetKey (MoveLeft)) {
				//make the Player Gameobject go left and forward at a speed set by Move Speed
                PlayerRigid.velocity = (new Vector3((transform.forward.x - transform.right.x) * MoveSpeed, PlayerRigid.velocity.y, (transform.forward.z - transform.right.z) * MoveSpeed));
				//if the player presses the move right key
			} else if (Input.GetKey (MoveRight)) {
				//make the Player Gameobject go right and forward at a speed set by Move Speed
                PlayerRigid.velocity = (new Vector3((transform.forward.x + transform.right.x) * MoveSpeed, PlayerRigid.velocity.y, (transform.forward.z + transform.right.z) * MoveSpeed));
			}
			//if the player presses the move backward key
		} else if (Input.GetKey (MoveBackward)) {
			
			//make the Player Gameobject go backward at a speed set by Move Speed
            PlayerRigid.velocity = new Vector3(transform.forward.x * -MoveSpeed, PlayerRigid.velocity.y, transform.forward.z * -MoveSpeed);
			//if the player presses the move left key
			if (Input.GetKey (MoveLeft)) {
				//make the Player Gameobject move left and backward at a speed set by Move Speed
                PlayerRigid.velocity = (-new Vector3((transform.forward.x - transform.right.x) * MoveSpeed, PlayerRigid.velocity.y, (transform.forward.z - transform.right.z) * MoveSpeed));
				//if the player presses the move right key
			} else if (Input.GetKey (MoveRight)) {
				//make the Player Gameobject move right and backward at a speed set by Move Speed
                PlayerRigid.velocity = (-new Vector3((transform.forward.x + transform.right.x) * MoveSpeed, PlayerRigid.velocity.y, (transform.forward.z + transform.right.z) * MoveSpeed));
			}
			//if the player presses the move right key
		} else if (Input.GetKey (MoveRight)) {
			
			//make the Player Gameobject move right at a speed set by Move Speed
			PlayerRigid.velocity = (new Vector3(transform.right.x * MoveSpeed, PlayerRigid.velocity.y, transform.right.z));
			//if the player presses the move left
		} else if (Input.GetKey (MoveLeft)) {
			
			//make the Player Gameobject move left at a speed set by Move Speed
			PlayerRigid.velocity = new Vector3(transform.right.x * -MoveSpeed, PlayerRigid.velocity.y, transform.right.z * -MoveSpeed);
		} else {
			
		}

        if (Input.GetKeyDown(JumpKey) && OnGround)
        {
            PlayerRigid.velocity = new Vector3(PlayerRigid.velocity.x, JumpHeight, PlayerRigid.velocity.z);
            OnGround = false;
        }
	}

    void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.tag == "Ground")
        {
            OnGround = true;
        }
    }
}
