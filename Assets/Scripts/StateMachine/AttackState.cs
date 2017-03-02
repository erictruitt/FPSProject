using UnityEngine;

public class AttackState : IEnemyState
{

    private readonly StatePatternEnemy m_enemy;

    public AttackState(StatePatternEnemy _statePatternEnemy) { m_enemy = _statePatternEnemy; }

    public void UpdateState() { }

    public void OnTriggerEnter(Collider _other) { }

    public void ToPatrolState() { }

    public void ToAlertAtate() { }

    public void ToChaseState() { }

    public void ToRetreatState() { }

    public void ToAttackState() { Debug.Log("Can't Transition Into Same State"); }

    public void ToDeathState() { }
}
