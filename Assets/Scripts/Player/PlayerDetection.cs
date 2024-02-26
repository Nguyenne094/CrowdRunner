using System;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
    private CrowdManager crowdManager;

    #region Events

    public static event Action onDoorHit;
    public static event Action onGetCoins; 

    #endregion

    void Awake()
    {
        crowdManager = GetComponent<CrowdManager>();
        if(crowdManager == null){
            Debug.LogWarning("Can not find CrowdManager in Inspector");
        }
    }

    void Update()
    {
        if(GameManager.Instance.IsGameState()){
            DetectDoors();
        }
    }

    private void DetectDoors()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, crowdManager.GetCrowdRadius());

        foreach(var collider in colliders){
            //*Collide Doors
            if(collider.TryGetComponent(out DoubleDoors doubleDoors)){
                int bonusAmount = doubleDoors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doubleDoors.GetBonusType(transform.position.x);

                crowdManager.ApplyBonus(bonusType, bonusAmount);

                onDoorHit?.Invoke();
                doubleDoors.DisableCol();
            }
            //*Collide Finish Line
            else if(collider.gameObject.CompareTag("Finish")){
                FinishLevel();
            }
            //*Collide Coins
            else if(collider.gameObject.CompareTag("Coin")){
                Destroy(collider.gameObject);
                
                onGetCoins?.Invoke();
                DataManager.Instance.AddCoins(1);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, crowdManager.GetCrowdRadius());
    }

    private void FinishLevel(){
        PlayerPrefs.GetInt("level", PlayerPrefs.GetInt("level") + 1);

        GameManager.Instance.SetGameState(GameManager.GameState.LevelComplete);
        // SceneManager.LoadScene(0);
    }
}