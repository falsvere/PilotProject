using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        
    }

    public void ActivateChangePlatformStateCoroutune(GameObject platform)
    {
        StartCoroutine(ChangePlatformState(platform));
    }

    public IEnumerator ChangePlatformState(GameObject platform)
    {
        platform.SetActive(false);
        Debug.Log(platform);
        yield return new WaitForSeconds(5);
        Debug.Log("should activate");
        Debug.Log(platform);
        platform.SetActive(true);
    }

    void Update()
    {
        
    }
}
