using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject player;
    public GameObject orangePortal;
    public GameObject bluePortal;

    Vector3 playerEnterVelocity; // �÷��̾ ��Ż�� �� ���� �ӵ�.

    bool m_EnteredPortal; // �÷��̾ ��Ż�� �� ��츦 Ȯ���ϴ� Boolean.
    bool isTouchingOrange, isTouchingBlue; // �÷��̾ ��Ż�� ���� ��츦 Ȯ���ϴ� Boolean.

    // Start is called before the first frame update
    void Start()
    {
        m_EnteredPortal = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerEnterVelocity = player.GetComponent<Rigidbody2D>().velocity;

        // �÷��̾ ��Ż�� ��Ҵ°� Ȯ��.
        isTouchingOrange = Physics2D.IsTouching(player.GetComponent<BoxCollider2D>(), orangePortal.GetComponent<BoxCollider2D>());
        isTouchingBlue = Physics2D.IsTouching(player.GetComponent<BoxCollider2D>(), bluePortal.GetComponent<BoxCollider2D>());

        if (isTouchingOrange && !m_EnteredPortal) // ������ ��Ż�� ���� ��� + �̹� �� ���� ���� ���.
        {
            m_EnteredPortal = true;
            player.transform.position = bluePortal.transform.position; //  ������ ��Ż�� ��ġ ��ȯ.
        }

        if (isTouchingBlue && !m_EnteredPortal) // ��� ��Ż�� ���� ��� + �̹� �� ���� ���� ���.
        {
            m_EnteredPortal = true;
            player.transform.position = orangePortal.transform.position; //  ��� ��Ż�� ��ġ ��ȯ.
        }

        if (!isTouchingBlue && !isTouchingOrange)
        {
            m_EnteredPortal = false;
        }
    }
}
