using System;
using Lean.Pool;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyGroupParent;

    [Header("Settings")]
    [SerializeField] private uint amount;
    [SerializeField] private float angle = 137.508f;
    [SerializeField] private float radius = 0.1f;

    void Start()
    {
        GenerateEnemies();
    }

    void Update()
    {
        if(enemyGroupParent.childCount == 0) Destroy(gameObject);
    }

    private void GenerateEnemies()
    {
        for(int i = 0; i < amount; i++){
            Vector3 enemyLocalPosition = GetPosition(i);
            enemyLocalPosition = transform.TransformPoint(enemyLocalPosition);

            LeanPool.Spawn(enemyPrefab, enemyLocalPosition, Quaternion.identity, enemyGroupParent);
        }
    }

    private Vector3 GetPosition(int i)
    {
        var x = radius * Mathf.Sqrt(i) * Mathf.Cos(i * angle * Mathf.Deg2Rad);
        var z = radius * Mathf.Sqrt(i) * Mathf.Sin(i * angle * Mathf.Deg2Rad);

        return new Vector3(x, 0, z);
    }
}
