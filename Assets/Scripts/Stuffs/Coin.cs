using UnityEngine;

public class Coin : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _speed = 300f;

    void Update()
    {
        if(GameManager.Instance.gameState != GameManager.GameState.Game)
            return;

        Rotate();
    }

    private void Rotate()
    {
        transform.eulerAngles += Vector3.up * _speed * Time.deltaTime;
        if(transform.eulerAngles.y > 360){
            transform.eulerAngles = Vector3.zero;
        }
    }
}
