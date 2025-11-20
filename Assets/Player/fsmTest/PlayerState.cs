using UnityEngine;

public abstract class PlayerState
{
    protected PlayerCon player;

    public PlayerState(PlayerCon player) { this.player = player; }

    public virtual void OnStateEnter()  { }
    public virtual void OnSteteUpdate() { }
    public virtual void OnStateExite()  { }

    public class Idle : PlayerState
    {
        public Idle(PlayerCon player) : base(player) { }

        public override void OnStateEnter() => player.anime.Play("PlayerIdle");
        
        public override void OnSteteUpdate()
        {
            if (player._input.horizontal() != 0) player.ChangeState(player.moveState);

            if (!player._move.isGrounded())
            {
                player.ChangeState(player.jumpState);
            }
            if(player.isDashing)
            {
                player.ChangeState(player.dashingState);
            }
        }

        public override void OnStateExite()
        {
        }

        
    }

    public class Move : PlayerState
    {
        public Move(PlayerCon player) : base(player) { }

        public override void OnStateEnter() => player.anime.Play("PlayerRun");
        

        public override void OnStateExite()
        {
        }

        public override void OnSteteUpdate()
        {

            if (Mathf.Abs(player._input.horizontal()) <= 0)
            {
                player.ChangeState(player.idleState);
            }

            if (!player._move.isGrounded())
            {
                player.ChangeState(player.jumpState);
            }
            if (player.isDashing)
            {
                player.ChangeState(player.dashingState);
            }

        }
    }

    public class Dashing : PlayerState
    {
        public Dashing(PlayerCon player) : base(player) { }

        public Vector3 dashPosition;

        public override void OnStateEnter()
        {
            
            player.DashSmoke();
        }


        public override void OnStateExite()
        {
            
        }

        public override void OnSteteUpdate()
        {

            if (!player.isDashing)
            {
                if (player._move.isGrounded())
                {
                    if (player._input.horizontal() != 0)
                    {
                        player.ChangeState(player.moveState);
                    }
                    else
                    {
                        player.ChangeState(player.idleState);

                    }
                }
                if (!player._move.isGrounded())
                {
                    player.ChangeState(player.jumpState);
                }
            }
        }
    }


    public class Jump : PlayerState
    {
        public Jump(PlayerCon player) : base(player) { }

        public override void OnStateEnter()
        {
            player.anime.Play("PlayerJumping");
        }

        public override void OnStateExite()
        {
        }

        public override void OnSteteUpdate()
        {

            if (player.isDashing)
            {
                player.ChangeState(player.dashingState);
            }

            if (player._move.isGrounded())
            {
                if (player._input.horizontal() != 0)
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