using UnityEngine;

public class DeathState : IEnemyState
{

    private readonly StatePatternEnemy m_enemy;

    public DeathState(StatePatternEnemy _statePatternEnemy) { m_enemy = _statePatternEnemy; }

    public void UpdateState() { }

    public void OnTriggerEnter(Collider _other) { }

    public void ToPatrolState() { }

    public void ToAlertAtate() { }

    public void ToChaseState() { }

    public void ToRetreatState() { }

    public void ToAttackState() { }

    public void ToDeathState() { Debug.Log("Can't Transition Into Same State"); }
}
