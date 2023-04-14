using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossBehavior : MonoBehaviour
{
    public int attackTime = 10;
    public float attackRate = 4f;
    public int restTime = 5;

    public int bulletAmount = 15;

    public GameObject bulletPrefab;
    public GameObject bulletParent;

    float startTime = 0f;

    bool shooting = false;
    bool started = false;

    float shootTimer = 0f;
    float restTimer = 0f;
    void Start()
    {
        Debug.Log("boss position: " + transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            float curTime = Time.time - startTime;
            if (shooting)
            {
                shootTimer += Time.deltaTime;
                if(shootTimer >= attackTime)
                {
                    CancelInvoke();
                    shootTimer = 0f;
                    shooting = false;
                }
            }
            else
            {
                restTimer += Time.deltaTime;
                if(restTimer >= restTime)
                {
                    InvokeRepeating("FireProjectiles", 0f, attackRate);
                    restTimer = 0f;
                    shooting = true;
                }
            }
        }
    }

    public void startShooting()
    {
        InvokeRepeating("FireProjectiles", 0f, attackRate);
        startTime = Time.time;
        shooting = true;
        started= true;
    }

    void FireProjectiles()
    {
        float angleStep = 360 / bulletAmount;
        float curAngle = 0f;

        for(int i = 0; i < bulletAmount; i++)
        {
            float dirX = transform.position.x + Mathf.Sin((curAngle * Mathf.PI) / 180f);
            float dirZ = transform.position.z + Mathf.Cos((curAngle * Mathf.PI) / 180f);

            Vector3 moveVector = new Vector3(dirX, transform.position.y, dirZ);
            Vector3 moveDir = (moveVector - transform.position).normalized;

            GameObject bullet = Instantiate(bulletPrefab, bulletParent.transform.position, transform.rotation);
            bullet.GetComponent<BulletBehavior>().moveDirection = moveDir;
            bullet.transform.SetParent(bulletParent.transform, true);
            Debug.Log("bullet " + i + ": X: " + dirX + " Z: " + dirZ + " angle: " + curAngle);

            curAngle += angleStep;
            
        }
    }

    public void Die()
    {

    }
}
