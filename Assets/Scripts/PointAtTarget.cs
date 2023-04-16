using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointAtTarget : MonoBehaviour
{
    public GameObject target;

    public GameObject pivotPt;
    public float rotationSpd = 1f;
    public float maxRotationAngle = 10f;

    public float maxDist = 50f;

    GameObject[] enemies;

    void Start()
    {

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("LevelEndPt");
            //findClosestEnemy();
        }


        activateArrow();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    void Update()
    {

        //if(LevelManager.enemiesInLevel > 0) {
        //findClosestEnemy();
        RotateArrow();
    }

    void RotateArrow() {
        Vector3 direction = target.transform.position - transform.position;

         Quaternion rotation = Quaternion.LookRotation(direction);
         transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpd * Time.deltaTime);
    }

    void findClosestEnemy()
    {
        float minDistance = Vector3.Distance(gameObject.transform.position, enemies[0].transform.position);
        GameObject targetEnemy = enemies[0];
        foreach (GameObject eachEnemy in enemies)
        {
            float currentDistance = Vector3.Distance(gameObject.transform.position, eachEnemy.transform.position);
            if (currentDistance < minDistance && !eachEnemy.GetComponent<EnemyHealth>().dead)
            {
                minDistance = currentDistance;
                targetEnemy = eachEnemy;
            }
        }

        target = targetEnemy;
    }

    void activateArrow()
    {
        if (SceneManager.GetActiveScene().name.Contains("Boss"))
        {
            gameObject.SetActive(false);
            Debug.Log("Arrow should not be visible");
        }
        else
        {
            gameObject.SetActive(true);
            Debug.Log("Arrow should be visible");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + direction);

            Quaternion targetRotation = Quaternion.LookRotation(-direction);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, targetRotation * Vector3.forward);
        }
    }

}
