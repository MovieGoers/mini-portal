using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffButtonScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite OnSprite;
    public Sprite OffSprite;
    public GameObject Blocked;

    bool m_isOn;

    private void Start()
    {
        m_isOn = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (m_isOn) // ���� �ִ� ���, 
            {
                spriteRenderer.sprite = OffSprite;
                Blocked.SetActive(false);
                m_isOn = false;
            }
            else { // ���� �ִ� ���,
                spriteRenderer.sprite = OnSprite;
                Blocked.SetActive(true);
                m_isOn = true;
            }
        }
    }
}
