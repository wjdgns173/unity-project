using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class ThiefScript : MonsterBase
{
    public bool canAttack = true;
    public bool isAttackign;
    public bool isCoolTime;
    public Vector2 MovePosition;

    public override void AttackEnter()
    {
        if (canAttack)
        {
            rb.gravityScale = 0f;
            isAttackign = true;
            anime.Play("AttackReady");
        }
        else
        {
            anime.Play("Idle");
        }
    }

    public override void AttackUpdate()
    {
        if(canAttack && !isAttackign && !isCoolTime)
        {
            rb.gravityScale = 0f;
            canAttack = false;
            isAttackign = true;
            anime.Play("AttackReady");
        }
    }

    public override void AttackExit()
    {
        isAttackign = false;
        rb.gravityScale = 3f;
    }

    public override void Attack()
    {
        base.Attack();
        if(!isCoolTime)
        {
            StartCoroutine(attackCoolTIme());
        }
    }

    public override bool StunIf()
    {
        return false;
    }

    public IEnumerator attackCoolTIme()
    {
        isCoolTime = true;
        canAttack = false;
        yield return new WaitForSeconds(1);
        canAttack = true;
        isCoolTime =false;
    }

    public void MoveToPlayer()
    {
        MovePosition = new Vector2
                            (
                                player.transform.position.x + (1 * -player.transform.localScale.x),
                                player.transform.position.y
                            );
        transform.position = MovePosition;
        Flip();
    }
}
