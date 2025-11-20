using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public PlayerCon player;

    //BP = BounusPerCent

    float BP_maxHp;
    int   BP_speed;
    int   BP_jumpForce;
    int   BP_maxJumpCnt;
    int   BP_dashDistance;
    int   BP_dashForce;
    int   BP_dashCoolTime;


    //-------
    [SerializeField] TextMeshProUGUI HpUi;


    public static PlayerManager instance;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCon>();
        instance = this;

        player.playerStat.StartStatSetting(player.playerData);
    }

    void Start()
    {
    }

    private void Update()
    {
    }

    public void Damage(float damageAmount)
    {
        if (PlayerCon.instance.isDashing) return;
        
        player.playerStat.Health -= damageAmount;
        player.StartCoroutine(player.apayo());
    }

    public void PlayerHpUi() //임시
    {
        HpUi.text =
            (
                player.playerStat.Health
                + " / " +
                player.playerStat.MaxHealth

            );
    }



}
