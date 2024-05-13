using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;



public class ThirdPersonMovement : MonoBehaviour
{
    public Animator anim;
    public CharacterController controller;
    public Transform cam;
    public float speed;
    public float cooldownTime = 2f;
    private float nextFireTime = 0f;
    public static int noOfClicks = 0;
    float lastClickTime = 0;
    float maxComboDelay = 1;
    public Transform GroundCheck;
    public LayerMask groundMask;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;
    public int health;
    public int maxHealth = 100;
    public Healthbar healthbar;
    Vector3 velocity;
    public bool grounded;
    private void Start()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
       
        //Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = true;
        //Cursor.visible = false;
    }
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    void Update()
    {
        PlayerWalk();
        PlayerJumping();
        PlayerAttack();
        PlayerIdle();
        
       if (grounded == false)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }

    }

   
    void PlayerWalk()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetangle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetangle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetangle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            anim.SetBool("Walk", true);

        }
        else
        {
            anim.SetBool("Walk", false);

        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 12;


        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 6;
        }
    }
    
    void PlayerJumping()
    {
        grounded = Physics.CheckSphere(GroundCheck.position, groundDistance, groundMask);

        if(grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            
        }


    }

    void PlayerIdle()
    {

    }
    
    void PlayerAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            anim.SetBool("Attack1", false);

        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            anim.SetBool("Attack2", false);

        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))
        {
            anim.SetBool("Attack3", false);
            noOfClicks = 0;
        }

        if (Time.time - lastClickTime > maxComboDelay)
        {
            noOfClicks = 0;
        }
        if (Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClick();
            }


        }
    }
    void OnClick()
    {
        lastClickTime = Time.time;
        noOfClicks++;
        if (noOfClicks == 1)
        {
            anim.SetBool("Attack1", true);

        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            anim.SetBool("Attack1", false);
            anim.SetBool("Attack2", true);
        }


        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2"))
        {
            anim.SetBool("Attack2", false);
            anim.SetBool("Attack3", true);
        }




    }

    void FixedUpdate()
    {
        grounded = false;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Floor")
        {
            grounded = true;
           
        }
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        healthbar.SetHealth(health);
        if (health <= 0)
        {
            Destroy(gameObject);
        }

    }











}
 