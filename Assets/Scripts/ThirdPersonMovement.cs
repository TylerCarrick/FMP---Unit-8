using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private void Start()
    {
        anim = GetComponent<Animator>();

    }
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    void Update()
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
            anim.SetBool("Run", false);
        }


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 12;
            anim.SetBool("Run", true);
            anim.SetBool("Walk", false);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 3;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetBool("Attack 1", false);
          
        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetBool("Attack 2", false);

        }
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            anim.SetBool("Attack 3", false);
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
            anim.SetBool("Attack", true);
            
        }
        noOfClicks = Mathf.Clamp(noOfClicks, 0, 3);

        if (noOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Sword Swing 1"))
        {
            anim.SetBool("Attack 1", false);
            anim.SetBool("Attack 2", true);
        }


        if (noOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Sword Swing 2"))
        {
            anim.SetBool("Attack 2", false);
            anim.SetBool("Attack 3", true);
        }




    }














}
 