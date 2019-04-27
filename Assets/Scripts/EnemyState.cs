using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public enum State
    {
        IDLE,
        ALERT,
        ATTACK
    };

    private State currentState = State.IDLE;

    public void SetState(State newState)
    {
        currentState = newState;
    }

    public State GetState()
    {
        return currentState;
    }
}