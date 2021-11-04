using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformControll : MonoBehaviour
{

    private int timeBeforeDestroy = 3; 
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private IEnumerator DestroyPlatformCoroutine()
    {
        for(int i = 0; i <= timeBeforeDestroy; i++) {
            yield return new WaitForSeconds(1);
            Debug.Log("Time before destroy :" + (timeBeforeDestroy - i));
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name == "Player")
        {
            StartCoroutine(DestroyPlatformCoroutine());
        }
    }
}
