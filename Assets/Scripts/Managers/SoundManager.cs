using System;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Sounds")]
    [SerializeField] private AudioSource buttonSound;
    [SerializeField] private AudioSource doorHitSound;
    [SerializeField] private AudioSource runnerDieSound;
    [SerializeField] private AudioSource levelCompleteSound;
    [SerializeField] private AudioSource gameoverSound;
    [SerializeField] private AudioSource getCoinsSound;
    [SerializeField] private AudioSource menuSound;
    [SerializeField] private AudioSource gameSound;

    void Start()
    {
        PlayerDetection.onDoorHit += PlayDoorHitSound;
        PlayerDetection.onGetCoins += PlayGetCoinsSound;
        GameManager.onGameStateChanged += OnStateChangedCallback;
        Enemy.onRunnerDie += PlayRunnerDieSound;
    }

    void OnDestroy()
    {
        PlayerDetection.onDoorHit -= PlayDoorHitSound;
        PlayerDetection.onGetCoins -= PlayGetCoinsSound;
        GameManager.onGameStateChanged -= OnStateChangedCallback;
        Enemy.onRunnerDie -= PlayRunnerDieSound;
    }
    
    // * Sounds background in Menu state and Game state
    private void OnStateChangedCallback(GameManager.GameState state)
    {
        if(state == GameManager.GameState.LevelComplete){
            levelCompleteSound?.Play();
        }
        else if(state == GameManager.GameState.Gameover){
            gameoverSound?.Play();
        }
        else if(state == GameManager.GameState.Menu){
            gameSound?.Stop();
            menuSound?.Play();
        }
        else if(state == GameManager.GameState.Game){
            menuSound?.Stop();
            gameSound?.Play();
        }
    }

    #region  METHODS for playing sound
    
    private void PlayRunnerDieSound()
    {
        runnerDieSound?.Stop();
        runnerDieSound?.Play();
    }

    private void PlayDoorHitSound(){
        doorHitSound?.Play();
    }
    
    private void PlayGetCoinsSound()
    {
        getCoinsSound?.Stop();
        getCoinsSound?.Play();
    }

    #endregion

    public void DisableSounds()
    {
        buttonSound.volume = 0;
        doorHitSound.volume = 0;
        runnerDieSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameoverSound.volume = 0;
        getCoinsSound.volume = 0;
        menuSound.volume = 0;
        gameSound.volume = 0;
    }

    public void EnableSounds()
    {
        buttonSound.volume = 1;
        doorHitSound.volume = 1;
        runnerDieSound.volume = 1;
        levelCompleteSound.volume = 1;
        gameoverSound.volume = 1;
        getCoinsSound.volume = 1;
        menuSound.volume = 1;
        gameSound.volume = 1;
    }
}
