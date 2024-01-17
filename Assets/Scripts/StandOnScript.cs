using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandOnScript : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    public Sprite OnSprite;
    public Sprite OffSprite;
    public GameObject Blocked;

    public float RotationSpeed;

    public bool m_isOn;

    bool m_isPlayerin;

    AudioSource m_audio;

    private void Start()
    {
        m_audio = gameObject.GetComponent<AudioSource>();
        m_isPlayerin = false;
        if (m_isOn)
        {
            spriteRenderer.sprite = OnSprite;
            Blocked.SetActive(true);
        }
        else
        {
            spriteRenderer.sprite = OffSprite;
            Blocked.SetActive(false);
        }
        RotationSpeed = 100f;
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * RotationSpeed * Time.deltaTime);
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
            }
            else
            { // 꺼져 있는 경우,
                spriteRenderer.sprite = OnSprite;
                Blocked.SetActive(true);
                m_isOn = true;
            }
            m_audio.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (m_isOn)
            {
                m_isOn = false;
                spriteRenderer.sprite = OffSprite;
                Blocked.SetActive(false);
            }
            else
            {
                m_isOn = true;
                spriteRenderer.sprite = OnSprite;
                Blocked.SetActive(true);
            }
            m_audio.Play();
        }
    }
}
