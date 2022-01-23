using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBarier : MonoBehaviour, IBarierBehavour
{
    private Color baseColor;
    private SpriteRenderer barierSprite;
    private Coroutine backToBaseColorCor;
    [SerializeField] Color colorOnHit;
    [SerializeField] float timeBeforeBackToBaseColorOnHit;

    public Color _baseColor
    {
        get
        {
           return baseColor;
        }
        set
        {
            baseColor = value;
        }
    }
    void Update()
    {
        
    }

    private void Awake()
    {
        barierSprite = GetComponent<SpriteRenderer>();
        baseColor = barierSprite.color;
    }

    public void ChangeColorOnHit()
    {
        barierSprite.color = colorOnHit;

        if(backToBaseColorCor != null)
        {
            StopCoroutine(backToBaseColorCor);
            backToBaseColorCor = null;
        }

        backToBaseColorCor = StartCoroutine(ChangeToBaseColor());
    }

    private IEnumerator ChangeToBaseColor()
    {
        yield return new WaitForSeconds(timeBeforeBackToBaseColorOnHit);
        barierSprite.color = baseColor;
        backToBaseColorCor = null;
    }
}
