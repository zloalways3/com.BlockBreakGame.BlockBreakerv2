using UnityEngine;
using UnityEngine.UI;

public class CheatToggleManager : MonoBehaviour
{
    public Image cheatToggleImage; // Изображение для состояния чит-кода
    public Image doubleHealthToggleImage; // Изображение для состояния удвоения жизней
    public Sprite cheatEnabledSprite; // Изображение, когда чит-код включен
    public Sprite cheatDisabledSprite; // Изображение, когда чит-код выключен
    public Sprite doubleHealthEnabledSprite; // Изображение, когда удвоение жизней включено
    public Sprite doubleHealthDisabledSprite; // Изображение, когда удвоение жизней выключено

    private void Start()
    {
        Time.timeScale = 1f;

        // Обновляем изображения в соответствии с текущими настройками
        UpdateCheatToggleImage();
        UpdateDoubleHealthToggleImage();
    }

    public void ToggleCheat()
    {
        GameSettingsManager.Instance.ToggleCheat(); // Переключаем состояние чит-кода
        UpdateCheatToggleImage(); // Обновляем изображение
    }

    public void ToggleDoubleHealth()
    {
        GameSettingsManager.Instance.ToggleDoubleHealth(); // Переключаем состояние удвоения жизней
        UpdateDoubleHealthToggleImage(); // Обновляем изображение
    }

    private void UpdateCheatToggleImage()
    {
        if (GameSettingsManager.Instance.IsCheatEnabled())
        {
            cheatToggleImage.sprite = cheatEnabledSprite; // Устанавливаем изображение включенного состояния
        }
        else
        {
            cheatToggleImage.sprite = cheatDisabledSprite; // Устанавливаем изображение выключенного состояния
        }
    }

    private void UpdateDoubleHealthToggleImage()
    {
        if (GameSettingsManager.Instance.IsDoubleHealthEnabled())
        {
            doubleHealthToggleImage.sprite = doubleHealthEnabledSprite; // Устанавливаем изображение включенного состояния
        }
        else
        {
            doubleHealthToggleImage.sprite = doubleHealthDisabledSprite; // Устанавливаем изображение выключенного состояния
        }
    }
}
