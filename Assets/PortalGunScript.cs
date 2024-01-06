using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalGunScript : MonoBehaviour
{
    public LineRenderer m_lineRenderer;
    public GameObject m_player;

    Vector3 m_startPos;
    Vector3 m_endPos;
    Vector3 m_mousePos;
    Vector3 m_mouseDir;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DrawAimLine();
    }

    void DrawAimLine()
    {
        m_mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        m_lineRenderer.enabled = true;

        m_startPos = m_player.transform.position;
        m_startPos.z = 0;
        m_lineRenderer.SetPosition(0, m_startPos);

        m_endPos = m_mousePos;
        m_endPos.z = 0;
        m_lineRenderer.SetPosition(1, m_endPos);
    }
}
