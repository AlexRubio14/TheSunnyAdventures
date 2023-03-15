using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rb;
    private playerController player;
    private float baseGravity;
    private float timeWaited;


    [Header("Dash")]
    [SerializeField]
    private float dashingTime = 0.2f;
    [SerializeField]
    private float dashForce = 20f;
    [SerializeField]
    private float timeCanDash = 1f; // <- Cooldown
    private bool isDashing;
    private bool canDash = true;
    //public bool IsDashing => isDashing;

    private void Awake()
    {
        rb = GetComponent <Rigidbody2D>();
        player = GetComponent<playerController>();
        baseGravity = rb.gravityScale;
        
    }

    public void WaitCD()
    {
        if (!canDash)
        {
            timeWaited += Time.deltaTime;
            if(timeWaited >= dashingTime)
            {
                isDashing = false;
                rb.gravityScale = baseGravity;
                if (timeWaited >= timeCanDash && !player.isJumping)
                {
                    canDash = true;
                    timeWaited = 0;
                }
            }
        }
    }
    public void Dash()
    {
            if (player.direction != 0 && canDash == true)
            {
                isDashing = true;
                canDash = false;
                rb.gravityScale = 0f;
                rb.velocity = new Vector2(player.direction * dashForce, 0f);
            }
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }

    public void SetIsDashing(bool isDashing)
    {
        this.isDashing = isDashing;
    }
}
