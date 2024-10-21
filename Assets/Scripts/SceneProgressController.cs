using TMPro;
using UnityEngine;

public class SceneProgressController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] levelIndicators;
    [SerializeField] private TextMeshProUGUI currentLevelDisplay;

    private int activeLevel;

    private void Awake()
    {
        LoadCurrentLevel();
        UpdateLevelDisplay();
    }

    private void LoadCurrentLevel()
    {
        activeLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
    }

    private void UpdateLevelDisplay()
    {
        currentLevelDisplay.text = "Lvl " + activeLevel;

        foreach (var levelText in levelIndicators)
        {
            levelText.text = "Level " + activeLevel;
        }
    }

    public void WinGame()
    {
        PlayerPrefs.SetInt("CurrentLevel", activeLevel + 1);
        PlayerPrefs.Save();

        var levelTracker = FindObjectOfType<LevelSystemController>();
        levelTracker.MarkLevelAsCompleted(activeLevel);
    }
}