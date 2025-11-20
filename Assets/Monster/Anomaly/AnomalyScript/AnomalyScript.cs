using System.Collections;
using UnityEngine;

public class AnomalyScript : MonsterBase
{

    [SerializeField] private float dashForce;
    public bool isDashing;
    public bool isCoolTime;
    public bool canDash = true;

    public bool hitPlayer;

    public override void Start()
    {
        base.Start();
        dashForce = Random.Range(19, 23);
        AttackRangeBoxSize.x = dashForce * 0.5f;
    }

    public void DashStart()
    {
        isDashing = true;
        StartCoroutine(Dashing());
    }

    public override void AttackEnter()
    {
        base.AttackEnter();
        hitPlayer = false;
    }

    public override void AttackUpdate()
    {
        if(isDashing)
        {
            if(PlayerInAttackHitBox())
            {
                if(!hitPlayer)
                {
                    Attack();
                    hitPlayer = true;
                }
            }
        }        
    }

    public override void AttackExit()
    {
        
    }

    public IEnumerator Dashing()
    {
        isCoolTime = false;
        isDashing = true;
        rb.linearVelocityX = dashForce * LocalX().x;

        yield return new WaitForSeconds(0.5f);

        isDashing = false;
        isCoolTime = true;
        rb.linearVelocityX = 0f;
        anime.SetTrigger("StopDash");

        yield return new WaitForSeconds(1f);
        ChangeState(moveState);

    }

    public override bool StunIf()
    {
        return !isDashing && !isCoolTime;
    }


    /*public void DashStart()
    {
        Flip();
        startDashPosition = transform.position;
        isDashing = true;
    }

    public override void AttackUpdate()
    {
        if (canDash)
        {
            if (isDashing)
            {
                rb.linearVelocityX = dashForce * LocalX().x;
                Attack();
                if (Vector2.Distance(startDashPosition, transform.position) >= dashDistance || iamStun)
                {
                    DashStop();
                }
            }
        }

        else
        {
            anime.Play("Idle");
        }
        
    }

    IEnumerator DashCoolTime()
    {
        canDash = false;
        yield return new WaitForSeconds(1);
        if (currentState == attackState) anime.Play("Attack");
        canDash = true;
    }

    public override void AttackExit()
    {
        rb.linearVelocityX = 0;
        isDashing = false;
        anime.Play("Move");
    }

    public override void StunEnter()
    {
        base.StunEnter();
        rb.linearVelocityX = 0;
        isDashing = false;
        StartCoroutine(DashCoolTime());
        anime.Play("Idle");
    }

    void DashStop()
    {
        rb.linearVelocityX = 0;
        isDashing = false;
        StartCoroutine(DashCoolTime());
        anime.SetTrigger("StopDash");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isDashing = false;
        canDash = false;
        ChangeState(moveState);
    }*/



}
