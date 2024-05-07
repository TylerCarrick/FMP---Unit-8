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

    }
    

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "Player")
        {

            if (collision.gameObject.TryGetComponent<EnemyScript>(out EnemyScript enemyComponent))
            {
                enemyComponent.TakeDamage(50);
            }
        }
        Destroy(gameObject);
    }



}