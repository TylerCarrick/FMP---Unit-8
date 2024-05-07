using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class HomingMissile : MonoBehaviour
{

    public Transform rocketTarget;
    public Rigidbody rocketRigidbody;
    public GameObject player;

    


    public float turn;
    public float rocketVelocity;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
    }
    private void Update()
    {
        rocketTarget = player.transform;
        rocketRigidbody.velocity = transform.forward * rocketVelocity;

        var rocketTargetRotation = Quaternion.LookRotation(rocketTarget.position - transform.position);

        rocketRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, rocketTargetRotation, turn));



    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
           
           if(collision.gameObject.TryGetComponent<ThirdPersonMovement>(out ThirdPersonMovement playerComponent))
            {
                playerComponent.TakeDamage(50);
            }
        }
       Destroy(gameObject);
    }


}
