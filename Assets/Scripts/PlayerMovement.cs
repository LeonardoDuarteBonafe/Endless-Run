using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;
	public  Animator animator;

    public float runSpeed = 40f;
	float horizontalMove = 0f;

	bool jump = false;

	public Transform endOfMapDeath;

    int playerPosition = 0;


    private void Awake()
    {
        endOfMapDeath = Instantiate(endOfMapDeath, new Vector3(0, -5, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = (int)transform.position.x;
        if(playerPosition > GameHandler.highscore)
        {
            GameHandler.highscore = playerPosition;
        }
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        

    
    	if (Input.GetButtonDown("Jump") ) {

			jump = true;
			animator.SetBool("isJumping", true);
		}

        endOfMapDeath.position = new Vector3(gameObject.transform.position.x, endOfMapDeath.transform.position.y, endOfMapDeath.transform.position.z);
    }

	public void OnLanding() {

		animator.SetBool("isJumping", false);

	}

    void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, false , jump);
		jump = false;
	}

}
