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
    public float m_FOV = 110.0f;
    public float m_currHealth;

    private bool m_targetInAttackZone;
    private GameObject m_currTarget;
    private float m_maxHealth;
    private bool m_hasRadioLocation;
    private Transform m_radioLocation;
    private GameObject m_reinforcement;
   // public MeshRenderer m_meshRendererFlag;

    [HideInInspector]
    public Transform m_chaseTarget;
    //[HideInInspector]
    public IEnemyState m_currentState;
    [HideInInspector]
    public ChaseState m_chaseState;
    [HideInInspector]
    public AlertState m_alertState;
    [HideInInspector]
    public PatrolState m_patrolState;
    [HideInInspector]
    public RetreatState m_retreatState;
    [HideInInspector]
    public AttackState m_attackState;
    [HideInInspector]
    public DeathState m_deathState;
    [HideInInspector]
    public NavMeshAgent m_navMeshAgent;


    private void Awake()
    {
        m_chaseState = new ChaseState(this);
        m_alertState = new AlertState(this);
        m_patrolState = new PatrolState(this);
        m_retreatState = new RetreatState(this);
        m_attackState = new AttackState(this);
        m_deathState = new DeathState(this);

        m_navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Start ()
    {
        m_targetInAttackZone = false;
        m_currTarget = null;
        m_currHealth = m_maxHealth = 5.0f;
        m_reinforcement = gameObject;
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

    private void OnTriggerExit(Collider other)
    {
        m_currentState.OnTriggerExit(other);
    }

    public void SetTargetInZone(bool _inZone) { m_targetInAttackZone = _inZone; }

    public bool GetTargetInZone() { return m_targetInAttackZone; }

    public void SetTargetGameObject(GameObject _target) { m_currTarget = _target; }

    public GameObject GetTargetGameObject() { return m_currTarget; }

    public void SetHasRadioLocation(bool _hasLocation) { m_hasRadioLocation = _hasLocation; }

    public bool GetHasRadioLocation() { return m_hasRadioLocation; }

    public void SetRadioLocation(Transform _position) { m_radioLocation = _position; }

    public Transform GetRadioLocation() { return m_radioLocation; }

    public bool IsHealthCritical() { return ((m_currHealth / m_maxHealth) < 0.5f) ? true : false; }

    public void RotateTowards(Transform _target)
    {
        Vector3 direction = (_target.position - transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, m_turnSpeed * Time.deltaTime);
    }

    public bool RotateAway(Transform _target)
    {
        Vector3 direction = (transform.position - _target.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, m_turnSpeed * Time.deltaTime);

        if (transform.rotation == rotation)
            return true;

        return false;
    }

    public bool TargetInFOV()
    {
        Vector3 m_directionToTarget = transform.position - m_currTarget.transform.position;

        float angleToTarget = Vector3.Angle(m_directionToTarget, transform.forward);

        return angleToTarget > m_FOV;
    }

    public void InstantiateReinforcements(int _numReinforcements, Vector3 _position)
    {
        for (int i = 1; i <= _numReinforcements; i++)
        {
            GameObject temp = Instantiate(gameObject, new Vector3(_position.x, _position.y + (i * 5), _position.z), Quaternion.identity);
            gameObject.GetComponent<StatePatternEnemy>().m_currentState = m_chaseState;
        }
    }
}
