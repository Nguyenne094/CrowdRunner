using CandyCoded.HapticFeedback;

public class VibrationManager : Singleton<VibrationManager>
{
    //* Sub via trigger event in SettingsManager script: Assets/Script/Managers/SettingsManager.cs

    // void Start()
    // {
    //     PlayerDetection.onDoorHit += Vibrate;
    //     Enemy.onRunnerDie += Vibrate;
    // }

    void OnDestroy()
    {
        PlayerDetection.onDoorHit -= Vibrate;
        Enemy.onRunnerDie -= Vibrate;
    }

    private void Vibrate()
{
    HapticFeedback.MediumFeedback();
}

    public void Enable()
    {
        PlayerDetection.onDoorHit += Vibrate;
        Enemy.onRunnerDie += Vibrate;
    }

    public void Disable()
    {
        PlayerDetection.onDoorHit -= Vibrate;
        Enemy.onRunnerDie -= Vibrate;
    }
}