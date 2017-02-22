using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    private WeaponFire m_weapon;


    void Start()
    {
        m_weapon = GetComponentInChildren<WeaponFire>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && m_weapon.m_clipEmpty)
        {
            m_weapon.Reload();
        }
        else if (Input.GetButton("Fire1"))
        {
            m_weapon.Fire();
        }
        
    }


}
