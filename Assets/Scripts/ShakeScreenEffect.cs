using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeScreenEffect : MonoBehaviour
{
    Vector3 cameraPos;
    IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        cameraPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cameraPos = gameObject.transform.position;
    }


    public void shakeCamera(float duration, float intensity, float damping) {
        print("shakeCamera was called)");
        coroutine = shakeCoroutine(duration, intensity, damping);
        StartCoroutine(coroutine);
        Debug.Log("Coroutine finished)");

    }
    IEnumerator shakeCoroutine(float duration, float intensity, float damping) {
        Vector3 oldPos = new Vector3(cameraPos.x, cameraPos.y, cameraPos.z);
        float randLimit = Random.Range(10f, 20f);
        float shakeInterval = duration/randLimit;

        for(int i = 0; i < randLimit; i++) {
            yield return new WaitForSeconds(shakeInterval);
            float newX = Random.Range(-1f, 1f) * intensity;
            float newY = Random.Range(-1f, 1f) *  intensity;
            Vector3 newPos = gameObject.transform.position += new Vector3(newX, newY, cameraPos.z);

            gameObject.transform.position =
             Vector3.Lerp(gameObject.transform.position, newPos, damping);
        }

        // reset the camera's position back to original
        gameObject.transform.position = oldPos;
    }
}
