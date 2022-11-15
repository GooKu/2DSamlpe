using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//testa
public class PlayerController : MonoBehaviour
{
//abc
    public event Action ArrivedGoalEvent;

    [SerializeField]
    private float speed = 10;

    private SpriteRenderer render;
    private Animator anim;
    private Rigidbody2D rig;

    private bool isJumping = false;
    private float jumpingTime = 0;

    private const string movingKey = "Moving";
    private const string jumpingKey = "Jumping";

    void Start()
    {
        render = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position - new Vector3(speed, 0, 0) * Time.deltaTime;
            render.flipX = true;
            anim.SetBool(movingKey, true);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool(movingKey, false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + new Vector3(speed, 0, 0) * Time.deltaTime;
            render.flipX = false;
            anim.SetBool(movingKey, true);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool(movingKey, false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rig.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
            isJumping = true;
            jumpingTime = 0;
            anim.SetBool(jumpingKey, true);
        }
    }

    private void FixedUpdate()
    {
        if (isJumping)
        {
            jumpingTime += Time.fixedDeltaTime;

            if (jumpingTime > .1f && rig.Cast(Vector2.down, new RaycastHit2D[10], .1f) > 0)
            {
                isJumping = false;
                anim.SetBool(jumpingKey, false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"OnTriggerEnter2D:{collision.name}");

        if(collision.tag == "goal")
        {
            ArrivedGoalEvent?.Invoke();
        }
    }

    public void OnButtonClick(int i)
    {
        Debug.Log("OnButtonClick:" + i);
    }
}
