using UnityEngine;

public class AlertState : IEnemyState
{
    private readonly StatePatternEnemy m_enemy;
    private float m_searchTimer;

    public AlertState(StatePatternEnemy _statePatternEnemy)
    {
        m_enemy = _statePatternEnemy;
    }

    public void UpdateState()
    {
        Look();
        Search();
    }

    public void OnTriggerEnter(Collider _other)
    {

    }

    public void ToPatrolState()
    {
        m_enemy.m_currentState = m_enemy.m_patrolState;
        m_searchTimer = 0;
    }

    public void ToAlertAtate() { Debug.Log("Can't Transition Into Same State"); }

    public void ToChaseState()
    {
        m_enemy.m_currentState = m_enemy.m_chaseState;
        m_searchTimer = 0;
    }

    public void ToRetreatState() { }

    public void ToAttackState() { }

    public void ToDeathState() { }

    private void Look()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_enemy.m_eyes.transform.position, m_enemy.m_eyes.transform.forward, out hit, m_enemy.m_sightRange) && hit.collider.CompareTag("Player"))
        {
            m_enemy.m_chaseTarget = hit.transform;
            ToChaseState();
        }
    }

    private void Search()
    {
        m_enemy.m_navMeshAgent.Stop();
        m_enemy.transform.Rotate(0.0f, m_enemy.m_turnSpeed * Time.deltaTime, 0.0f);
        m_searchTimer += Time.deltaTime;

        if (m_searchTimer >= m_enemy.m_searchingDuration)
        {
            ToPatrolState();
        }
    }
}
