using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public GameObject player;
    public GameObject orangePortal;
    public GameObject bluePortal;

    Vector3 playerEnterVelocity; // 플레이어가 포탈에 들어갈 때의 속도.

    bool m_EnteredPortal; // 플레이어가 포탈에 들어간 경우를 확인하는 Boolean.
    bool isTouchingOrange, isTouchingBlue; // 플레이어가 포탈에 닿은 경우를 확인하는 Boolean.

    // Start is called before the first frame update
    void Start()
    {
        m_EnteredPortal = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerEnterVelocity = player.GetComponent<Rigidbody2D>().velocity;

        // 플레이어가 포탈에 닿았는가 확인.
        isTouchingOrange = Physics2D.IsTouching(player.GetComponent<BoxCollider2D>(), orangePortal.GetComponent<BoxCollider2D>());
        isTouchingBlue = Physics2D.IsTouching(player.GetComponent<BoxCollider2D>(), bluePortal.GetComponent<BoxCollider2D>());

        if (isTouchingOrange && !m_EnteredPortal) // 오렌지 포탈에 닿은 경우 + 이미 들어간 적이 없는 경우.
        {
            m_EnteredPortal = true;
            player.transform.position = bluePortal.transform.position; //  오렌지 포탈로 위치 변환.
        }

        if (isTouchingBlue && !m_EnteredPortal) // 블루 포탈에 닿은 경우 + 이미 들어간 적이 없는 경우.
        {
            m_EnteredPortal = true;
            player.transform.position = orangePortal.transform.position; //  블루 포탈로 위치 변환.
        }

        if (!isTouchingBlue && !isTouchingOrange)
        {
            m_EnteredPortal = false;
        }
    }
}
