using System;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class NextLevel : MonoBehaviour
{

    public float transitionTime = .1f;

    public GameHandler gh;

    private void Awake()
    {
        gh = FindObjectOfType<GameHandler>();
    }
    void OnTriggerEnter2D(Collider2D trig){

         if(trig.CompareTag("Player")){

             LoadNextLevel();
       
        }
    }

    public void LoadNextLevel(){

        gh.CalculatePlayerFitnessByScore();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        GameMaster.instance.AttHud();
    }

     IEnumerator  LoadLevel(int levelIndex){

       //Calcular o score

        yield return new WaitForSeconds(transitionTime);
        GameMaster.instance.ResetNumOfHearts();
        GameMaster.instance.ResetNumOfPoints();
        GameMaster.instance.ResetNumOfBullets();

        GameMaster.instance.SetNumOfHearts(5);
        GameMaster.instance.SetNumOfPoints(0);
        GameMaster.instance.SetNumOfBullets(10);

        SceneManager.LoadScene(levelIndex);
        

    }


}
