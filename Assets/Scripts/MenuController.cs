using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    private UIDocument _doc;
    private Button _playButton;
    private Button _quitButton;
    private void Awake()
    {
        _doc = GetComponent<UIDocument>();
        _playButton = _doc.rootVisualElement.Q<Button>("PlayButton");
        _quitButton = _doc.rootVisualElement.Q<Button>("QuitButton");
        _playButton.clicked += PlayButtonOnClicked;
        _quitButton.clicked += Quit;
    }

    private void PlayButtonOnClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
