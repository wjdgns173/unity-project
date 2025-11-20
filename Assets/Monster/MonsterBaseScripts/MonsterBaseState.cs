using UnityEngine;

public abstract class MonsterBaseState
{
    protected MonsterBase _monster;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected MonsterBaseState(MonsterBase monster)
    {
        _monster = monster;
    }

    public virtual void OnStateEnter() 
    { }

    public virtual void OnSteteUpdate() 
    { }
    
    public virtual void OnStateExite() 
    { }
    
}
