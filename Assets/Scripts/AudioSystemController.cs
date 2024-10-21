using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSystemController : MonoBehaviour
{
    [SerializeField] private AudioMixer _mainAudioMixer;
    [SerializeField] private Slider _bgMusicSlider; // Слайдер для фоновой музыки
    [SerializeField] private Slider _fxSoundSlider; // Слайдер для звуковых эффектов
    
    private const float DefaultVolume = 0.75f;

    private void Awake()
    {
        LoadAndApplyVolumeSettings();
        SubscribeToSliderEvents();
    }

    private void LoadAndApplyVolumeSettings()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat(ScenePathsAudioParameters.BackgroundMusicLevel, DefaultVolume);
        float savedSoundVolume = PlayerPrefs.GetFloat(ScenePathsAudioParameters.EffectSoundLevel, DefaultVolume);

        _bgMusicSlider.value = savedMusicVolume;
        AdjustMusicVolume(savedMusicVolume);

        _fxSoundSlider.value = savedSoundVolume;
        AdjustSoundVolume(savedSoundVolume);
    }

    private void SubscribeToSliderEvents()
    {
        _bgMusicSlider.onValueChanged.AddListener(AdjustMusicVolume);
        _fxSoundSlider.onValueChanged.AddListener(AdjustSoundVolume);
    }

    public void AdjustMusicVolume(float volume)
    {
        _mainAudioMixer.SetFloat(ScenePathsAudioParameters.BackgroundMusicLevel, volume); // Логарифмическая шкала
        PlayerPrefs.SetFloat(ScenePathsAudioParameters.BackgroundMusicLevel, volume);
    }

    public void AdjustSoundVolume(float volume)
    {
        _mainAudioMixer.SetFloat(ScenePathsAudioParameters.EffectSoundLevel, volume); // Логарифмическая шкала
        PlayerPrefs.SetFloat(ScenePathsAudioParameters.EffectSoundLevel, volume);
    }
}