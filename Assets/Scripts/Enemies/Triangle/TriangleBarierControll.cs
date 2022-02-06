using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBarierControll : BaseBarier
{
    [SerializeField] private GameObject triangle;
    [SerializeField] private float timeForColorToChangeOnCollWithPlayer;
    [SerializeField] private float recoverTime;
    [SerializeField] private Color finalColorOnBarierDestroy;
    private Color transparentRed;
    private Color transparentBaseColor;
    private float yDifference = 0.3f;
    private Coroutine destroyBarierCor;
    private Collider2D barierCollider;

    private void Start()
    {
        barierCollider = GetComponent<Collider2D>();
        transparentRed = new Color(finalColorOnBarierDestroy.r, finalColorOnBarierDestroy.g, finalColorOnBarierDestroy.b, 0f);
        transparentBaseColor = new Color(_baseColor.r, _baseColor.g, _baseColor.b, 0f);
    }

    void Update()
    {
        UpdateBarierPosition();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(destroyBarierCor == null)
            {
                destroyBarierCor = StartCoroutine(DestroyBarier());
            }
        }
    }

    private IEnumerator DestroyBarier()
    {
        float startTime = Time.time;
        int colorRecoverStepsAmount = 5;

        for(int i = 1; Time.time < startTime + timeForColorToChangeOnCollWithPlayer; i++)
        {
            _barierSprite.color = Color.Lerp(_baseColor, finalColorOnBarierDestroy, i * 0.1f );
            yield return new WaitForSeconds(timeForColorToChangeOnCollWithPlayer / 10);
        }

        _barierSprite.color = transparentRed;
        barierCollider.enabled = false;

        yield return new WaitForSeconds(recoverTime);

        for (int i = 1; i <= colorRecoverStepsAmount; i++)
        {
            _barierSprite.color = Color.Lerp(transparentBaseColor, _baseColor, i * 0.2f);
            yield return new WaitForSeconds(0.1f);
        }
        barierCollider.enabled = true;
        destroyBarierCor = null;
    }

    private void UpdateBarierPosition()
    {
        Vector3 trianglePosition = triangle.transform.position;
        trianglePosition.y += yDifference;
        transform.position = trianglePosition;
    }
}
