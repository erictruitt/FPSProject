using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyState {

    void UpdateState();

    void OnTriggerEnter(Collider _other);

    void ToPatrolState();

    void ToAlertAtate();

    void ToChaseState();
}
