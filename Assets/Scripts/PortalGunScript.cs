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

    int m_lastPortalMade; // 마지막으로 생성된 포탈 표시. 0은 없음, 1은 블루 포탈, 2은 오렌지 포탈.

    // Start is called before the first frame update
    void Start()
    {
        aimLineLength = 30.0f; // Aim Line의 길이.
        aimLineStartLength = 0.5f; // Aim Line의 시작점과 플레이어간의 간격.
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

        m_mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 화면상 마우스 좌표 -> 게임상 좌표로 변환.

        m_startPos = player.transform.position;
        m_startPos.z = 0;

        m_endPos = m_mousePos;
        m_endPos.z = 0;

        m_mouseDir = (m_endPos - m_startPos);
        m_mouseDir.Normalize(); // 캐릭터 To 마우스까지 방향의 단위벡터.

        lineRenderer.SetPosition(0, m_mouseDir * aimLineStartLength + m_startPos); // 캐릭터 좌표에서 선 시작.
        lineRenderer.SetPosition(1, m_mousePos); // 마우스 좌표에서 선 끝.

        m_colliderpoints[0] = m_mouseDir * aimLineStartLength + m_startPos; // Edge Collider 선 시작.
        m_colliderpoints[1] = m_mouseDir * aimLineLength + m_startPos; // Edge Collider 선 끝.

        edgeCollider.points = m_colliderpoints; // Edge Collider Points 설정.
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
            m_tempVector = bluePortal.transform.position; // 이전 포탈 위치 저장.
            m_tempQuaternion = bluePortal.transform.rotation; // 이전 포탈 각도 저장.

            bluePortal.transform.position = new Vector3(portal_X, portal_Y, 0);
            bluePortal.transform.rotation = m_pointedGameObject.transform.rotation;

            m_lastPortalMade = 1; //  블루 포탈 생성됨 표시.
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            m_tempVector = orangePortal.transform.position; // 이전 포탈 위치 저장.
            m_tempQuaternion = orangePortal.transform.rotation; // 이전 포탈 각도 저장.

            orangePortal.transform.position = new Vector3(portal_X, portal_Y, 0);
            orangePortal.transform.rotation = m_pointedGameObject.transform.rotation;

            m_lastPortalMade = 2; //  오렌지 포탈 생성됨 표시.
        }

        // 두 포탈이 겹치는 경우,
        Bounds blueBound = bluePortal.GetComponent<BoxCollider2D>().bounds;
        Bounds orangeBound = orangePortal.GetComponent<BoxCollider2D>().bounds;

        if (blueBound.Intersects(orangeBound))
        {
            // 마지막으로 쏜 포탈이 블루 포탈인 경우,
            if (m_lastPortalMade == 1)
            {
                bluePortal.transform.position = m_tempVector;
                bluePortal.transform.rotation = m_tempQuaternion;
                m_lastPortalMade = 0;
            }

            // 마지막으로 쏜 포탈이 오렌지 포탈인 경우,
            if (m_lastPortalMade == 2)
            {
                orangePortal.transform.position = m_tempVector;
                orangePortal.transform.rotation = m_tempQuaternion;
                m_lastPortalMade = 0;
            }
        }


    }
}