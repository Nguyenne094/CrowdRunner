using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI level_TMP;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject shopPanel;

    void Start()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        shopPanel.SetActive(false);

        level_TMP.text = "Level " + (ChunkManager.Instance.GetLevel());

        progressBar.value = 0;

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }

    void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameManager.GameState state)
    {
        if(state == GameManager.GameState.Gameover){
            ShowGameOver();
        }
        else if(state == GameManager.GameState.LevelComplete){
            ShowLevelComplete();
        }
    }

    void Update()
    {
        UpdateProgressBar();
    }

    public void PlayButtonPressed(){
        GameManager.Instance.SetGameState(GameManager.GameState.Game);
    }

    public void RetryButtonPressed(){
        InterstitialAd.Instance.ShowAd();
        
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }

    public void ShowGameOver(){
        Time.timeScale = 0;
        gamePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void ShowLevelComplete(){
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }

    public void ShowSettingsPanel(){
        Time.timeScale = 0;
    }

    public void HideSettingsPanel(){
        Time.timeScale = 1;
    }

    public void UpdateProgressBar(){
        if(!GameManager.Instance.IsGameState()) return;

        float progress = CrowdManager.Instance.transform.position.z / ChunkManager.Instance.GetFinishZ();

        progressBar.value = progress;
    }
}