using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler m_current;
    public GameObject m_pooledObject;
    public int m_pooledAmount = 20;
    public bool m_willGrow = true;
    public List<GameObject> m_pooledObjects;

    private void Awake()
    {
        m_current = this;
    }

    private void Start()
    {
        m_pooledObjects = new List<GameObject>();

        for (int i = 0; i < m_pooledAmount; i++)
        {
            GameObject tempObj = Instantiate(m_pooledObject);
            tempObj.SetActive(false);
            m_pooledObjects.Add(tempObj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < m_pooledObjects.Count; i++)
        {
            if (!m_pooledObjects[i].activeInHierarchy)
            {
                return m_pooledObjects[i];
            }
        }

        if (m_willGrow)
        {
            GameObject newObj = Instantiate(m_pooledObject);
            newObj.SetActive(false);
            m_pooledObjects.Add(newObj);
            return newObj;
        }

        return null;
    }
}
