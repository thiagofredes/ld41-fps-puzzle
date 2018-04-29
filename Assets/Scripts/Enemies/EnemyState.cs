using UnityEngine;

public class EnemyState {

    protected EnemyController _enemyRef;

    public virtual void OnEnter(){}
	
    public virtual void Update(){}

    public virtual void OnExit(){}

    public virtual void Stop(){}
}
