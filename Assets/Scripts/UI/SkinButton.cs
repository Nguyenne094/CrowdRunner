using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Change: Skin IMG, lock IMG and button IMG interactable
/// </summary>
public class SkinButton : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image skinImage;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private GameObject selectorImage;

    private bool _unlocked;
    private Button _thisButton;

    void Awake()
    {
        _thisButton = GetComponent<Button>();
    }

    /// <summary>
    /// Assign skin image sprite and set lock state
    /// </summary>
    public void ConfigureSkin(Sprite skinSprite, bool unlocked){

        //* Assign skin and set lock state
        skinImage.sprite = skinSprite;
        this._unlocked = unlocked;

        if(unlocked){
            Unlock();
        }
        else{
            Lock();
        }
    }

    private void Lock()
    {
        _thisButton.interactable = false;
        skinImage.gameObject.SetActive(false);
        lockImage.SetActive(true);
    }

    public void Unlock()
    {
        _thisButton.interactable = true;
        skinImage.gameObject.SetActive(true);
        lockImage.SetActive(false);
    
        _unlocked = true;
    }

    public void Select(){
        selectorImage.SetActive(true);
    }

    public void Deselect(){
        selectorImage.SetActive(false);
    }

    public bool IsUnlocked(){
        return _unlocked;
    }

    /// <returns>Button</returns>
    public Button GetButton(){
        return _thisButton;
    }
}
