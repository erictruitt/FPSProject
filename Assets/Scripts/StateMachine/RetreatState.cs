using UnityEngine;

public class RetreatState : IEnemyState
{

    private readonly StatePatternEnemy m_enemy;

    public RetreatState(StatePatternEnemy _statePatternEnemy) { m_enemy = _statePatternEnemy; }

    public void UpdateState() { }

    public void OnTriggerEnter(Collider _other) { }

    public void ToPatrolState() { }

    public void ToAlertAtate() { }

    public void ToChaseState() { }

    public void ToRetreatState() { Debug.Log("Can't Transition Into Same State"); }

    public void ToAttackState() { }

    public void ToDeathState() { }
}
