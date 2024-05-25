using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private GameSystem gameSystem;
    public Button playButton;
    public Button settingsButton;
    public Button exitButton;
    // Start is called before the first frame update
    void Start()
    {
        gameSystem = FindObjectOfType<GameSystem>();
        playButton.onClick.AddListener(gameSystem.loadGameMenu);
        settingsButton.onClick.AddListener(gameSystem.openSettings);
        exitButton.onClick.AddListener(gameSystem.quitGame);
    }

    void Update() {
        if (playButton == null || settingsButton == null || exitButton == null) {
            playButton = GameObject.Find("PlayButton").GetComponent<Button>();
            settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
            exitButton = GameObject.Find("ExitButton").GetComponent<Button>();
            playButton.onClick.AddListener(gameSystem.loadGameMenu);
            settingsButton.onClick.AddListener(gameSystem.openSettings);
            exitButton.onClick.AddListener(gameSystem.quitGame);
        }
    }
}
