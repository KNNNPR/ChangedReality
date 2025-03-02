using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // Ссылка на AudioMixer
    [SerializeField] private Slider volumeSlider; // Ссылка на UI-слайдер

    private const string VolumeKey = "MasterVolume"; // Должно совпадать с параметром в AudioMixer

    private void Start()
    {
        // Проверяем, есть ли сохраненная громкость
        if (PlayerPrefs.HasKey(VolumeKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey);
            SetVolume(savedVolume);
            volumeSlider.value = savedVolume;
        }
        else
        {
            SetVolume(1f); // По умолчанию громкость 100%
            volumeSlider.value = 1f;
        }

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        float volumeDb = Mathf.Log10(volume) * 20; // Перевод в децибелы
        audioMixer.SetFloat(VolumeKey, volumeDb); // Устанавливаем громкость
        PlayerPrefs.SetFloat(VolumeKey, volume); // Сохраняем в PlayerPrefs
        PlayerPrefs.Save();
    }
}