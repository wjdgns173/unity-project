using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;

public abstract class MonsterBase : MonoBehaviour
{
    public GameObject player;

    public Vector2 FindBoxSize;
    public Vector2 AttackRangeBoxSize;
    public Vector2 AttackHitBoxSize;
    public Vector2 ShoutOutBoxSize;

    public Transform FindBox;
    public Transform AttackRangeBox;
    public Transform AttackHitBox;
    public Transform ShoutOutBox;

    public bool  hasFindPlayer;

    public float boxDistance;

    public bool  iamStun;

    public MonsterData monsterData;

    bool isFacingRight;

    public bool     isFlashing;
    public Material flashBang;
    public Material originalMaterial;

    [SerializeField] private float _curHealth;
    public float Health
    {
        get => _curHealth;
        set
        {
            _curHealth = Mathf.Clamp(value, 0, monsterData.maxHealth);
            if (Health <= 0.1f)
            {
                Die();
            }
        }
    }

    public MonsterState        currentState;
    public MonsterState.Stun   stunState;
    public MonsterState.Idle   idleState;
    public MonsterState.Move   moveState;
    public MonsterState.Cruise cruiseState;
    public MonsterState.Attack attackState;

    public Animator       anime;
    public Rigidbody2D    rb;
    public SpriteRenderer sp;

    public LayerMask playerLayer;

    public GameObject DieParticle;
    public Transform partiSpawnPosition;


    void Awake()
    {

        rb    = GetComponent<Rigidbody2D>();
        sp    = GetComponent<SpriteRenderer>();
        anime = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player");


        Health = monsterData.maxHealth;

        originalMaterial = sp.material;

        idleState   = new MonsterState.Idle(this);
        moveState   = new MonsterState.Move(this);
        stunState   = new MonsterState.Stun(this);
        attackState = new MonsterState.Attack(this);
        cruiseState = new MonsterState.Cruise(this);

    }

    public virtual void Start()
    {
        ChangeState(cruiseState);
        if (player == null) 
            player = GameObject.FindWithTag("Player");

    }

    public virtual void Update()
    {
        currentState?.OnSteteUpdate();
        Debug.Log($"현재 State = {currentState}");
    }


    #region  "Base기본 행동"
    public virtual void Move() //움직임
    {
        float newX =
            Mathf.MoveTowards
                (
                    transform.position.x, player.transform.position.x,
                    monsterData.speed * Time.deltaTime
                );

        transform.position = new Vector2(newX, transform.position.y);
    }

    public virtual void Attack() //플레이어 떄렸다고  //애니메이션으로 관리
    {
        RaycastHit2D a =
            Physics2D.BoxCast
                    (
                        AttackHitBox.position, AttackHitBoxSize,
                        0f, 
                        LocalX(),
                        boxDistance, playerLayer
                    );

        bool playerHit = a.collider != null;
        if (playerHit)
        {
            Debug.Log("플레이어 줘팸");
            PlayerManager.instance.Damage(monsterData.attackDamage);
        }
    }

    public virtual bool FindPlayer() //찾았다고
    {
        if (!hasFindPlayer)
        {
            RaycastHit2D hit =
                Physics2D.BoxCast
                        (
                            FindBox.position, FindBoxSize,
                            0f, 
                            LocalX(),
                            boxDistance, playerLayer
                        );

            if (hit.collider != null)
            {
                hasFindPlayer = true;
            }
        }

        return hasFindPlayer;
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
    public virtual bool CanAttack() //공격 "사거리에" 안에 플레이어가 있다고
    {

        RaycastHit2D a =
            Physics2D.BoxCast
                    (
                        AttackRangeBox.position, AttackRangeBoxSize,
                        0f, 
                        LocalX(),
                        boxDistance, playerLayer
                    );

        bool canAttack = a.collider != null;

        return canAttack && FindPlayer();
    }

    public virtual bool PlayerInAttackHitBox() //플레이어가 공격범위 내에 있다고 
    {

        RaycastHit2D a =
            Physics2D.BoxCast
                    (
                        AttackHitBox.position, AttackHitBoxSize,
                        0f, 
                        LocalX(),
                        boxDistance, playerLayer
                    );

        bool canAttack = a.collider != null;

        return canAttack;
    }
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public virtual void Damage(float damageAmount) //플레이어가 나 떄렸다고
    {
        hasFindPlayer = true;
        Health -= damageAmount - (damageAmount * (monsterData.defense * 0.01f));
        if(Health > 0.1f)
        {
            if (!isFlashing) StartCoroutine(apayo());
            if (StunIf())    ChangeState(stunState);
        }
    }
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public IEnumerator apayo() //아프니까 빤짝인다고
    {

        isFlashing  =  true;
        sp.material = flashBang;

        yield return new WaitForSeconds(0.1f);

        sp.material = originalMaterial;
        isFlashing  = false;
    }
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public virtual void Die() //죽는다고
    {
        Instantiate(DieParticle,partiSpawnPosition.position,transform.rotation);
        gameObject.SetActive(false);
    }

    //=======================================================================================//    

    public virtual void Flip() //뒤집는다고
    {
        float direction = player.transform.position.x - transform.position.x;

        Vector2 localScale = transform.localScale;

        localScale.x = Mathf.Abs(localScale.x) * (direction > 0 ? 1 : -1);
        transform.localScale = localScale;

    }

    public virtual bool StunIf()
    {
        return true;
    }

    public virtual void Stun()
    {
        StartCoroutine(StunTime());
    }
    
    public virtual IEnumerator StunTime()
    {
        yield return new WaitForSeconds(0.05f);

        if  (CanAttack()) ChangeState(attackState);
        else ChangeState(cruiseState);

        iamStun = false;

    }

    #endregion

    public Vector2 LocalX() //내 LocalScale.x라고
    {
        Vector2 localX = new Vector2(transform.localScale.x, 0f);
        return localX;
    }
    #region "Gizmos"
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(FindBox.position + transform.right.normalized * boxDistance, FindBoxSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(AttackRangeBox.position + transform.right.normalized * boxDistance, AttackRangeBoxSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(AttackHitBox.position + transform.right.normalized * boxDistance, AttackHitBoxSize);
    }
    #endregion

    #region "State관리
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */
    public virtual void AttackEnter()
    {
        anime.Play("Attack");
    }

    public virtual void AttackUpdate()
    {
        
    }

    public virtual void AttackExit()
    {

    }
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public virtual void IdleEnter()
    {
        
    }

    public virtual void IdleUpdate()
    {
    }

    public virtual void IdleExit()
    {

    }
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public virtual void CruiseEnter()
    {
    }

    public virtual void CruiseUpdate()
    {
        if (FindPlayer()) ChangeState(moveState);
    }

    public virtual void CruiseExit()
    {

    }
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public virtual void MoveEnter()
    {
        if (iamStun) ChangeState(stunState);
        anime.Play("Move");
    }

    public virtual void MoveUpdate()
    {
        Flip();
        if (CanAttack()) ChangeState(attackState);
        else
        {
            Move();
        }
        
        
    }

    public virtual void MoveExit()
    {

    }
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public virtual void StunEnter()
    {
        if(iamStun) return;
        iamStun = true;
        Stun();
    }

    public virtual void StunUpdate()
    {
        Debug.LogWarning("스턴중" + iamStun);
    }

    public virtual void StunExit()
    {
        
    }
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    public void ChangeState(MonsterState newState)   //currentState? => if(currentState != null)
    {
        if (currentState == newState) return;
        currentState?.OnStateExite();
        currentState = newState;
        currentState?.OnStateEnter();
    }
    #endregion


}
