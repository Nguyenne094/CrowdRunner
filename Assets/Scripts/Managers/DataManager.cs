using TMPro;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    [SerializeField] private TextMeshProUGUI[] coins_TMP;
    private int coins;
    
    void Start()
    {
        //Update data to variable
        coins = PlayerPrefs.GetInt("coins");
    }

    void Update()
    {
        UpdateCoinsText();
    }

    private void UpdateCoinsText()
    {
        foreach(var coin in coins_TMP){
            coin.text = PlayerPrefs.GetInt("coins").ToString();
        }
    }

    public void AddCoins(int amount){
        coins += amount;

        UpdateCoinsText();

        PlayerPrefs.SetInt("coins", coins);
    }

    public int GetCoins()
    {
        return coins;
    }

    public void UseCoins(int amount)
    {
        coins -= amount;

        UpdateCoinsText();

        PlayerPrefs.SetInt("coins", coins);
    }

    // public void DeleteAllData(){
    //     PlayerPrefs.DeleteAll();
    //     ShopManager.Instance.ConfigureSkinButtons();
    //     UpdateCoinsText();
    // }

    [ContextMenu("Reset Data"), Tooltip("Reset coins and shop")]
    public void ResetData(){
        PlayerPrefs.DeleteAll();

        //* Update view
        UpdateCoinsText();
        ShopManager.Instance.ConfigureSkinButtons();
    }
}
