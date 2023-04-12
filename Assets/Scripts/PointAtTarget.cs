using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtTarget : MonoBehaviour
{
    public GameObject target;
    public float directionChange = 1f;

    KeyCode activateArrow;

    bool isToggled = false;

    void Start() {
        if(target == null) {
            target = GameObject.FindGameObjectWithTag("LevelEndPt");
        }
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
        RotateArrow();
    }

    void RotateArrow() {
        Vector3 direction = target.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, direction);
        Vector3 axis = Vector3.Cross(transform.forward, direction);
        Quaternion rotation = Quaternion.AngleAxis(angle, axis);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, directionChange * Time.deltaTime);
    }
}
