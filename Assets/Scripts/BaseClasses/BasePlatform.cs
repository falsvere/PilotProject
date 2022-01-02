using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePlatform : MonoBehaviour
{
    public int timeBeforeDestroy = 6;
    public int timeBeforeAppear = 6;

    private GameManager gameManager;
    public bool isDestroyCoroutineActive = false;
    public bool isAppearCoroutineActive = false;
    public bool isCircleEnemyInteractionActive = false;

    public Coroutine destroyPlatformCoroutine;

    public PlatformParticles[] childsScript;
    public BoxCollider2D platformBoxCollider;

    public void SetGameManager()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    //корутины исчесзновения и появления платформы
    public virtual IEnumerator DestroyPlatformCoroutine(int timeBeforeDestroy)
    {
        isDestroyCoroutineActive = true;

        for (int i = 0; i <= timeBeforeDestroy; i++)
        {
            yield return new WaitForSeconds(1);
           // Debug.Log("Time before destroy :" + (timeBeforeDestroy - i));
        }

        platformBoxCollider.enabled = false;

        gameObject.SetActive(false);

        gameManager.StartCoroutine(AppearPlatformCoroutine(timeBeforeAppear));

        isDestroyCoroutineActive = false;
    }

    public virtual IEnumerator AppearPlatformCoroutine(int timeBeforeAppear)
    {
        isAppearCoroutineActive = true;

        GatherAllPlatfomParticles();

        yield return new WaitForSeconds(timeBeforeAppear);

        // Debug.Log("should activate");
        gameObject.SetActive(true);
        platformBoxCollider.enabled = true;

        isAppearCoroutineActive = false;
    }

    // если игрок коснулся платформы и корутина изменения еще не запущена - запускаем ее на гейм менеджере тк при отключении объекта корутина остановится
    public virtual void RunPlatformStateCoroutine(Collision2D collision, int timeBeforeDestroy, int timeBeforeAppear)
    {
        if (collision.collider.name == "Player" && !isDestroyCoroutineActive)
        {
            destroyPlatformCoroutine = StartCoroutine(DestroyPlatformCoroutine(timeBeforeDestroy));
        }
    }

    public virtual IEnumerator CircleEnemyInteraction()
    {
        isCircleEnemyInteractionActive = true;

        if (isDestroyCoroutineActive)
        {
            StopCoroutine(destroyPlatformCoroutine);
            isDestroyCoroutineActive = false;
        }

        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);

        gameManager.StartCoroutine(AppearPlatformCoroutine(timeBeforeAppear));

        isCircleEnemyInteractionActive = false;
    }

    private void GatherAllPlatfomParticles()
    {
        foreach(PlatformParticles childScript in childsScript)
        {
            childScript.ResetPosition();
        }
    }

    public void SetChildrensScriptComponent()
    {
        childsScript = transform.GetComponentsInChildren<PlatformParticles>();
    }

    public abstract void SetParentScriptForChildrens();
}
