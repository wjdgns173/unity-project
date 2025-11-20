using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName ="Player/Data")]
public class PlayerData : ScriptableObject
{


    public float MaxHealth;
    public float Speed;
    public float JumpForce;
    public int   JumpCnt;
    public int   MaxJumpCnt;
    public float DashDistance;
    public float DashForce;
    public float DashCoolTime;
}





