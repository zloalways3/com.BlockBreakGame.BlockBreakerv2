using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    public static GameSettingsManager Instance { get; private set; }

    public bool cheatEnabled = false; // Состояние чит-кода
    public bool doubleHealthEnabled = false; // Состояние удвоения жизней
    public DifficultyManager.DifficultyLevel currentDifficulty;

    private void Awake()
    {
        // Реализуем паттерн Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(Instance.gameObject); // Уничтожаем старый объект
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Загружаем настройки при запуске
        LoadSettings();
    }

    public void ToggleCheat()
    {
        cheatEnabled = !cheatEnabled; // Переключаем состояние чит-кода
        PlayerPrefs.SetInt("CheatEnabled", cheatEnabled ? 1 : 0); // Сохраняем в PlayerPrefs
        PlayerPrefs.Save(); // Сохраняем изменения на устройстве
    }

    public void ToggleDoubleHealth()
    {
        doubleHealthEnabled = !doubleHealthEnabled; // Переключаем состояние удвоения жизней
        PlayerPrefs.SetInt("DoubleHealthEnabled", doubleHealthEnabled ? 1 : 0); // Сохраняем в PlayerPrefs
        PlayerPrefs.Save(); // Сохраняем изменения
    }

    // Метод для установки уровня сложности
    public void SetDifficultyFromClick(int difficulty)
    {
        DifficultyManager.DifficultyLevel level = (DifficultyManager.DifficultyLevel)difficulty;
        SetDifficulty(level);
    }

    public void SetDifficulty(DifficultyManager.DifficultyLevel difficulty)
    {
        currentDifficulty = difficulty; // Устанавливаем уровень сложности
        PlayerPrefs.SetInt("Difficulty", (int)currentDifficulty); // Сохраняем уровень сложности
        PlayerPrefs.Save(); // Сохраняем изменения
    }

    // Методы для получения текущих состояний
    public bool IsCheatEnabled()
    {
        return cheatEnabled;
    }

    public bool IsDoubleHealthEnabled()
    {
        return doubleHealthEnabled;
    }

    // Метод для загрузки сохраненных настроек
    private void LoadSettings()
    {
        cheatEnabled = PlayerPrefs.GetInt("CheatEnabled", 0) == 1; // По умолчанию чит-код выключен
        doubleHealthEnabled = PlayerPrefs.GetInt("DoubleHealthEnabled", 0) == 1; // По умолчанию удвоение жизней выключено
        currentDifficulty = (DifficultyManager.DifficultyLevel)PlayerPrefs.GetInt("Difficulty", 0); // По умолчанию лёгкий уровень
    }
}
