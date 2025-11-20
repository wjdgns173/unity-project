using UnityEngine;

public abstract class PlayerBaseState
{

    protected PlayerCon _playerCon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected PlayerBaseState(PlayerCon player)
    {
        _playerCon = player;
    }

    public virtual void OnStateEnter()
    { }

    public virtual void OnSteteUpdate()
    { }
    
    public virtual void OnStateExite()
    {}
}
