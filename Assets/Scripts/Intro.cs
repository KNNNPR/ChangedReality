using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndToLevel : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string levelName = "Level1"; // ��� ����� 1-�� ������

    private void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>(); // �������� VideoPlayer, ���� �� ����������
        }

        videoPlayer.loopPointReached += OnVideoEnd; // �������� �� ������� ���������� �����
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(levelName); // ��������� �������
    }
    public void ChangeScene(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
    }
}