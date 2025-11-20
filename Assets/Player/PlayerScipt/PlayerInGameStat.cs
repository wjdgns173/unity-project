using UnityEngine;

[System.Serializable]
public class PlayerInGameStat
{

    public PlayerData baseStat;

    public float MaxHealth;
    public float curHealth;
    public float Health
    {
        get => curHealth;
        set
        {
            curHealth = Mathf.Clamp(value,  0,  MaxHealth);

            PlayerManager.instance.PlayerHpUi();

            if(curHealth <= 0)
            {
                //die
            }   
        }
    }  

    public float speed;

    public float jumpForce;
    public int   jumpCnt;
    public int   maxJumpCnt;

    public float dashDistance;
    public float dashForce;
    public float dashCoolTime;

    public float atk;
    public float magicAtk;


    public void StartStatSetting(PlayerData playerbaseStat)
    {
        baseStat = playerbaseStat;
        StatSet();
    }

    public void StatSet()
    {
        MaxHealth    = baseStat.MaxHealth;
        curHealth    = MaxHealth;

        speed        = baseStat.Speed;

        jumpCnt = baseStat.JumpCnt;
        jumpForce    = baseStat.JumpForce;
        maxJumpCnt   = baseStat.MaxJumpCnt;

        dashForce = baseStat.DashForce;
        dashDistance = baseStat.DashDistance;
        dashCoolTime = baseStat.DashCoolTime;
    }


    //=====================================

    public void SpeedUp(float bonusSpeed)
    {
        speed += 
            baseStat.Speed * (bonusSpeed * 0.01f);
    }

    public void HpUp(float bonusHp)
    {
        MaxHealth += 
            baseStat.MaxHealth * (bonusHp * 0.01f);
    }

    public void DashCoolTimeDonw(float minusCoolTime)
    {
        dashCoolTime -= 
            baseStat.DashCoolTime * (minusCoolTime * 0.01f);
    }

    public void DashDistanceUp(float bonusDistance)
    {
        dashDistance += 
            baseStat.DashDistance * (bonusDistance * 0.01f);
    }

    public void atkUp(float bonusAtk)
    {
        atk += bonusAtk;
    }

    public void Heal(float HealAmmount)
    {
        curHealth = 
            Mathf.Min(curHealth + HealAmmount, MaxHealth);
    }


}
