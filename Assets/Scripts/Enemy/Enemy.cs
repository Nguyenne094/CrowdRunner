using System;
using Lean.Pool;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State{
        Idle, 
        Running
    }

    [Header("Settings")]
    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;

    private State _state;
    private Transform _targetRunner;

    //* Event
    public static event Action onRunnerDie;

    void Update()
    {
        ManageState();
    }

    private void ManageState()
    {
        switch (_state)
        {
            case State.Idle:
                SearchForTarget();
                break;

            case State.Running:
                RunTowardsTarget();
                break;
        }
    }

    private void SearchForTarget()
    {
        if(GameManager.Instance.gameState == GameManager.GameState.Game)
        {
            Collider[] detectedColliders = Physics.OverlapSphere(transform.position, searchRadius);

            //* Find one target, if has target break loop
            foreach (var collider in detectedColliders)
            {
                if(_targetRunner != null) break;
                else if(collider.TryGetComponent<Runner>(out Runner runner)){
                    if(runner.IsTarget())
                        continue;

                    _targetRunner = runner.transform;
                    runner.SetTarget();

                    StartRunningTowardsTarget();
                }
            }
        }
    }

    private void StartRunningTowardsTarget(){
        _state = State.Running;
    }

    private void RunTowardsTarget()
    {
        GetComponent<Animator>().SetBool("Running", true);

        if(_targetRunner == null){
            return;
        }

        var dirToRunners = (_targetRunner.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dirToRunners, Vector3.up);
        
        transform.position = Vector3.MoveTowards(transform.position, _targetRunner.position, moveSpeed * Time.deltaTime);

        if(Vector3.Distance(transform.position, _targetRunner.position) < .1f){
            onRunnerDie?.Invoke();

            LeanPool.Despawn(_targetRunner.gameObject);
            LeanPool.Despawn(gameObject);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    }
}
