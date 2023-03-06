using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INavigable
{

    public void Run();
    public Vector3 FindNewPatrolPosition();
    public void MoveToPosition(Vector3 pos1, Vector3 pos2);
    public void SetState(state myState);
    public void SetPlayerRef(GameObject player);
    public state GetState();
}

public enum state
{
    wandering,
    chasing,
    attacking,
    fleeing,
    recharging,
    supercharged,
    stunned,
}
