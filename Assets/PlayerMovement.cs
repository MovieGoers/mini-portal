using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_rb;

    bool m_isGrounded;

    public float playerSpeed = 3.0f;
    public float playerJumpForce = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_isGrounded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)){
            m_rb.velocity += new Vector2(-1 * playerSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            m_rb.velocity += new Vector2(1 * playerSpeed * Time.deltaTime, 0);
        }

        if (Input.GetKey(KeyCode.Space) && m_isGrounded)
        {
            m_rb.AddForce(transform.up * playerJumpForce);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = false;
        }
    }
}
