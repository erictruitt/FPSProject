using UnityEngine;

public class ChaseState : IEnemyState
{
    private readonly StatePatternEnemy m_enemy;

    public ChaseState(StatePatternEnemy _statePatternEnemy)
    {
        m_enemy = _statePatternEnemy;
    }

    public void UpdateState()
    {
        //Look();
        //Chase();
    }

    public void OnTriggerEnter(Collider _other) { }

    public void OnTriggerExit(Collider _other) { }

    public void ToPatrolState() { }

    public void ToAlertAtate() { m_enemy.m_currentState = m_enemy.m_alertState; }

    public void ToChaseState() { Debug.Log("Can't Transition Into Same State"); }

    public void ToRetreatState() { }

    public void ToAttackState() { }

    public void ToDeathState() { }

    //private void Look()
    //{
    //    RaycastHit hit;
    //    Vector3 enemyToTarget = (m_enemy.m_chaseTarget.position + m_enemy.m_offset) - m_enemy.m_eyes.transform.position;
    //    if (Physics.Raycast(m_enemy.m_eyes.transform.position, enemyToTarget, out hit, m_enemy.m_sightRange) && hit.collider.CompareTag("Player"))
    //    {
    //        m_enemy.m_chaseTarget = hit.transform;
    //    }
    //    else
    //    {
    //        ToAlertAtate();
    //    }
    //}

    //private void Chase()
    //{
    //    m_enemy.m_navMeshAgent.SetDestination(m_enemy.m_chaseTarget.position);
    //    m_enemy.m_navMeshAgent.Resume();
    //}
}
