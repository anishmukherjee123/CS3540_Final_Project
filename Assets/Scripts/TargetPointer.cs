using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetPointer : MonoBehaviour
{   
    public GameObject player;
    public GameObject target;

    [SerializeField] Camera uiCam;
    Vector3 targetPosition;
    RectTransform pointerRectTransform;
    GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        activateArrow();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        if(target == null) {
            // target = GameObject.FindGameObjectWithTag("LevelEndPt");
            // targetPosition = target.transform.position;
            SetTarget();
        }

        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        activateArrow();
        SetTarget();

        float border = 50f;

       Vector3 targetPosScreenPt = Camera.main.WorldToScreenPoint(targetPosition);
       Debug.Log(OffScreen(targetPosScreenPt, border));

        if(OffScreen(targetPosScreenPt, border)) {
            RotatePointer();
            Vector3 capPos = targetPosScreenPt;
            if(capPos.x <= border) {
                capPos.x = border;
            }
            if(capPos.x >= Screen.width - border) {
                capPos.x = Screen.width - border;
            }
            if(capPos.y <= border) {
                capPos.y = border;
            }
            if(capPos.y >= Screen.height - border) {
                capPos.y = Screen.height - border;
            }
            Vector3 pointerWorldPos = uiCam.ScreenToWorldPoint(capPos);
            pointerRectTransform.position = pointerWorldPos;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);
        } else {
            Vector3 pointerWorldPos = uiCam.ScreenToWorldPoint(targetPosScreenPt);
            pointerRectTransform.position = pointerWorldPos;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

            pointerRectTransform.localEulerAngles = Vector3.zero;
        }
    }

    void RotatePointer() {
       Vector3 toPosition = targetPosition;
       Vector3 fromPosition = player.transform.position; 
       fromPosition.z = 0f;
       Vector3 direction = (toPosition - fromPosition).normalized;

       float angle = Vector2.SignedAngle(Vector2.right, direction);

       pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    bool OffScreen(Vector3 screenPos, float border) {
        return screenPos.x <= border || screenPos.x >= Screen.width - border || screenPos.y <= border || screenPos.y >= Screen.height - border;
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
            if(!(PauseMenu.isGamePaused)) {
                gameObject.SetActive(true);
                Debug.Log("Arrow should be visible");
            }
        }
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
        void SetTarget() {
        if(LevelManager.enemiesInLevel <= 0) {
            target = GameObject.FindGameObjectWithTag("LevelEndPt");
        } else {
            findClosestEnemy();
        }

        targetPosition = target.transform.position;
    }
}
