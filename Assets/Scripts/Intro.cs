using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoEndToLevel : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private string levelName = "Level1"; // Имя сцены 1-го уровня

    private void Start()
    {
        if (videoPlayer == null)
        {
            videoPlayer = GetComponent<VideoPlayer>(); // Получаем VideoPlayer, если не установлен
        }

        videoPlayer.loopPointReached += OnVideoEnd; // Подписка на событие завершения видео
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(levelName); // Загружаем уровень
    }
    public void ChangeScene(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
    }
}