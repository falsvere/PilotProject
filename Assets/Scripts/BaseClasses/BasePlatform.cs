using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlatform : MonoBehaviour
{

    private GameManager gameManager;
    private bool isCoroutineActive = false;

    public void SetGameManager()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    //корутина исчесзновения и появления платформы
    public virtual IEnumerator DestroyPlatformCoroutine(int timeBeforeDestroy, int timeBeforeAppear)
    {
        isCoroutineActive = true;

        for (int i = 0; i <= timeBeforeDestroy; i++)
        {
            yield return new WaitForSeconds(1);
           // Debug.Log("Time before destroy :" + (timeBeforeDestroy - i));
        }

        gameObject.SetActive(false);

        yield return new WaitForSeconds(timeBeforeAppear);

       // Debug.Log("should activate");
        gameObject.SetActive(true);

        isCoroutineActive = false;
    }

    // если игрок коснулся платформы и корутина изменения еще не запущена - запускаем ее на гейм менеджере тк при отключении объекта корутина остановится
    public virtual void RunPlatformStateCoroutine(Collision2D collision, int timeBeforeDestroy, int timeBeforeAppear)
    {
        if (collision.collider.name == "Player" && !isCoroutineActive)
        {
            gameManager.StartCoroutine(DestroyPlatformCoroutine(timeBeforeDestroy, timeBeforeAppear));
        }
    }
}
