using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHoleDestroy : MonoBehaviour {

    private void OnEnable()
    {
        Invoke("Destroy", 10.0f);
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
