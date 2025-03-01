using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchMenu : MonoBehaviour
{

    public void ChangeScene(int numberScenes)
    {
        SceneManager.LoadScene(numberScenes);
    }
     public void Exit()
    {
        Application.Quit();
    }
}
