using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointAtTarget : MonoBehaviour
{
    public GameObject target;
    public float rotationSpd = 1f;

    KeyCode activateArrow;

    GameObject[] enemies;

    bool isToggled = false;

    void Start() {
        if(target == null) {
            findClosestEnemy();
        }

        if(SceneManager.GetActiveScene().name.Contains("Boss")) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        activateArrow = KeyCode.X;
    }
    void Update () {
        if(Input.GetKey(activateArrow)) {
            isToggled = true;
            if(isToggled) {
                gameObject.SetActive(true);
            } else {
                gameObject.SetActive(false);
            }
        }

        if(LevelManager.enemiesInLevel > 0) {
            findClosestEnemy();
        } else {
            target = GameObject.FindGameObjectWithTag("LevelEndPt");
        }

        print("The current target: " + target.name);
        
        RotateArrow();
    }

    void RotateArrow() {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);
        Vector3 axis = Vector3.Cross(transform.forward, direction);
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
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
}
