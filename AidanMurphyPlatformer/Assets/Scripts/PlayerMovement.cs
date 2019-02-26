﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rgbd;

    float defaultGravity;

    public float moveSpeed;
    public float jumpSpeed;

    LayerMask layer;

    public float gravityIncreaseAmount;

    public float fallMultiplyer = 15f;
    public float lowJumpMultiplyer = 26f;

    public GameObject feet;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = gameObject.GetComponent<Rigidbody2D>();
        defaultGravity = rgbd.gravityScale;
        layer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Jump();
        BetterJump();
    }

    void Movement()
    {
        float moveX = Input.GetAxis("Horizontal");
        rgbd.velocity = new Vector2(moveX * moveSpeed, rgbd.velocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (Physics2D.OverlapBox(feet.transform.position, new Vector2(1f, 0.2f), 0, layer))
            {
                rgbd.velocity = Vector2.up * jumpSpeed;
            }
        }
    }

    void BetterJump()
    {
        if (rgbd.velocity.y < 0) rgbd.gravityScale = fallMultiplyer;
        else if (rgbd.velocity.y > 0 && !Input.GetButton("Jump")) rgbd.gravityScale = lowJumpMultiplyer;
        else rgbd.gravityScale = 1f;
    }

}
