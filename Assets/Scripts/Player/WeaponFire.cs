using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponFire : MonoBehaviour
{
    //[SerializeField] private string m_name = "Weapon";
    [SerializeField]
    private float m_fireRate = 0.1f;
    [SerializeField]
    private int m_reserveAmmo = 0;
    [SerializeField]
    private int m_ammoInClip = m_clipSize;
    [SerializeField]
    private ParticleSystem m_muzzleFlash;
    [SerializeField]
    private Vector3 m_recoilRotation;
    [SerializeField]
    private Vector3 m_recoilKickback;
    [SerializeField]
    private Transform m_recoilHolder;
    [SerializeField]
    private Transform m_cameraRecoilHolder;
    [SerializeField]
    private AudioClip m_reloadSFX;
    [SerializeField]
    private AudioClip m_fireSFX;
    [SerializeField]
    private AudioClip m_dryFireSFX;
    [SerializeField]
    private Text m_clipAmmoUI;
    [SerializeField]
    private Text m_reserveAmmoUI;

    public bool m_clipEmpty;

    private const int m_clipSize = 30;

    private Vector3 m_currRecoil1;
    private Vector3 m_currRecoil2;
    private Vector3 m_currRecoil3;
    private Vector3 m_currRecoil4;

    private float m_fireHoldTimer;

    private AudioSource m_audioSource;

     

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_fireHoldTimer = 0.0f;
        m_clipEmpty = false;
        m_clipAmmoUI.text = m_ammoInClip.ToString();
        m_reserveAmmoUI.text = m_reserveAmmo.ToString();
    }

    private void FixedUpdate()
    {
        RecoilController();
    }

    private void Update()
    {
        if (m_ammoInClip <= 0)
            m_clipEmpty = true;

    }

    public void Fire()
    {
        if (m_fireHoldTimer > Time.time)
            return;

        m_ammoInClip--;
        m_clipAmmoUI.text = m_ammoInClip.ToString();

        m_audioSource.Play();
        m_muzzleFlash.Play();


        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width * 0.5f, Screen.height * 0.5f));
        if (Physics.Raycast(ray, out hit, 1000.0f) && m_ammoInClip > 0)
        {
            if (hit.transform.tag == "Environment")
            {
                GameObject obj = ObjectPooler.m_current.GetPooledObject();

                if (obj != null)
                {
                    obj.transform.position = hit.point;
                    obj.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);
                    obj.SetActive(true);
                }
            }


        }
            m_currRecoil1 += new Vector3(m_recoilRotation.x, Random.Range(-m_recoilRotation.y, m_recoilRotation.y));
            m_currRecoil3 += new Vector3(Random.Range(-m_recoilKickback.x, m_recoilKickback.x), Random.Range(-m_recoilKickback.y, m_recoilKickback.y), m_recoilKickback.z);

        m_fireHoldTimer = Time.time + m_fireRate;
    }

    public void Reload()
    {
        if (!m_audioSource.isPlaying && m_ammoInClip > 0)
        {
            m_clipEmpty = false;
            m_audioSource.clip = m_fireSFX;
            m_clipAmmoUI.text = m_ammoInClip.ToString();
            return;
        }
        else if (m_ammoInClip > 0)
        {
            m_clipAmmoUI.text = m_ammoInClip.ToString();
            return;
        }
        else if (m_reserveAmmo == 0)
        {
            if (m_audioSource.clip != m_dryFireSFX)
                m_audioSource.clip = m_dryFireSFX;

            if (!m_audioSource.isPlaying)
                m_audioSource.Play();

            return;
        }

        m_audioSource.clip = m_reloadSFX;
        m_audioSource.Play();

        if (m_reserveAmmo < m_clipSize)
        {
            m_ammoInClip += m_reserveAmmo;
            m_reserveAmmo = 0;
            m_reserveAmmoUI.text = m_reserveAmmo.ToString();
        }
        else
        {
            m_ammoInClip += m_clipSize;
            m_reserveAmmo -= m_clipSize;
            m_reserveAmmoUI.text = m_reserveAmmo.ToString();
        }

    }

    private void RecoilController()
    {
        m_currRecoil1 = Vector3.Lerp(m_currRecoil1, Vector3.zero, 0.1f);
        m_currRecoil2 = Vector3.Lerp(m_currRecoil2, m_currRecoil1, 0.1f);

        m_currRecoil3 = Vector3.Lerp(m_currRecoil3, Vector3.zero, 0.1f);
        m_currRecoil4 = Vector3.Lerp(m_currRecoil4, m_currRecoil3, 0.1f);

        m_recoilHolder.localEulerAngles = m_currRecoil2;
        m_recoilHolder.localPosition = m_currRecoil4;

        m_cameraRecoilHolder.localEulerAngles = m_currRecoil2 * 0.8f;
    }
}
