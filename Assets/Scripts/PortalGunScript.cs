using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGunScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject player;
    public GameObject bluePortal;
    public GameObject orangePortal;

    public float aimLineLength;
    public float aimLineStartLength;

    public int PortalModes; // 1 : ��� ��Ż��, 2 : ���/������ ����.

    Vector3 m_startPos;
    Vector3 m_endPos;
    Vector3 m_mousePos;
    Vector3 m_mouseDir;

    Vector3 m_tempVector;
    Quaternion m_tempQuaternion;

    GameObject m_pointedGameObject;

    float portal_X, portal_Y;
    Vector3 portal_XYZ;

    int m_lastPortalMade; // ���������� ������ ��Ż ǥ��. 0�� ����, 1�� ��� ��Ż, 2�� ������ ��Ż.

    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        aimLineLength = 30.0f; // Aim Line�� ����.
        aimLineStartLength = 0.5f; // Aim Line�� �������� �÷��̾�� ����.
        lineRenderer.enabled = true;

        portal_X = bluePortal.transform.position.x;
        portal_Y = bluePortal.transform.position.y;

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
        m_mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // ȭ��� ���콺 ��ǥ -> ���ӻ� ��ǥ�� ��ȯ.

        m_startPos = player.transform.position;
        m_startPos.z = 0;

        m_endPos = m_mousePos;
        m_endPos.z = 0;

        m_mouseDir = (m_endPos - m_startPos);
        m_mouseDir.Normalize(); // ĳ���� To ���콺���� ������ ��������.

        lineRenderer.SetPosition(0, m_mouseDir * aimLineStartLength + m_startPos); // ĳ���� ��ǥ���� �� ����.
        lineRenderer.SetPosition(1, m_mousePos); // ���콺 ��ǥ���� �� ��.

        hit = Physics2D.Raycast(m_mouseDir * aimLineStartLength + m_startPos, m_mouseDir, 300);
        lineRenderer.SetPosition(1, hit.point); // ���콺 ��ǥ���� �� ��.

        lineRenderer.endColor = Color.blue;
        if(PortalModes == 1)
        {
            lineRenderer.startColor = Color.blue;
        }
        else
        {
            lineRenderer.startColor = Color.red;
        }
        
    }

    private void HandlePortalCreation()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hit.collider.tag == "Portalable")
            {
                m_pointedGameObject = hit.collider.gameObject;
                portal_X = hit.point.x;
                portal_Y = hit.point.y;
            }
            m_tempVector = bluePortal.transform.position; // ���� ��Ż ��ġ ����.
            m_tempQuaternion = bluePortal.transform.rotation; // ���� ��Ż ���� ����.

            bluePortal.transform.position = new Vector3(portal_X, portal_Y, 0);
            bluePortal.transform.rotation = m_pointedGameObject.transform.rotation;

            m_lastPortalMade = 1; //  ��� ��Ż ������ ǥ��.
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (hit.collider.tag == "Portalable")
            {
                m_pointedGameObject = hit.collider.gameObject;
                portal_X = hit.point.x;
                portal_Y = hit.point.y;
            }
            if (PortalModes == 2) {
                m_tempVector = orangePortal.transform.position; // ���� ��Ż ��ġ ����.
                m_tempQuaternion = orangePortal.transform.rotation; // ���� ��Ż ���� ����.

                orangePortal.transform.position = new Vector3(portal_X, portal_Y, 0);
                orangePortal.transform.rotation = m_pointedGameObject.transform.rotation;

                m_lastPortalMade = 2; //  ������ ��Ż ������ ǥ��.           
            }
            else
            {
                m_tempVector = bluePortal.transform.position; // ���� ��Ż ��ġ ����.
                m_tempQuaternion = bluePortal.transform.rotation; // ���� ��Ż ���� ����.

                bluePortal.transform.position = new Vector3(portal_X, portal_Y, 0);
                bluePortal.transform.rotation = m_pointedGameObject.transform.rotation;

                m_lastPortalMade = 1; //  ��� ��Ż ������ ǥ��.   
            }
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