using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffButtonScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    AudioSource m_audioSource;

    public Sprite OnSprite;
    public Sprite OffSprite;
    public GameObject Blocked;

    bool m_isOn;

    private void Start()
    {
        m_isOn = true;
        m_audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (m_isOn) // 켜져 있는 경우, 
            {
                spriteRenderer.sprite = OffSprite;
                Blocked.SetActive(false);
                m_isOn = false;
                m_audioSource.Play();
            }
            else { // 꺼져 있는 경우,
                spriteRenderer.sprite = OnSprite;
                Blocked.SetActive(true);
                m_isOn = true;
                m_audioSource.Play();
            }
        }
    }
}
