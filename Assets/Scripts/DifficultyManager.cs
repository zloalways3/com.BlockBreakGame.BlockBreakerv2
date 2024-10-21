using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    // Возможные сложности
    public enum DifficultyLevel { Easy, Normal, Hard, VeryHard, Insane }
    
    public DifficultyLevel currentDifficulty; // Текущая сложность

    private void Awake()
    {
        // Устанавливаем текущую сложность из GameSettingsManager
        currentDifficulty = FindObjectOfType<GameSettingsManager>().currentDifficulty;
        Debug.Log(currentDifficulty);
    }

    // Возвращает список доступных жизней для блоков в зависимости от сложности
    public List<int> GetHealthLevels()
    {
        List<int> healthLevels = new List<int>();

        if (GameSettingsManager.Instance.cheatEnabled)
        {
            healthLevels.Add(1); // При включенном чит-коде блоки имеют одну жизнь
            return healthLevels;
        }

        switch (currentDifficulty)
        {
            case DifficultyLevel.Easy:
                healthLevels.Add(2);
                break;
            case DifficultyLevel.Normal:
                healthLevels.Add(2);
                healthLevels.Add(5);
                break;
            case DifficultyLevel.Hard:
                healthLevels.Add(5);
                break;
            case DifficultyLevel.VeryHard:
                healthLevels.Add(5);
                healthLevels.Add(7);
                break;
            case DifficultyLevel.Insane:
                healthLevels.Add(7);
                healthLevels.Add(9);
                break;
        }

        return healthLevels;
    }
}