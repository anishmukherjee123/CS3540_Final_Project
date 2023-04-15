using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointAtTarget : MonoBehaviour
{
    public GameObject target;
    public float rotationSpd = 1f;

    GameObject[] enemies;

    bool isToggled = false;

    void Start() {
        // if(target == null) {
        //     findClosestEnemy();
        // }

        if(target == null) {
            target = GameObject.FindGameObjectWithTag("LevelEndPt"); 
        }


        activateArrow();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    void Update () {
        activateArrow();
        
        //if(LevelManager.enemiesInLevel > 0) {
          //  findClosestEnemy();
        //} else {
            target = GameObject.FindGameObjectWithTag("LevelEndPt");
        //}

        print("The current target: " + target.name);
        
        RotateArrow();
    }

    void RotateArrow() {
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0f; // set y-component to 0 to only rotate in forward direction
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpd * Time.deltaTime);
    }

    void findClosestEnemy() {
        float minDistance = Vector3.Distance(gameObject.transform.position, enemies[0].transform.position);
        GameObject targetEnemy = enemies[0];
        foreach(GameObject eachEnemy in enemies) {
            float currentDistance = Vector3.Distance(gameObject.transform.position, eachEnemy.transform.position);
            if(currentDistance < minDistance && !eachEnemy.GetComponent<EnemyHealth>().dead) {
                minDistance = currentDistance;
                targetEnemy = eachEnemy;
            }
        }

        target = targetEnemy;
    }

    void activateArrow() {
        if(SceneManager.GetActiveScene().name.Contains("Boss")) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }
    }
}
