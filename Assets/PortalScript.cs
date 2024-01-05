using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject player;
    public GameObject orangePortal;
    public GameObject bluePortal;

    Vector3 m_playerVelocity; // �÷��̾ ��Ż�� �� ���� �ӵ�.

    bool m_EnteredPortal; // �÷��̾ ��Ż�� �� ��츦 Ȯ���ϴ� Boolean.
    bool isTouchingOrange, isTouchingBlue; // �÷��̾ ��Ż�� ���� ��츦 Ȯ���ϴ� Boolean.

    float m_PlayerVelocityAngle; // �÷��̾ ��Ż�� �� ���� �ӵ��� ���� Degree
    float m_newPlayerVelocityAngle; // �÷��̾ ��Ż�� ���� ���� �ӵ��� ���� Degree

    float m_orangePortalRotation;
    float m_bluePortalRotation;

    // Start is called before the first frame update
    void Start()
    {
        m_EnteredPortal = false;

        m_orangePortalRotation = orangePortal.transform.rotation.eulerAngles.z;
        m_bluePortalRotation = bluePortal.transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        m_playerVelocity = player.GetComponent<Rigidbody2D>().velocity;

        m_PlayerVelocityAngle = Mathf.Atan2(m_playerVelocity.y, m_playerVelocity.x) * Mathf.Rad2Deg; //  ��Ż�� �� �� ���� Degree
        m_newPlayerVelocityAngle = 360 - m_PlayerVelocityAngle + m_orangePortalRotation + m_bluePortalRotation; // ��Ż�� ������ ���� Degree

        // ��Ż�� ���� �� ���� ����.
        Vector3 desiredDirection = new Vector3(Mathf.Cos(m_newPlayerVelocityAngle * Mathf.Deg2Rad), Mathf.Sin(m_newPlayerVelocityAngle * Mathf.Deg2Rad), 0);


        // �÷��̾ ��Ż�� ��Ҵ°� Ȯ��.
        isTouchingOrange = Physics2D.IsTouching(player.GetComponent<BoxCollider2D>(), orangePortal.GetComponent<BoxCollider2D>());
        isTouchingBlue = Physics2D.IsTouching(player.GetComponent<BoxCollider2D>(), bluePortal.GetComponent<BoxCollider2D>());

        if (isTouchingOrange && !m_EnteredPortal) // ������ ��Ż�� ���� ��� + �̹� �� ���� ���� ���.
        {
            m_EnteredPortal = true;
            player.transform.position = bluePortal.transform.position; //  ������ ��Ż�� ��ġ ��ȯ.
            player.GetComponent<Rigidbody2D>().velocity = desiredDirection.normalized * m_playerVelocity.magnitude; // ��Ż�� ������ �ӵ��� ���� ��ȯ.
        }

        if (isTouchingBlue && !m_EnteredPortal) // ��� ��Ż�� ���� ��� + �̹� �� ���� ���� ���.
        {
            m_EnteredPortal = true;
            player.transform.position = orangePortal.transform.position; //  ��� ��Ż�� ��ġ ��ȯ.
            player.GetComponent<Rigidbody2D>().velocity = desiredDirection.normalized * m_playerVelocity.magnitude; // ��Ż�� ������ �ӵ��� ���� ��ȯ.
        }

        if (!isTouchingBlue && !isTouchingOrange)
        {
            m_EnteredPortal = false;
        }
    }
}
