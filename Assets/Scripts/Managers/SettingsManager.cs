using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manage sounds and haptics
/// </summary>
public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Sprite optionsOnSprite;
    [SerializeField] private Sprite optionsOffSprite;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image hapticsButtonImage;

    [Header("Setting")]
    [SerializeField] private bool soundState = true;
    [SerializeField] private bool hapticsState = true;

    private string soundsData;
    private string hapticsData;

    void Awake()
    {
        //save data similar to SO but simple, these data will store in your device

        soundState = PlayerPrefs.GetInt(soundsData, 1) == 1;
        hapticsState = PlayerPrefs.GetInt(hapticsData, 1) == 1;
    }

    void Start()
    {
        Setup();
    }

    private void Setup()
    {
        if(soundState){
            EnableSounds();
        }
        else{
            DisableSounds();
        }

        if(hapticsState){
            EnableHaptics();
        }
        else{
            DisableHaptics();
        }
    }

    #region  HapticsState
    public void ChangeHapticsSate(){
        hapticsState = !hapticsState;

        if(hapticsState){
            EnableHaptics();
        }
        else if(!hapticsState){
            DisableHaptics();
        }

        PlayerPrefs.SetInt(hapticsData, hapticsState ? 1 : 0);
    }

    private void DisableHaptics()
    {
        //enable the haptics
        VibrationManager.Instance.Disable();

        //change haptics image button to on
        hapticsButtonImage.sprite = optionsOffSprite;
    }

    private void EnableHaptics()
    {
        //disable the haptics
        VibrationManager.Instance.Enable();

        //change haptics image button to off
        hapticsButtonImage.sprite = optionsOnSprite;
    }

    #endregion

    #region  SoundsState
    public void ChangeSoundsState(){
        soundState = !soundState;

        if(soundState){
            EnableSounds();
        }
        else if(!soundState){
            DisableSounds();
        }

        PlayerPrefs.SetInt(soundsData, soundState ? 1 : 0);
    }

    private void EnableSounds()
    {
        //enable all the sounds
        SoundManager.Instance.EnableSounds();

        //change sound image button to on
        soundsButtonImage.sprite = optionsOnSprite;
    }

    private void DisableSounds()
    {
        //disable all the sounds
        SoundManager.Instance.DisableSounds();

        //change sound image button to off
        soundsButtonImage.sprite = optionsOffSprite;
    }

    #endregion
}
