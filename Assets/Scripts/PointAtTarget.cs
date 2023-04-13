using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PointAtTarget : MonoBehaviour
{
    public float rotationSpeed = 2f;

    bool isToggled = false;
    KeyCode activateArrow;
    GameObject target;

    void Start() {
        if(SceneManager.GetActiveScene().name.Contains("Boss")) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }

        // Move the arrow's pivot point to the base
        //transform.Translate(0, -1, 0);

        // Set the arrow's initial local rotation to point forward along the Z-axis
        transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));

        if(target == null) {
            target = GameObject.FindGameObjectWithTag("Enemy");
        }
        activateArrow = KeyCode.X;
    }
    void Update () {
        // at first guide the player to the enemies
        // once the player as defeated all enemies, guide
        // them to the exit
        if(LevelManager.enemiesInLevel > 0) {
            target = GameObject.FindGameObjectWithTag("Enemy");
        } else {
            target = GameObject.FindGameObjectWithTag("LevelEndPt");
        }

        print("The current target is: " + target.name);
        
        RotateArrowV2();
    }

    void RotateArrow() {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);
        Vector3 axis = Vector3.Cross(transform.forward, direction);
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    void RotateArrowV2() {
        Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void RotateArrowV3() {
        transform.LookAt(target.transform.position, Vector3.up);
    }
}
