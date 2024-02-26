using Lean.Pool;
using Unity.Mathematics;
using UnityEngine;

public class CrowdManager : Singleton<CrowdManager>
{
    [Header("Crowed First Setting")]
    [SerializeField] private GameObject runnerPrefab;
    [SerializeField] private Transform crowdParent;
    
    [Space(5)]
    [SerializeField] private uint startRunnersAmount;
    [SerializeField] private float angle = 137.508f;
    [SerializeField] private float radius = 0.1f;
    public static int _size;
  
    
    void Start()
    {
        if(runnerPrefab == null){
            Debug.LogWarning("Runner Prefab is not assigned");
        }
        
        for (int i = 0; i < startRunnersAmount; i++)
        {
            LeanPool.Spawn(runnerPrefab, transform.position, quaternion.identity, crowdParent);
        }
        
        _size = crowdParent.childCount;
    }

    void Update()
    {
        _size = crowdParent.childCount;

        for(int i = 0; i < _size; i++){
            Transform child = crowdParent.GetChild(i);
            child.localPosition = GetPosition(i);
        }
    }

    void LateUpdate()
    {
        if(!GameManager.Instance.IsGameState()){
            return;
        }

        if(crowdParent.childCount <= 0){
            GameManager.Instance.SetGameState(GameManager.GameState.Gameover);
        }
    }

    private Vector3 GetPosition(int i)
    {
        var x = radius * Mathf.Sqrt(i) * Mathf.Cos(i * angle * Mathf.Deg2Rad);
        var z = radius * Mathf.Sqrt(i) * Mathf.Sin(i * angle * Mathf.Deg2Rad);

        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius(){
        return radius * Mathf.Sqrt(_size);
    }

    public void ApplyBonus(BonusType bonusType, int bonusAmount)
    {
        switch (bonusType){
            case BonusType.Addition:
                AddRunner(bonusAmount);
                break;
            case BonusType.Difference:
                SubRunner(bonusAmount);
                break;
            case BonusType.Product:
                var productAmount = (CrowdManager._size * bonusAmount / 2);
                AddRunner(productAmount);
                break;
            case BonusType.Division:
                var remainingRunners = _size / bonusAmount;
                var divisionAmount = _size - remainingRunners;
                SubRunner(divisionAmount);
                break;
        }
    }

    private void AddRunner(int amount)
    {
        for(int i = 0; i < amount; i++){
            var newRunner = LeanPool.Spawn(runnerPrefab, crowdParent.position, Quaternion.identity, crowdParent).transform;
            newRunner.GetComponent<SkinSelector>().SelectSkin(ShopManager.Instance.GetLastSelectedSkin());
            newRunner.GetComponentInChildren<Animator>().SetBool("Running", true);
        }
    }

    private void SubRunner(int amount)
    {
        if(amount >= _size){
            amount = _size;
        }

        for(int i = 0; i < amount; i++)
        {
            var currentRunner = crowdParent.GetChild(i);
            currentRunner.SetParent(null);
            LeanPool.Despawn(currentRunner.gameObject);
        }
    }
}