using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovementDani player;
    private float baseGravity;

    [Header("Dash")]
    [SerializeField]
    private float dashingTime = 0.2f;
    [SerializeField]
    private float dashForce = 20f;
    [SerializeField]
    private float timeCanDash = 1f; // <- Cooldown
    private bool isDashing;
    private bool canDash = true;
    public bool IsDashing => isDashing;

    private void Awake()
    {
        rb = GetComponent <Rigidbody2D>();
        player = GetComponent<PlayerMovementDani>();
        baseGravity = rb.gravityScale;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        if (player.Direction != 0 && canDash == true)
        {
            isDashing = true;
            canDash = false;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(player.Direction * dashForce, 0f);
            yield return new WaitForSeconds(dashingTime);
            isDashing = false;
            rb.gravityScale = baseGravity;
            yield return new WaitForSeconds(timeCanDash);
            canDash = true;
        }
    }
}
