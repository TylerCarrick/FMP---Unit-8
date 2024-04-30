using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public Camera cam;

    public int damage;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SendRaycast();
        }
    }

    void SendRaycast()
    {
        RaycastHit _hitInfo;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out _hitInfo))
        {
            EnemyScript Unit;
            _hitInfo.transform.TryGetComponent<EnemyScript>(out Unit);
            if (Unit != null)
            {
                Unit.TakeDamage(damage);  
            }
        }
    }



}
