using System.Collections.Generic;
using System;

public class PlayerFSM
{


    /*private PlayerState currentState;
    private Dictionary<PlayerState, List<(Func<bool> condition, PlayerState target)>> transitions;

    void update()
    {
        currentState.OnSteteUpdate();

        //조건들을 체크하면서 true가 있는지 확인
        foreach (var (condition, target) in transitions[currentState]) 
        {
            if (condition()) //만약 true가 있다면 change
            {
                ChangeState(target);
                break;
            }
        }
    }
    
    public void ChangeState(PlayerState newState)
    {
        currentState.OnStateExite();
        currentState = newState;
        currentState.OnStateEnter();
    }

    public void AddTransition(PlayerState from, PlayerState to, Func<bool> condition)//에서 //으로 //조건 // ex) 1에서 2로 넘어갈려면 a = true여야 한다  
    {
        if (!transitions.ContainsKey(from)) //만약 딕셔너리에 from(State)상태가 없다면               //ContainsKey = 있는지 없는지 확인
            transitions[from] = new List<(Func<bool>, PlayerState)>(); // from에서 to로 넘어갈 조건 추가 

        transitions[from].Add((condition, to));

        
        //새로운 리스트를 만들어 등록
        //from = 전환 시작 상태 (현재 상태)
    }*/
}
