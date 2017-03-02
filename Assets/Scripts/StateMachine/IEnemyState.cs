using UnityEngine;

public interface IEnemyState {

    void UpdateState();

    void OnTriggerEnter(Collider _other);

    void OnTriggerExit(Collider _other);

    void ToPatrolState();

    void ToAlertAtate();

    void ToChaseState();

    void ToRetreatState();

    void ToAttackState();

    void ToDeathState();
}
