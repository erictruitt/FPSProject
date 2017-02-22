using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GrunkAnimControl : MonoBehaviour
{
    private Animator m_animator;

    [SerializeField]
    private Vector3 m_prevPosition;
    [SerializeField]
    private float m_currSpeed;
    [SerializeField]
    private float m_maxSpeed;

    [SerializeField]
    private float m_turnAngle;
    [SerializeField]
    private Vector3 m_prevForward;


    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_prevPosition = transform.position;
        m_maxSpeed = GetComponent<NavMeshAgent>().speed;
        m_turnAngle = 0;
        m_prevForward = transform.forward;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        m_turnAngle = Vector3.Angle(m_prevForward, transform.forward);

        Mathf.Clamp(m_turnAngle, -1.0f, 1.0f);

        m_prevForward = transform.forward;

        Vector3 curMove = transform.position - m_prevPosition;

        m_currSpeed = curMove.magnitude / Time.fixedDeltaTime;
        m_currSpeed /= m_maxSpeed;
        Mathf.Clamp(m_currSpeed, -1.0f, 1.0f);

        m_prevPosition = transform.position;
        m_animator.SetFloat("Speed", m_currSpeed);
        m_animator.SetFloat("Direction", m_turnAngle);

    }
}
