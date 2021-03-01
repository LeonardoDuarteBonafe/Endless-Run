using System;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlayerHealth : MonoBehaviour
{

    public Animator anim;
    public float transitionTime = .25f;  
    private Vector3 respPoint;
	//public Transform endOfMapDeath;
    public Transform respawnPoint;
    public Test test = new Test();
    public GameHandler gh = new GameHandler();

 	private void Awake(){
        transitionTime = .25f;
        respPoint = gameObject.transform.position;
	}

    public void Reset(){

        //Calcula o score

        if(GameMaster.instance.GetNumOfHearts() <= 0){
            Debug.Log("Reset");
            GameHandler.numberOfRemainingLife = 0;
            gh.CalculatePlayerFitnessByDeath();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
            
            GameMaster.instance.ResetNumOfHearts();
            GameMaster.instance.ResetNumOfPoints();
            GameMaster.instance.ResetNumOfBullets();

            GameMaster.instance.SetNumOfHearts(5);
            GameMaster.instance.SetNumOfPoints(0);
            GameMaster.instance.SetNumOfBullets(10);

           GameMaster.instance.AttHud();

        } 
    }


    private void OnCollisionEnter2D(Collision2D collision){

		if (collision.collider.tag == "RespawnTag"){

            respPoint = new Vector3(collision.transform.position.x + 0.5f, collision.transform.position.y + 1, collision.transform.position.z);
            collision.collider.isTrigger = true; 
           
        }
        if (collision.collider.tag == "EndOfMap"){

            StartCoroutine(PlayerRespawn());
            
        }

        if(collision.collider.tag == "enemy"){
            Debug.Log("Colisao?");
             StartCoroutine(PlayerHurt());
        }

        if(collision.collider.tag == "gemBackground")
        {
            GameHandler.numberOfTotalCoins++;
            Debug.Log("Gemas no totais atualizadas: " + GameHandler.numberOfTotalCoins);
        }

	}

    void OnTriggerEnter2D(Collider2D trig){

         if(trig.CompareTag("enemy")){
            Debug.Log("Trigger?");
             StartCoroutine(PlayerHurt());
       
        }
    }


     IEnumerator PlayerHurt(){

        anim.SetBool("isHurting", true);
        yield return new WaitForSeconds(transitionTime);
        GameMaster.instance.SetNumOfHearts(-1);
        anim.SetBool("isHurting", false);
        Reset();
        
    }

    IEnumerator PlayerRespawn(){

        anim.SetBool("isHurting", true);
        yield return new WaitForSeconds(transitionTime);
        gameObject.transform.position = respPoint;
        GameMaster.instance.SetNumOfHearts(-1);
        anim.SetBool("isHurting", false);
        Reset();
    }

}
