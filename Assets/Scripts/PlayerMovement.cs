using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_rb;
    AudioSource m_audioSource;

    bool m_isGrounded;
    bool m_isFacingRight;

    bool m_isPlayerJump;
    public static bool isPortalJump;

    public float playerSpeed;
    public float playerJumpForce;
    public float playerFloatingRatio;

    public Animator animator;
    public SpriteRenderer spriteSrc;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();

        m_isGrounded = true;
        playerJumpForce = 200.0f;
        playerSpeed = 200.0f;
        m_isFacingRight = true;
        playerFloatingRatio = 0.15f;

        m_isPlayerJump = false;
        isPortalJump = false;
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
                if (m_rb.velocity.x > 0)
                {
                    Vector3 newVelocity = new Vector3(m_rb.velocity.x - playerSpeed * playerFloatingRatio * Time.deltaTime, m_rb.velocity.y, 0);
                    m_rb.velocity = newVelocity;
                }
                else if (isPortalJump) // 정방향으로 가는 방향이지만 포탈 점프일 경우에만 속도 제어.
                {
                    Vector3 newVelocity = new Vector3(m_rb.velocity.x - playerSpeed * playerFloatingRatio * 0.15f * Time.deltaTime, m_rb.velocity.y, 0);
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
                else if(isPortalJump) // 정방향으로 가는 방향이지만 포탈 점프일 경우에만 속도 제어.
                {
                    Vector3 newVelocity = new Vector3(m_rb.velocity.x + playerSpeed * playerFloatingRatio * 0.15f * Time.deltaTime, m_rb.velocity.y, 0);
                    m_rb.velocity = newVelocity;
                }
            }
        }

        if ((Input.GetKeyDown(KeyCode.Space) || (Input.GetKeyDown(KeyCode.W))) && m_isGrounded)
        {
            m_rb.AddForce(transform.up * playerJumpForce);
            m_isPlayerJump = true;
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
            m_isPlayerJump = false;
            isPortalJump = false;
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
