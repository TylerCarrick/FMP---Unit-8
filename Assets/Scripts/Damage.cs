using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{


    public int damage;
    

    // Update is called once per frame
    void Update()
    {
       
    }

    
    

    void OnTriggerEnter(Collider collision)
    {
        print(collision.name + "Damagascripthit this");
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyScript ES = collision.GetComponent<EnemyScript>();

            ES.health -= 100;

        }
        
    }



}