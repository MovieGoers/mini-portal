using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D m_rb;

    bool m_isGrounded;

    public float playerSpeed;
    public float playerJumpForce;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody2D>();
        m_isGrounded = true;
        playerJumpForce = 200.0f;
        playerSpeed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A) && m_isGrounded)
        {
            Vector3 newVelocity= new Vector3(-1 * playerSpeed, m_rb.velocity.y, 0);
            m_rb.velocity = newVelocity;
            //transform.Translate(new Vector3(-1 * playerSpeed * Time.deltaTime, 0, 0));
        }

        if (Input.GetKey(KeyCode.D) && m_isGrounded)
        {
            Vector3 newVelocity = new Vector3(1 * playerSpeed, m_rb.velocity.y, 0);
            m_rb.velocity = newVelocity;
            //transform.Translate(new Vector3(1 * playerSpeed * Time.deltaTime, 0, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space) && m_isGrounded)
        {
            m_rb.AddForce(transform.up * playerJumpForce);
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
