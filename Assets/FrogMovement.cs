using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{

    public Animator anim;
    public float timeBetweenJump = 500f;
    private Rigidbody2D rb2D;

     private void Start() {

        rb2D = this.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
 
        if(timeBetweenJump == 0){

            rb2D.AddForce(new Vector2(0,5), ForceMode2D.Impulse);
            timeBetweenJump = 500f;
            
        }
    else {
        
        timeBetweenJump -= 1f;
    }

    }

    void OnTriggerEnter2D(Collider2D trig){


        if(trig.gameObject.CompareTag("Bullet") ){

            StartCoroutine(DestroyFrog());
        }

    }

    
    private void OnCollisionEnter2D(Collision2D collision){

		if (collision.collider.tag == "Player"){

            StartCoroutine(DestroyFrog());
           
        }


	}

        IEnumerator DestroyFrog(){

        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
        
    }
}
