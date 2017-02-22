using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]

public class StatePatternEnemy : MonoBehaviour {

    public float m_turnSpeed = 5.0f;
    public float m_searchingDuration = 4.0f;
    public float m_sightRange = 20.0f;
    public Transform[] m_waypoints;
    public Transform m_eyes;
    public Vector3 m_offset = new Vector3(0.0f, 0.5f, 0.0f);
   // public MeshRenderer m_meshRendererFlag;

    [HideInInspector]
    public Transform m_chaseTarget;
    [HideInInspector]
    public IEnemyState m_currentState;
    [HideInInspector]
    public ChaseState m_chaseState;
    [HideInInspector]
    public AlertState m_alertState;
    [HideInInspector]
    public PatrolState m_patrolState;
    [HideInInspector]
    public NavMeshAgent m_navMeshAgent;


    private void Awake()
    {
        m_chaseState = new ChaseState(this);
        m_alertState = new AlertState(this);
        m_patrolState = new PatrolState(this);

        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start ()
    {
        m_currentState = m_patrolState;
	}
	
	void Update ()
    {
        m_currentState.UpdateState();
	}

    private void OnTriggerEnter(Collider other)
    {
        m_currentState.OnTriggerEnter(other);
    }

    public void RotateTowards(Transform _target)
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, m_turnSpeed * Time.deltaTime);

    }
}
