using System;
using UnityEngine;

public class Player : Singleton<Player>
{
    #region Variable SerializeField
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _slideSpeed = 5f;
    [SerializeField] private bool canMove = false;
    #endregion

    #region Variable private
    private Vector3 touchedScreenPosition;
    #endregion

    #region Reference
    CrowdManager crowdManager;
    PlayerAnimator playerAnimator;
    #endregion

    void Awake()
    {
        crowdManager = GetComponent<CrowdManager>();
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Start()
    {
        StopMoving();
        playerAnimator.Idle();

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    private void Update()
    {
        if(canMove){
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
    }

    void LateUpdate()
    {
        if(canMove) ControlMove();
    }

    void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Game){
            StartMoving();
            playerAnimator.Run();
        }
        else if(state == GameManager.GameState.Gameover){
            StopMoving();
        }
        else if(state == GameManager.GameState.LevelComplete){
            playerAnimator.Idle();
            StopMoving();
        }
    }

    private void ControlMove(){
        if(Input.GetMouseButtonDown(0)){
            touchedScreenPosition = Input.mousePosition;
        }
        else if(Input.GetMouseButton(0)){
            float widthScreenDifference = Input.mousePosition.x - touchedScreenPosition.x;

            float displacement = (widthScreenDifference > 0) ? 1 : -1;
            displacement *= _slideSpeed * Time.deltaTime;

            //*desired position
            Vector3 desiredPosition = transform.position;
            desiredPosition.x = transform.position.x + displacement;

            //* Limit position
            desiredPosition.x = Mathf.Clamp(desiredPosition.x, - Chunk.chunkWidth / 2 + crowdManager.GetCrowdRadius(), Chunk.chunkWidth / 2 - crowdManager.GetCrowdRadius());

            transform.position = desiredPosition;
        }
    }

    public void StartMoving(){
        canMove = true;
    }

    public void StopMoving(){
        canMove = false;
    }
}