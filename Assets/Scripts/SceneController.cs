using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    //willl change our scene to the string passed in
    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    //reloads the current scene we are in
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //loads out title scene. must be called title exactly
    public void ToTitleScene()
    {
        GameController.instance.controlType = ControlType.Normal;
        SceneManager.LoadScene("Title");
    }

    //gets our active scenes name
    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    //quits our game
    public void QuitGame()
    {
        Application.Quit();
    }
}
