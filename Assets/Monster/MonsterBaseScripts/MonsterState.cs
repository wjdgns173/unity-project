using UnityEngine;

public abstract class MonsterState
{

    protected MonsterBase monster;

    public MonsterState(MonsterBase monster) { this.monster = monster; }

    public virtual void OnStateEnter()  { }
    public virtual void OnSteteUpdate() { }
    public virtual void OnStateExite()  { }

    public class Idle : MonsterState
    {
        public Idle(MonsterBase monster) : base(monster) { }

        public override void OnStateEnter()
        {
            monster.IdleEnter();
        }
        
        public override void OnSteteUpdate()
        {
            monster.IdleUpdate();
        }

        public override void OnStateExite()
        {
            monster.IdleExit();   
        }

        
    }

    public class Move : MonsterState
    {
        public Move(MonsterBase monster) : base(monster) { }

        public override void OnStateEnter()
        {
            monster.MoveEnter();
        }
        

        public override void OnStateExite()
        {
            monster.MoveExit();
        }

        public override void OnSteteUpdate()
        {
            monster.MoveUpdate();
        }
    }

    public class Attack : MonsterState
    {
        public Attack(MonsterBase monster) : base(monster) { }

        public override void OnStateEnter()
        {
            monster.AttackEnter();
        }


        public override void OnStateExite()
        {
            monster.AttackExit();
        }

        public override void OnSteteUpdate()
        {
            monster.AttackUpdate();
        }
    }


    public class Cruise : MonsterState //순찰
    {
        public Cruise(MonsterBase monster) : base(monster) { }

        public override void OnStateEnter()
        {
            monster.CruiseEnter();
        }

        public override void OnStateExite()
        {
            monster.CruiseExit();
        }

        public override void OnSteteUpdate()
        {
            monster.CruiseUpdate();
        }
    }

    public class Stun : MonsterState
    {
        public Stun(MonsterBase monster) : base(monster) { }

        public override void OnStateEnter()
        {
            monster.StunEnter();
        }

        public override void OnStateExite()
        {
            monster.StunExit();
        }

        public override void OnSteteUpdate()
        {
            monster.StunUpdate();
        }
    }

}
