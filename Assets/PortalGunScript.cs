using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGunScript : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public GameObject player;
    public EdgeCollider2D edgeCollider;
    public GameObject placeholder;
    public GameObject bluePortal;
    public GameObject orangePortal;

    public float aimLineLength;
    public float aimLineStartLength;

    Vector3 m_startPos;
    Vector3 m_endPos;
    Vector3 m_mousePos;
    Vector3 m_mouseDir;

    Vector2[] m_colliderpoints;

    GameObject m_pointedGameObject;
    bool m_pointedFound;

    float portal_X, portal_Y;
    Vector3 portal_XYZ;

    // Start is called before the first frame update
    void Start()
    {
        aimLineLength = 30.0f;
        aimLineStartLength = 0.0f;
        edgeCollider.enabled = true;
        lineRenderer.enabled = true;
        m_pointedFound = false;
    }

    // Update is called once per frame
    void Update()
    {
        DrawAimLine();
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
            m_pointedFound = true;
        }

        if (m_pointedFound)
        {
            // 1 : player
            // 2 : ground

            float beta1, beta2;
            beta1 = player.transform.position.y - (m_mouseDir.y / m_mouseDir.x) * player.transform.position.x;
            beta2 = m_pointedGameObject.transform.position.y - Mathf.Tan(m_pointedGameObject.transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * m_pointedGameObject.transform.position.x;

            portal_X = (beta2 - beta1) / ((m_mouseDir.y / m_mouseDir.x) - Mathf.Tan(m_pointedGameObject.transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
            portal_Y = (m_mouseDir.y / m_mouseDir.x) * portal_X + beta1;
        }

        if (m_pointedFound && Input.GetKey(KeyCode.Mouse0))
        {
            bluePortal.transform.position = new Vector3(portal_X, portal_Y, 0);
            bluePortal.transform.rotation = m_pointedGameObject.transform.rotation;
        }

        if (m_pointedFound && Input.GetKey(KeyCode.Mouse1))
        {
            orangePortal.transform.position = new Vector3(portal_X, portal_Y, 0);
            orangePortal.transform.rotation = m_pointedGameObject.transform.rotation;
        }

        m_pointedFound = false;
    }
}