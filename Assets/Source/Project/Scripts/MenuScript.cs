using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Text titleConfirmation;
    public GameObject painelConfirmation;

    public GameObject canvas;

    private string sceneToConfirm;

    public void StartScene(string scene)
    {
        Time.timeScale = 1;
        if (scene.CompareTo("Quit") == 0)
            Application.Quit();
        else
            SceneManager.LoadScene(scene);
    }

    public void OpenCanvas()
    {
        canvas.SetActive(true);
    }

    public void Confirm()
    {
        StartScene(sceneToConfirm);
    }

    public void AskCorfimation(string scene)
    {
        sceneToConfirm = scene;
        titleConfirmation.text = scene + "?";
        painelConfirmation.SetActive(true);
    }
    
    public void CancelCorfimation()
    {
        painelConfirmation.SetActive(false);
    }
}
