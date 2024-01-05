using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject player;
    public GameObject orangePortal;
    public GameObject bluePortal;

    Vector3 playerEnterVelocity;

    bool m_EnteredPortal;

    // Start is called before the first frame update
    void Start()
    {
        m_EnteredPortal = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerEnterVelocity = player.GetComponent<Rigidbody2D>().velocity;

        bool isTouchingOrange = Physics2D.IsTouching(player.GetComponent<BoxCollider2D>(), orangePortal.GetComponent<BoxCollider2D>());
        bool isTouchingBlue = Physics2D.IsTouching(player.GetComponent<BoxCollider2D>(), bluePortal.GetComponent<BoxCollider2D>());

        if (isTouchingOrange && !m_EnteredPortal)
        {
            m_EnteredPortal = true;
            player.transform.position = bluePortal.transform.position;
        }

        if (isTouchingBlue && !m_EnteredPortal)
        {
            m_EnteredPortal = true;
            player.transform.position = orangePortal.transform.position;
        }

        if (!isTouchingBlue && !isTouchingOrange)
        {
            m_EnteredPortal = false;
        }
    }
}
