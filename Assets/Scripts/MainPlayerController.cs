using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum State { STAND, CROUCH, CRAWL, RUN, SLIDE };


public class MainPlayerController : MonoBehaviour {
    public static MainPlayerController Instance;

    public Vector3 Movement;
    public Vector3 Rotation;
    public GameObject Player;
    public float Gravity = 21f;
    public float TerminalVelocity = 20f;
    public float VerticalVelocity;

    public float RotationSpeed = 10.0f;
    public float WalkSpeed = 5.0f;
    public float CrouchSpeed = 3.0f;
    public float JumpSpeed = 10.0f;
    public float RunSpeed = 10.0f;
    public float MoveSpeed = 5.0f;
    public float OxygenRegen = .001f;

    public float SlideSpeed = 10.0f;
    private Vector3 SlideDirection;

    public State CurrentState = State.STAND;

    public GameObject Ship;

    void Awake () {
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
        Player = GameObject.Find("Player");
    }

    public void UpdatePlayer () {
        HandleMovement();
        HandleRotation();
	}

    void HandleMovement()
    {
        Movement *= MoveSpeed;
        Movement = new Vector3(Movement.x, VerticalVelocity, Movement.z);
        HandleGravity();
        Player1Controller.CharacterController.Move(Movement * Time.deltaTime);
    }

    void HandleRotation()
    {
        //transform.Rotate(Rotation * RotationSpeed);
    }

    void HandleGravity()
    {
        if(Movement.y > -TerminalVelocity)
        {
            Movement = new Vector3(Movement.x, Movement.y - Gravity * Time.deltaTime, Movement.z);
        }
        if(Player1Controller.CharacterController.isGrounded && Movement.y < - 1)
        {
            Movement = new Vector3(Movement.x, -1, Movement.z);

        }
    }

    public void HandleJump()
    {
        if (Player1Controller.CharacterController.isGrounded && (CurrentState == State.STAND || CurrentState == State.RUN))
        {
            VerticalVelocity = JumpSpeed;
        }
    }

    public void HandleToggleCrouch()
    {
        if (Player1Controller.CharacterController.isGrounded)
        {
            print(CurrentState);
            if (CurrentState == State.STAND)
            {
                MoveSpeed = CrouchSpeed;
                CurrentState = State.CROUCH;
                transform.localScale = new Vector3(1.5f, 5.0f, 1.5f);
                Player1Controller.Instance.CurrentScale = 2.5f;
            }
            else if (CurrentState == State.CROUCH)
            {
                RaycastHit hitInfo;
                bool under = false;
                if(Physics.Raycast(Player.transform.position, Player.transform.up, out hitInfo))
                {
                    if(hitInfo.collider.gameObject.tag == "Obstacle")
                    {
                        under = true;
                    }
                }
                if (!under)
                {
                    MoveSpeed = WalkSpeed;
                    CurrentState = State.STAND;
                    transform.localScale = new Vector3(1.0f, 2.5f, 1.0f);
                    Player1Controller.Instance.CurrentScale = 5.0f;
                }
            }
            else if (CurrentState == State.RUN)
            {
                CurrentState = State.SLIDE;
                MoveSpeed = SlideSpeed;
                transform.localScale = new Vector3(1.5f, 5.0f, 1.5f);
                Player1Controller.Instance.CurrentScale = 2.5f;
            }
            else if (CurrentState == State.SLIDE)
            {
                CurrentState = State.CROUCH;
                MoveSpeed = CrouchSpeed;
            }
        }
    }

    public void HandleRun()
    {
        if(Player1Controller.CharacterController.isGrounded && CurrentState == State.STAND)
        {
            CurrentState = State.RUN;
            MoveSpeed = RunSpeed;
        }
    }

    public void HandleWalk()
    {
        //print("Should be walking");
        if (Player1Controller.CharacterController.isGrounded && (CurrentState == State.STAND || CurrentState == State.RUN))
        {
            CurrentState = State.STAND;
            MoveSpeed = WalkSpeed;
        }
    }


	// Triggers
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Portal") {
			float oldY = Player.transform.position.y;

			Vector3 newPos = Player.transform.position;
			Quaternion newRot = Player.transform.rotation;

			bool canPortal = PortalManager.P.portalMove(other.gameObject.GetComponentInParent<Portal>().portalID, ref newPos, ref newRot);

			if (canPortal) {
                MouseLook.Instance.ThroughPortal = true;
                MouseLook.Instance.Portal = newRot;

				newPos.y = oldY;
                Player.transform.position = newPos;
                Player.transform.rotation = newRot;
            }
		}
        else if(other.gameObject.tag == "Beacon")
        {
            Player1Controller.Instance.Win = true;
            Gravity = 0f;
            Destroy(GameObject.Find("UpperBounds").gameObject);
        }
        else if(other.gameObject == Ship)
        {
            //print to the player the game is over, WIN!
            SceneManager.LoadScene("__WinScene");
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "OxygenSource")
        {
            OxygenController.Instance.AddOxygen(OxygenRegen);
        }
    }

    void OnCollisionStay(Collision other)
    {
        print(other.gameObject);
        if(other.gameObject.tag == "Enemy")
        {
            OxygenController.Instance.LoseOxygen();
            GUIBarScript.Instance.ForceUpdate();
        }
    }
}
