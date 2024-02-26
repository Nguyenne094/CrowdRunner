using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

/// <summary>
/// Manage some lists of Button, skin Sprite, skin price Text
/// </summary>
public class ShopManager : Singleton<ShopManager>
{
    [Header("Buttons")]
    [SerializeField] private SkinButton[] skinButtons;
    [SerializeField] private Button purchaseButton;
    
    [Header("Skins")] 
    [SerializeField] private SpriteAtlas skinAtlas;

    [Header("Prices")]
    [SerializeField, Tooltip("One price for all skins")] private int skinPrice;
    [SerializeField] private TextMeshProUGUI price_TMP;

    public static event Action<int> onSkinSelected;
    
    IEnumerator Start()
    {
        RewardedAdButton.onRewardedAdRewarded += RewardPlayer;
        
        UnlockSkin(0);
        price_TMP.text = skinPrice.ToString();

        ConfigureSkinButtons();
        UpdatePurchaseButton();

        yield return null;
        SelectSkin(GetLastSelectedSkin());
    }

    private void OnDestroy()
    {
        RewardedAdButton.onRewardedAdRewarded -= RewardPlayer;
    }

    private void RewardPlayer()
    {
        DataManager.Instance.AddCoins(150);
        UpdatePurchaseButton();
    }

    /// <summary>
    /// Load button skin data and add click button event for each skin button
    /// </summary>
    public void ConfigureSkinButtons()
    {
        Debug.Log(skinAtlas.spriteCount);
        for(int i = 0; i < skinButtons.Length; i++){
            bool unlocked = PlayerPrefs.GetInt("skinButton" + i) == 1;

            skinButtons[i].ConfigureSkin(skinAtlas.GetSprite("Skin " + i), unlocked);

            int skinIndex = i;

            skinButtons[i].GetButton().onClick.AddListener(() => SelectSkin(skinIndex));
        }
    }

    public void UnlockSkin(int skinIndex){
        PlayerPrefs.SetInt("skinButton" + skinIndex, 1);
        skinButtons[skinIndex].Unlock();
    }

    private void UnlockSkin(SkinButton skinButton){
        var skinIndex = skinButton.transform.GetSiblingIndex();
        UnlockSkin(skinIndex);
    }

    /// <summary>
    /// Enable skin frame that is selected and disable the rest
    /// </summary>
    public void SelectSkin(int skinIndex){
        for (int i = 0; i < skinButtons.Length; i++)
        {
            if(i == skinIndex){
                skinButtons[i].Select();
            }
            else{
                skinButtons[i].Deselect();
            }
        }

        onSkinSelected?.Invoke(skinIndex);
        SaveLastSelectedSkin(skinIndex);
    }

    public void PurchaseRandomSkin(){
        //* Add locked skins into a list
        List<SkinButton> skinButtonsList = new List<SkinButton>();
        for (int i = 0; i < skinButtons.Length; i++)
        {
            if(!skinButtons[i].IsUnlocked())
                skinButtonsList.Add(skinButtons[i]);
        }
        
        if(skinButtonsList.Count <= 0) return;

        //* Random locked skin to unlock
        SkinButton randomSkin = skinButtonsList[UnityEngine.Random.Range(0, skinButtonsList.Count)];

        //* unlock and enable this random skin
        UnlockSkin(randomSkin);
        SelectSkin(randomSkin.transform.GetSiblingIndex());

        //* Update datas
        DataManager.Instance.UseCoins(skinPrice);
        UpdatePurchaseButton();
    }

    /// <summary>
    /// If coins less than skin price, disable purchase button, if not enable it
    /// </summary>
    public void UpdatePurchaseButton(){
        if(DataManager.Instance.GetCoins() < skinPrice){
            purchaseButton.interactable = false;
        }
        else{
            purchaseButton.interactable = true;
        }
    }

    public int GetLastSelectedSkin()
    {
        return PlayerPrefs.GetInt("lastSelectedSkin", 0);
    }

    private void SaveLastSelectedSkin(int index)
    {
        PlayerPrefs.SetInt("lastSelectedSkin", index);
    }
}
