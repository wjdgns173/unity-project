using UnityEngine;

public abstract class PlayerState
{
    protected PlayerCon player;

    public PlayerState(PlayerCon player) { this.player = player; }

    public virtual void OnStateEnter()  { }
    public virtual void OnSteteUpdate() { }
    public virtual void OnStateExite()  { }

    public bool IamAir      => !player._move.isGrounded();
    public bool IamMove     => player._input.horizontal() != 0;
    public bool IamStay     => player._input.horizontal() == 0;
    public bool IamDashing  => player.isDashing;
    public bool IamGrounded => player._move.isGrounded();

    public class Idle : PlayerState
    {
        public Idle(PlayerCon player) : base(player) { }

        public override void OnStateEnter() => player.anime.Play("PlayerIdle");
        
        public override void OnSteteUpdate()
        {
            if (IamMove) player.ChangeState(player.moveState);

            if (IamAir)
            {
                player.ChangeState(player.jumpState);
            }
            if(IamDashing)
            {
                player.ChangeState(player.dashingState);
            }
        }

        public override void OnStateExite() {}

        
    }

    public class Move : PlayerState
    {
        public Move(PlayerCon player) : base(player) { }

        public override void OnStateEnter() => player.anime.Play("PlayerRun");
        

        public override void OnStateExite() {}

        public override void OnSteteUpdate()
        {

            if (IamStay)
            {
                player.ChangeState(player.idleState);
            }

            if (IamAir)
            {
                player.ChangeState(player.jumpState);
            }
            if (IamDashing)
            {
                player.ChangeState(player.dashingState);
            }

        }
    }

    public class Dashing : PlayerState
    {
        public Dashing(PlayerCon player) : base(player) { }

        public Vector3 dashPosition;

        public override void OnStateEnter() => player.DashSmoke();

        public override void OnStateExite() {}

        public override void OnSteteUpdate()
        {

            if (!IamDashing)
            {
                if (IamGrounded)
                {
                    if (IamMove)
                    {
                        player.ChangeState(player.moveState);
                    }
                    else
                    {
                        player.ChangeState(player.idleState);

                    }
                }
                if (IamAir)
                {
                    player.ChangeState(player.jumpState);
                }
            }
        }
    }


    public class Jump : PlayerState
    {
        public Jump(PlayerCon player) : base(player) { }

        public override void OnStateEnter() => player.anime.Play("PlayerJumping");

        public override void OnStateExite(){}

        public override void OnSteteUpdate()
        {

            if (IamDashing)
            {
                player.ChangeState(player.dashingState);
            }

            if (IamGrounded)
            {
                if (IamMove)
                {
                    player.ChangeState(player.moveState);
                }
                else
                {
                    player.ChangeState(player.idleState);
                    
                }
            }
        }
    }

    public class AttackState : PlayerBaseState
    {
        public AttackState(PlayerCon player) : base(player) { }

        public override void OnStateEnter()
        {
        
        }

        public override void OnStateExite()
        {
        }

        public override void OnSteteUpdate()
        {
        }
    }

}