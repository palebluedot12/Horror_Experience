using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    bool m_IsPlayerInRange;


    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit(Collider other) //중간에 벽이 있는 경우.
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            //벡터 수학에 따르면 A부터 B까지의 벡터는 B - A입니다.
            //따라서 PointOfView 게임 오브젝트에서 JohnLemon까지의 방향은 JohnLemon의 포지션에서 PointOfView 게임 오브젝트의 포지션을 뺀 값입니다. 
           
            Ray ray = new Ray(transform.position, direction); //레이의 원점, 방향
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    gameEnding.CaughtPlayer();//레이캐스트에 걸리고, 그것이 플레이어라면
                }
            }
        }
    }
}
