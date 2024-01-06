using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGunScript : MonoBehaviour
{
    public LineRenderer m_lineRenderer;
    public GameObject m_player;

    public float aimLineLength;

    Vector3 m_startPos;
    Vector3 m_endPos;
    Vector3 m_mousePos;
    Vector3 m_mouseDir;

    // Start is called before the first frame update
    void Start()
    {
        aimLineLength = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        DrawAimLine();
    }

    void DrawAimLine()
    {
        m_mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // 화면상 마우스 좌표 -> 게임상 좌표로 변환.

        m_lineRenderer.enabled = true;

        m_startPos = m_player.transform.position;
        m_startPos.z = 0;
        m_lineRenderer.SetPosition(0, m_startPos); // 캐릭터 좌표에서 선 시작.

        m_endPos = m_mousePos;
        m_endPos.z = 0;

        m_mouseDir = (m_endPos - m_startPos);
        m_mouseDir.Normalize(); // 캐릭터 To 마우스까지 방향의 단위벡터.

        m_lineRenderer.SetPosition(1, m_mouseDir * aimLineLength + m_startPos);
    }
}
