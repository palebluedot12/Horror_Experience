using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f; //회전속도 (인스펙터창에 나타나게 하기 위해 public)
    Animator m_Animator;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f); //수평이동이 있는지 여부 파악
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f); //수직 ""
        bool isWalking = hasHorizontalInput || hasVerticalInput; //수평, 수직 하나라도 있는지 확인
        m_Animator.SetBool("IsWalking", isWalking);//파리미터 이름, 데이터 값 순서로
        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }

        }
        else
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f); 
        //현재 회전값, 목표 회전값, 각도의 변화, 크기의 변화
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove() //루트 모션을 적용하여 이동과 회전을 개별적으로 적용
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

}
