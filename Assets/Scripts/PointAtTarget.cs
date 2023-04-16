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

    void Start() {

        if(target == null) {
            target = GameObject.FindGameObjectWithTag("LevelEndPt"); 
            //findClosestEnemy();
        }


        activateArrow();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    void Update () {
        
        //if(LevelManager.enemiesInLevel > 0) {
            //findClosestEnemy();
        //} else {
            target = GameObject.FindGameObjectWithTag("LevelEndPt");
        //}

        Debug.Log("The current target: " + target.name);
        
        RotateArrowV3();
    }

    void RotateArrow() {
        Vector3 direction = target.transform.position - pivotPt.transform.position;

        direction.y = 0f; // set y-component to 0 to only rotate in forward direction

        Debug.Log("The direction vector: " + direction);

        Quaternion rotation = Quaternion.LookRotation(direction);

        Debug.Log("The rotation of the arrow: " + rotation);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpd * Time.deltaTime);
    }

    void RotateArrowV2() {
        if (target != null)
        {
            Vector3 direction = (target.transform.position - pivotPt.transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, -direction); // invert the direction vector
            Quaternion targetRotation = Quaternion.LookRotation(-direction); // invert the direction vector

            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            Debug.Log("Distance to target: " + distanceToTarget);
            if (angle > 90f && distanceToTarget <= maxDist)
            {
                direction = (pivotPt.transform.position - target.transform.position).normalized; // flip the direction vector
                angle = Vector3.Angle(transform.forward, -direction); // invert the direction vector again
                targetRotation = Quaternion.LookRotation(-direction); // invert the direction vector again
            }

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Mathf.Min(maxRotationAngle, angle));
        }
    }

    void RotateArndPivot() {
        Vector3 direction = (target.transform.position - pivotPt.transform.position);
        Quaternion rotation = Quaternion.LookRotation(-direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpd * Time.deltaTime);
    }

    void RotateArrowV3() {
        if (target != null)
        {
            // Calculate the direction towards the target
            Vector3 direction = (target.transform.position - transform.position).normalized;

            // Align the arrow's rotation with the direction line
            transform.LookAt(target.transform.position);

            // // Rotate the arrow towards the target within the maxRotationAngle
            // Quaternion targetRotation = Quaternion.LookRotation(direction);
            // transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, maxRotationAngle * Time.deltaTime);
        }


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
            Debug.Log("Arrow should not be visible");
        } else {
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
