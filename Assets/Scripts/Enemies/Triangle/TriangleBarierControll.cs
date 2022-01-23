using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleBarierControll : BaseBarier
{
    [SerializeField] private GameObject triangle;
    [SerializeField] private float timeForColorToChangeOnCollWithPlayer;
    [SerializeField] private Color finalColorOnBarierDestroy;
    [SerializeField] private Color transparentRed;
    private float yDifference = 0.320f;
    private Coroutine destroyBarierCor;
    private Collider2D barierCollider;

    private void Start()
    {
        barierCollider = GetComponent<Collider2D>();
        transparentRed = new Color(finalColorOnBarierDestroy.r, finalColorOnBarierDestroy.g, finalColorOnBarierDestroy.b, 0f);
    }

    void LateUpdate()
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

        for(int i = 1; Time.time < startTime + timeForColorToChangeOnCollWithPlayer; i++)
        {
            _barierSprite.color = Color.Lerp(_baseColor, finalColorOnBarierDestroy, i * 0.1f );
            yield return new WaitForSeconds(timeForColorToChangeOnCollWithPlayer / 10);
        }

        _barierSprite.color = transparentRed;

        barierCollider.enabled = false;
        destroyBarierCor = null;
    }

    private void UpdateBarierPosition()
    {
        Vector3 trianglePosition = triangle.transform.position;
        trianglePosition.y += yDifference;
        transform.position = trianglePosition;
    }
}
