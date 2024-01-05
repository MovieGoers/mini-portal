using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject player;
    public GameObject orangePortal;
    public GameObject bluePortal;

    Vector3 m_playerVelocity; // 플레이어가 포탈에 들어갈 때의 속도.

    bool m_EnteredPortal; // 플레이어가 포탈에 들어간 경우를 확인하는 Boolean.
    bool isTouchingOrange, isTouchingBlue; // 플레이어가 포탈에 닿은 경우를 확인하는 Boolean.

    float m_PlayerVelocityAngle; // 플레이어가 포탈에 들어갈 때의 속도의 각도 Degree
    float m_newPlayerVelocityAngle; // 플레이어가 포탈에 나갈 때의 속도의 각도 Degree

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

        m_PlayerVelocityAngle = Mathf.Atan2(m_playerVelocity.y, m_playerVelocity.x) * Mathf.Rad2Deg; //  포탈을 들어갈 때 각도 Degree
        m_newPlayerVelocityAngle = 360 - m_PlayerVelocityAngle + m_orangePortalRotation + m_bluePortalRotation; // 포탈을 나갈때 각도 Degree

        // 포탈을 나갈 때 방향 벡터.
        Vector3 desiredDirection = new Vector3(Mathf.Cos(m_newPlayerVelocityAngle * Mathf.Deg2Rad), Mathf.Sin(m_newPlayerVelocityAngle * Mathf.Deg2Rad), 0);


        // 플레이어가 포탈에 닿았는가 확인.
        isTouchingOrange = Physics2D.IsTouching(player.GetComponent<BoxCollider2D>(), orangePortal.GetComponent<BoxCollider2D>());
        isTouchingBlue = Physics2D.IsTouching(player.GetComponent<BoxCollider2D>(), bluePortal.GetComponent<BoxCollider2D>());

        if (isTouchingOrange && !m_EnteredPortal) // 오렌지 포탈에 닿은 경우 + 이미 들어간 적이 없는 경우.
        {
            m_EnteredPortal = true;
            player.transform.position = bluePortal.transform.position; //  오렌지 포탈로 위치 변환.
            player.GetComponent<Rigidbody2D>().velocity = desiredDirection.normalized * m_playerVelocity.magnitude; // 포탈을 나갈때 속도의 방향 변환.
        }

        if (isTouchingBlue && !m_EnteredPortal) // 블루 포탈에 닿은 경우 + 이미 들어간 적이 없는 경우.
        {
            m_EnteredPortal = true;
            player.transform.position = orangePortal.transform.position; //  블루 포탈로 위치 변환.
            player.GetComponent<Rigidbody2D>().velocity = desiredDirection.normalized * m_playerVelocity.magnitude; // 포탈을 나갈때 속도의 방향 변환.
        }

        if (!isTouchingBlue && !isTouchingOrange)
        {
            m_EnteredPortal = false;
        }
    }
}
