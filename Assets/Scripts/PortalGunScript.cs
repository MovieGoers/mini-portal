using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGunScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject player;
    public EdgeCollider2D edgeCollider;
    public GameObject bluePortal;
    public GameObject orangePortal;

    public float aimLineLength;
    public float aimLineStartLength;

    Vector3 m_startPos;
    Vector3 m_endPos;
    Vector3 m_mousePos;
    Vector3 m_mouseDir;

    Vector3 m_tempVector;
    Quaternion m_tempQuaternion;

    Vector2[] m_colliderpoints;

    GameObject m_pointedGameObject;

    float portal_X, portal_Y;
    Vector3 portal_XYZ;

    int m_lastPortalMade; // ���������� ������ ��Ż ǥ��. 0�� ����, 1�� ��� ��Ż, 2�� ������ ��Ż.

    // Start is called before the first frame update
    void Start()
    {
        aimLineLength = 30.0f; // Aim Line�� ����.
        aimLineStartLength = 0.5f; // Aim Line�� �������� �÷��̾�� ����.
        edgeCollider.enabled = true;
        lineRenderer.enabled = true;

        m_lastPortalMade = 0;
    }

    // Update is called once per frame
    void Update()
    {
        DrawAimLine();
        HandlePortalCreation();
    }

    void DrawAimLine()
    {
        m_colliderpoints = edgeCollider.points;

        m_mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ȭ��� ���콺 ��ǥ -> ���ӻ� ��ǥ�� ��ȯ.

        m_startPos = player.transform.position;
        m_startPos.z = 0;

        m_endPos = m_mousePos;
        m_endPos.z = 0;

        m_mouseDir = (m_endPos - m_startPos);
        m_mouseDir.Normalize(); // ĳ���� To ���콺���� ������ ��������.

        lineRenderer.SetPosition(0, m_mouseDir * aimLineStartLength + m_startPos); // ĳ���� ��ǥ���� �� ����.
        lineRenderer.SetPosition(1, m_mousePos); // ���콺 ��ǥ���� �� ��.

        m_colliderpoints[0] = m_mouseDir * aimLineStartLength + m_startPos; // Edge Collider �� ����.
        m_colliderpoints[1] = m_mouseDir * aimLineLength + m_startPos; // Edge Collider �� ��.

        edgeCollider.points = m_colliderpoints; // Edge Collider Points ����.
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Portalable")
        {
            m_pointedGameObject = collision.gameObject;

            float beta1, beta2;
            beta1 = player.transform.position.y - (m_mouseDir.y / m_mouseDir.x) * player.transform.position.x;
            beta2 = m_pointedGameObject.transform.position.y - Mathf.Tan(m_pointedGameObject.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * m_pointedGameObject.transform.position.x;

            portal_X = (beta2 - beta1) / ((m_mouseDir.y / m_mouseDir.x) - Mathf.Tan(m_pointedGameObject.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
            portal_Y = (m_mouseDir.y / m_mouseDir.x) * portal_X + beta1;
        }
    }

    private void HandlePortalCreation()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_tempVector = bluePortal.transform.position; // ���� ��Ż ��ġ ����.
            m_tempQuaternion = bluePortal.transform.rotation; // ���� ��Ż ���� ����.

            bluePortal.transform.position = new Vector3(portal_X, portal_Y, 0);
            bluePortal.transform.rotation = m_pointedGameObject.transform.rotation;

            m_lastPortalMade = 1; //  ��� ��Ż ������ ǥ��.
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            m_tempVector = orangePortal.transform.position; // ���� ��Ż ��ġ ����.
            m_tempQuaternion = orangePortal.transform.rotation; // ���� ��Ż ���� ����.

            orangePortal.transform.position = new Vector3(portal_X, portal_Y, 0);
            orangePortal.transform.rotation = m_pointedGameObject.transform.rotation;

            m_lastPortalMade = 2; //  ������ ��Ż ������ ǥ��.
        }

        // �� ��Ż�� ��ġ�� ���,
        Bounds blueBound = bluePortal.GetComponent<BoxCollider2D>().bounds;
        Bounds orangeBound = orangePortal.GetComponent<BoxCollider2D>().bounds;

        if (blueBound.Intersects(orangeBound))
        {
            // ���������� �� ��Ż�� ��� ��Ż�� ���,
            if (m_lastPortalMade == 1)
            {
                bluePortal.transform.position = m_tempVector;
                bluePortal.transform.rotation = m_tempQuaternion;
                m_lastPortalMade = 0;
            }

            // ���������� �� ��Ż�� ������ ��Ż�� ���,
            if (m_lastPortalMade == 2)
            {
                orangePortal.transform.position = m_tempVector;
                orangePortal.transform.rotation = m_tempQuaternion;
                m_lastPortalMade = 0;
            }
        }


    }
}