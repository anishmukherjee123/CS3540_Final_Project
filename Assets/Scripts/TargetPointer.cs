using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPointer : MonoBehaviour
{   
    public GameObject player;
    public GameObject target;

    Vector3 targetPosition;
    RectTransform pointerRectTransform;
    // Start is called before the first frame update
    void Start()
    {
        if(player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if(target == null) {
            target = GameObject.FindGameObjectWithTag("LevelEndPt");
            targetPosition = target.transform.position;
        }

        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 toPosition = targetPosition;
       Vector3 fromPosition = player.transform.position; 
       Vector3 direction = (toPosition - fromPosition).normalized;
       float angle = Vector2.SignedAngle(Vector2.right, direction);
       pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }
}
