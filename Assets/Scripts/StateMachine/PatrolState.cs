using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState {

    private const float TIMETOWAIT = 5.0f;

    private readonly StatePatternEnemy m_enemy;
    private int m_nextWaypoint;

    private float m_waitTimer;


    public PatrolState(StatePatternEnemy _statePatternEnemy)
    {
        m_enemy = _statePatternEnemy;
        m_nextWaypoint = 0;
        m_waitTimer = Time.time;
    }

    public void UpdateState()
    {
        //Look();
        Patrol();
    }

    public void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.CompareTag("Player"))
        {
            //ToAlertAtate();
        }
    }

    public void ToPatrolState() { Debug.Log("Can't Transition Into Same State"); }

    public void ToAlertAtate() { m_enemy.m_currentState = m_enemy.m_alertState; }

    public void ToChaseState() { m_enemy.m_currentState = m_enemy.m_chaseState; }

    private void Look()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_enemy.m_eyes.transform.position, m_enemy.m_eyes.transform.forward, out hit, m_enemy.m_sightRange) && hit.collider.CompareTag("Player"))
        {
            m_enemy.m_chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    void Patrol()
    {
        if (Time.time < m_waitTimer)
        {
            m_enemy.RotateTowards(m_enemy.m_waypoints[m_nextWaypoint].gameObject.transform);
            return;
        }

        m_enemy.m_navMeshAgent.updatePosition = true;
        m_enemy.m_navMeshAgent.updateRotation = true;
        m_enemy.m_navMeshAgent.SetDestination(m_enemy.m_waypoints[m_nextWaypoint].position);
        m_enemy.m_navMeshAgent.Resume();

        if (m_enemy.m_navMeshAgent.remainingDistance <= m_enemy.m_navMeshAgent.stoppingDistance && !m_enemy.m_navMeshAgent.pathPending)
        {
            m_enemy.m_navMeshAgent.Stop();
            m_enemy.m_navMeshAgent.updatePosition = false;
            m_enemy.m_navMeshAgent.updateRotation = false;

            m_nextWaypoint = (m_nextWaypoint + 1) % m_enemy.m_waypoints.Length;

            m_waitTimer = Time.time + TIMETOWAIT;
        }
    }

}
