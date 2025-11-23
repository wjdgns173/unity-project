using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerCon : MonoBehaviour
{

    public PlayerMove  _move;
    public PlayerInput _input;

    [Space(10)]

    public Rigidbody2D rb;
    public Animator    anime;
    public SpriteRenderer sp;
    
    public GameObject GunPosition;

    [Space(10)]

    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheck;

    [Space(10)]

    [SerializeField] Transform smokePosition;

    [Space(10)]

    public PlayerData       playerData;
    public PlayerInGameStat playerStat;

    [Space(10)]

    public bool isDashing;
    public bool canDash = true;
    public bool dashCooltiming;
    public Vector3 dashPosition;

    [Space(10)]

    private bool isFacingRight;
    private bool isTestHoriSave; //호리존탈 저장
    public  float  horiSave = 1;

    [Space(10)]

    public GameObject groundDashSmoke;
    public GameObject groundSmoke;
    public GameObject airDashEffect;
    public GameObject airJumpEffect;

    [Space(10)]

    public PlayerState         currentState;
    public PlayerState.Idle    idleState;
    public PlayerState.Move    moveState;
    public PlayerState.Jump    jumpState;
    public PlayerState.Dashing dashingState;

    [Space(10)]

    public Material flashBang;
    public Material original;
    private bool    isFlashing;

    [Space(10)]


    public float dashMyDistance = 0;

    public static PlayerCon instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        instance = this;

        _move   = GetComponent<PlayerMove>();
        _input  = GetComponent<PlayerInput>();

        rb = GetComponent<Rigidbody2D>();

        _move.groundCheck = groundCheck;
        _move.groundLayer = groundLayer;

        idleState    = new PlayerState.Idle(this);
        moveState    = new PlayerState.Move(this);
        jumpState    = new PlayerState.Jump(this);
        dashingState = new PlayerState.Dashing(this);
    }

    void Start()
    {
        SaveHorizontal();
        ChangeState(idleState);
    }

    // Update is called once per frame
    void Update()
    {

        currentState?.OnSteteUpdate();
        Debug.Log("대슁" + isDashing);

        if (isDashing)
        {
            float curDashDistance = Vector3.Distance(dashPosition, transform.position);
            rb.linearVelocity    = new Vector2(playerStat.dashForce * horiSave, -1f);

            if (curDashDistance >= playerStat.dashDistance)
            {
                StartCoroutine(DashCoolTime());
                isDashing = false;
            }

            else return;
        }

        anime.SetFloat("yVelocity", rb.linearVelocityY);
        SaveHorizontal();
        Flip();
        OnMove();

        if (_move.isGrounded())
        {
            playerStat.jumpCnt = playerStat.maxJumpCnt;
        }

    }

    //==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//==//
    #region 관리
    private void OnMove()
    {
        _move.Move(_input.horizontal(), playerStat.speed);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (playerStat.jumpCnt > 0 && !isDashing)
            {
                _move.Jump(playerStat.jumpForce);
                playerStat.jumpCnt--;

                if (_move.isGrounded())
                {
                    Instantiate
                        (
                            groundSmoke,
                            smokePosition.position,
                            transform.rotation
                        );
                }

                else
                {
                    Instantiate
                        (
                            airJumpEffect,
                            smokePosition.position,
                            airJumpEffect.transform.rotation
                        );
                }

            }
        }
    }

    void Flip()
    {
        if (isFacingRight && (WeaponCon.instance.mousePos.x < transform.position.x)
        || !isFacingRight && (WeaponCon.instance.mousePos.x > transform.position.x))
        //========================================================================//
        {
            isFacingRight      = !isFacingRight;
            Vector3 localScale = transform.localScale;

            localScale.x *= -1f;

            transform.localScale = localScale;
        }
    }

    void SaveHorizontal()
    {
        if ((isTestHoriSave && _input.horizontal() < 0)
        || (!isTestHoriSave && _input.horizontal() > 0))
        {
            isTestHoriSave = !isTestHoriSave;
            horiSave = _input.horizontal();
        }
    }

    public void ChangeState(PlayerState newState)   //currentState? => if(currentState != null)
    {
        if (currentState == newState) return;
        currentState?.OnStateExite();
        currentState = newState;
        currentState?.OnStateEnter();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            if (canDash)
            {
                isDashing = true;
                canDash = false;
                dashPosition = transform.position;

            }
        }

    }

    public void DashSmoke()
    {
        if (_move.isGrounded())
        {
            GameObject groundSmoke = Instantiate
                                    (
                                        groundDashSmoke,
                                        smokePosition.position,
                                        transform.rotation
                                    );

            groundSmoke.gameObject.transform.localScale 
            = new Vector3
              (
                  horiSave,                             //X
                  groundDashSmoke.transform.localScale.x,//Y
                  groundDashSmoke.transform.localScale.z //Z
              );
        }

        else
        {
            GameObject airSmoke = Instantiate
                                  (
                                    airDashEffect,
                                    smokePosition.position,
                                    transform.rotation
                                  );

            airSmoke.gameObject.transform.localScale 
            = new Vector3
              (
                  horiSave,                          //X
                  airSmoke.transform.localScale.x,    //Y
                  airSmoke.transform.localScale.z     //Z
              );
        }
    }

    public IEnumerator DashCoolTime()
    {
        dashCooltiming = true;
        
        yield return new WaitForSeconds(playerStat.dashCoolTime);

        dashCooltiming = false;
        canDash        = true;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (isDashing)
            {
                if (!dashCooltiming)
                {
                    StartCoroutine(DashCoolTime());
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (isDashing)
            {
                if (!dashCooltiming)
                {
                    StartCoroutine(DashCoolTime());
                }
            }
        }
    }


    public IEnumerator apayo()
    {
        if (isFlashing) yield break;
        isFlashing  = true;
        sp.material = flashBang;

        yield return new WaitForSeconds(0.1f);

        sp.material = original;
        isFlashing  = false;
    }


    #endregion
}
