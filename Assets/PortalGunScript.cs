using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGunScript : MonoBehaviour
{
    public LineRenderer m_lineRenderer;
    public GameObject m_player;
    public EdgeCollider2D m_edgeCollider;

    public float aimLineLength;
    public float aimLineStartLength;

    Vector3 m_startPos;
    Vector3 m_endPos;
    Vector3 m_mousePos;
    Vector3 m_mouseDir;

    Vector2[] m_colliderpoints;

    // Start is called before the first frame update
    void Start()
    {
        aimLineLength = 10.0f;
        aimLineStartLength = 0.5f;
        m_edgeCollider.enabled = true;
        m_lineRenderer.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        DrawAimLine();
        ContactFilter2D filter = new ContactFilter2D();
        Collider2D[] results = new Collider2D[10];

        int numColliders = m_edgeCollider.OverlapCollider(filter, results);
        for (int i = 0; i < numColliders; i++)
        {
            GameObject collidedObject = results[i].gameObject;
            Debug.Log(collidedObject.tag);
        }
    }

    void DrawAimLine()
    {
        m_colliderpoints = m_edgeCollider.points;

        m_mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 화면상 마우스 좌표 -> 게임상 좌표로 변환.

        m_startPos = m_player.transform.position;
        m_startPos.z = 0;

        m_endPos = m_mousePos;
        m_endPos.z = 0;

        m_mouseDir = (m_endPos - m_startPos);
        m_mouseDir.Normalize(); // 캐릭터 To 마우스까지 방향의 단위벡터.

        m_lineRenderer.SetPosition(0, m_mouseDir * aimLineStartLength + m_startPos); // 캐릭터 좌표에서 선 시작.
        m_lineRenderer.SetPosition(1, m_mouseDir * aimLineLength + m_startPos);

        m_colliderpoints[0] = m_mouseDir * aimLineStartLength + m_startPos; // Edge Collider 선 시작.
        m_colliderpoints[1] = m_mouseDir * aimLineLength + m_startPos; // Edge Collider 선 끝.

        m_edgeCollider.points = m_colliderpoints; // Edge Collider Points 설정.
    }
}