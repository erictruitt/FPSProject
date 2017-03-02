using UnityEngine;

public class AlertState : IEnemyState
{
    private const float TIMETOWAIT = 5.0f;

    private readonly StatePatternEnemy m_enemy;
    private float m_alertMsgHoldTimer = -1.0f;

    public AlertState(StatePatternEnemy _statePatternEnemy)
    {
        m_enemy = _statePatternEnemy;
    }

    public void UpdateState()
    {
        if (m_alertMsgHoldTimer < 0.0f)
        {
            SetTimer();
        }
        else if (Time.time < m_alertMsgHoldTimer)
        {
            TransitionState();
        }
    }

    public void OnTriggerEnter(Collider _other) { }

    public void OnTriggerExit(Collider _other) { }

    public void ToPatrolState() { m_enemy.m_currentState = m_enemy.m_patrolState; }

    public void ToAlertAtate() { Debug.Log("Can't Transition Into Same State"); }

    public void ToChaseState()
    {
        m_enemy.m_currentState = m_enemy.m_chaseState;
        m_alertMsgHoldTimer = -1.0f;
    }

    public void ToRetreatState() { m_enemy.m_currentState = m_enemy.m_retreatState; }

    public void ToAttackState()
    {
        m_enemy.m_currentState = m_enemy.m_attackState;
        m_alertMsgHoldTimer = -1.0f;
    }

    public void ToDeathState() { m_enemy.m_currentState = m_enemy.m_deathState; }

    private void SetTimer() { m_alertMsgHoldTimer += Time.deltaTime + TIMETOWAIT; }

    private void TransitionState()
    {
        m_enemy.InstantiateReinforcements(3, m_enemy.GetRadioLocation().position);

        if (m_enemy.RotateAway(m_enemy.GetRadioLocation()))
        {
            if (m_enemy.TargetInFOV())
            {
                if (m_enemy.GetTargetInZone())
                {
                    ToAttackState();
                    return;
                }
            }

            ToChaseState();
        }
    }
}
