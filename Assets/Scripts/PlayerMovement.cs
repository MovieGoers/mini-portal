using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_rb;
    AudioSource m_audioSource;

    bool m_isGrounded;
    bool m_isFacingRight;

    public float playerSpeed;
    public float playerJumpForce;
    public float playerFloatingRatio;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();

        m_isGrounded = true;
        playerJumpForce = 200.0f;
        playerSpeed = 200.0f;
        m_isFacingRight = true;
        playerFloatingRatio = 0.045f;

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(m_rb.velocity.x));
        animator.SetBool("IsGrounded", m_isGrounded);

        if (m_rb.velocity.x < 0 && m_isFacingRight)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            m_isFacingRight = false;
        }
        if (m_rb.velocity.x > 0 && !m_isFacingRight)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            m_isFacingRight = true;
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (m_isGrounded) {
                Vector3 newVelocity = new Vector3(-1 * playerSpeed * Time.deltaTime, m_rb.velocity.y, 0);
                m_rb.velocity = newVelocity;
            }
            else
            {
                if (m_rb.velocity.x > 0) {
                    Vector3 newVelocity = new Vector3(m_rb.velocity.x - playerSpeed * playerFloatingRatio * Time.deltaTime, m_rb.velocity.y, 0);
                    m_rb.velocity = newVelocity;
                }
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (m_isGrounded)
            {
                Vector3 newVelocity = new Vector3(1 * playerSpeed * Time.deltaTime, m_rb.velocity.y, 0);
                m_rb.velocity = newVelocity;
            }
            else
            { 
                if (m_rb.velocity.x < 0)
                {
                    Vector3 newVelocity = new Vector3(m_rb.velocity.x + playerSpeed * playerFloatingRatio * Time.deltaTime, m_rb.velocity.y, 0);
                    m_rb.velocity = newVelocity;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
        {
            m_rb.AddForce(transform.up * playerJumpForce);
        }

        if (m_rb.velocity.x != 0 && m_isGrounded)
        {
            if (!m_audioSource.isPlaying)
                m_audioSource.Play();
        }
        else
        {
            m_audioSource.Stop();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = false;
        }
    }
}
