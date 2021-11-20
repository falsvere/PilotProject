using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControll : MonoBehaviour
{

    private int timeBeforeDestroy = 5;

    private GameManager gameManager;

    private bool isCoroutineActive = false;


    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private IEnumerator DestroyPlatformCoroutine()
    {
        isCoroutineActive = true;

        for(int i = 0; i <= timeBeforeDestroy; i++) {
            yield return new WaitForSeconds(1);
            Debug.Log("Time before destroy :" + (timeBeforeDestroy - i));
        }

        gameObject.SetActive(false);
        yield return new WaitForSeconds(5);
        Debug.Log("should activate");
        gameObject.SetActive(true);

        isCoroutineActive = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Player" && !isCoroutineActive)
        {
            gameManager.StartCoroutine(DestroyPlatformCoroutine());
        }
    }
}
