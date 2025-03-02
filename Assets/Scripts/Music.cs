using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer; // ������ �� AudioMixer
    [SerializeField] private Slider volumeSlider; // ������ �� UI-�������

    private const string VolumeKey = "MasterVolume"; // ������ ��������� � ���������� � AudioMixer

    private void Start()
    {
        // ���������, ���� �� ����������� ���������
        if (PlayerPrefs.HasKey(VolumeKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey);
            SetVolume(savedVolume);
            volumeSlider.value = savedVolume;
        }
        else
        {
            SetVolume(1f); // �� ��������� ��������� 100%
            volumeSlider.value = 1f;
        }

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        float volumeDb = Mathf.Log10(volume) * 20; // ������� � ��������
        audioMixer.SetFloat(VolumeKey, volumeDb); // ������������� ���������
        PlayerPrefs.SetFloat(VolumeKey, volume); // ��������� � PlayerPrefs
        PlayerPrefs.Save();
    }
}