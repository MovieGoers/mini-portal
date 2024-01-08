using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffButtonScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite OnSprite;
    public Sprite OffSprite;
    public GameObject Blocked;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            spriteRenderer.sprite = OffSprite;
            Blocked.SetActive(false);
        }
    }
}
