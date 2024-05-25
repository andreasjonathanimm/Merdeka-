using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    private float volume;

    private Button playButton;
    private Button mainMenuSettingsButton;
    private Button exitButton;

    public Button[] gameMenuButtons;
    public AudioSource audioSource;
    public Canvas systemCanvas;
    public Slider volumeSlider;
    public GameObject mainMenu;
    public GameObject gameMenu;
    public GameObject pauseMenu;
    public GameObject pauseButton;
    public GameObject settingsPanel;
    public AudioClip mainMenuMusic;
    public AudioClip buttonSound;

    void Awake() {
        // Check if there is already an instance of GameSystem
        if (FindObjectsOfType<GameSystem>().Length > 1) {
            // If there is already an instance of GameSystem, destroy this instance
            Destroy(this.gameObject);
            return;
        }

        // Make this object persistent
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (PlayerPrefs.HasKey("volume")) {
            volume = PlayerPrefs.GetFloat("volume");
            audioSource.volume = volume;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0) {
            // Add main menu music
            playMusic(mainMenuMusic);
            setMusicLoop(true);
        }

        for (int i = 0; i < gameMenuButtons.Length; i++) {
            int index = i;
            gameMenuButtons[index].onClick.AddListener(delegate { loadGame(gameMenuButtons[index].name); });
        }
    }

    public void pauseGame() {
        playSound(buttonSound);

        // Pause the game
        Time.timeScale = 0;

        // Open the pause menu
        pauseMenu.SetActive(true);

        // Disable the pause button
        pauseButton.SetActive(false);
    }

    public void resumeGame() {
        playSound(buttonSound);

        // Close the pause menu
        pauseMenu.SetActive(false);

        pauseButton.SetActive(true);

        // Resume the game
        Time.timeScale = 1;
    }

    public void loadGameMenu() {
        playSound(buttonSound);

        // Load the game menu
        gameMenu.SetActive(true);

        if (GameObject.Find("MainMenu") != null){
            // Disable main menu
            mainMenu = GameObject.Find("MainMenu"); 
            mainMenu.SetActive(false);
        }
        pauseButton.SetActive(false);
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(false);
        systemCanvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        // Reset the time scale
        Time.timeScale = 1;
    }

    public void loadGame(string sceneName) {
        playSound(buttonSound);

        // Load the game
        SceneManager.LoadScene(sceneName);

        gameMenu.SetActive(false);
        pauseButton.SetActive(true);
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(false);
        systemCanvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        // Reset the time scale
        Time.timeScale = 1;

        if (SceneManager.GetActiveScene().buildIndex == 0) {
            // Add main menu music
            playMusic(mainMenuMusic);
            setMusicLoop(true);
        }
    }

    public void openSettings() {
        playSound(buttonSound);

        if (GameObject.Find("MainMenu") != null) {
            // Disable game Canvas
            mainMenu = GameObject.Find("MainMenu");
            mainMenu.SetActive(false);
        }

        if (SceneManager.GetActiveScene().buildIndex != 0) {
            // Disable game Canvas
            pauseMenu.SetActive(false);
        }
        gameMenu.SetActive(false);
        volumeSlider.value = volume;
        pauseButton.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void closeSettings() {
        playSound(buttonSound);

        // Close the settings panel
        settingsPanel.SetActive(false);

        if (mainMenu != null){
            mainMenu.SetActive(true);
        }

        if (SceneManager.GetActiveScene().buildIndex != 0) {
            pauseMenu.SetActive(true);
        }
    }

    public void goToMainMenu() {
        playSound(buttonSound);
        // Load the main menu
        SceneManager.LoadScene(0);

        // Reset the time scale
        Time.timeScale = 1;

        gameMenu.SetActive(false);
        pauseButton.SetActive(false);
        settingsPanel.SetActive(false);
        pauseMenu.SetActive(false);

        // Add main menu music
        playMusic(mainMenuMusic);
        setMusicLoop(true);
    }

    public void quitGame() {
        playSound(buttonSound);
        // Quit the game
        Application.Quit();
        if (Application.isEditor) {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }

    public void adjustVolume() {
        // Globally adjust the volume of the game
        volume = volumeSlider.value;
        audioSource.volume = volume;

        // Save the volume to PlayerPrefs
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void playSound(AudioClip sound) {
        // Play a sound effect
        audioSource.PlayOneShot(sound);
    }

    public void playMusic(AudioClip music, bool loop = true) {
        // Play a music track
        audioSource.clip = music;
        audioSource.Play();
        audioSource.loop = loop;
    }

    public void stopMusic() {
        // Stop the music
        audioSource.Stop();
    }

    public void pauseMusic() {
        // Pause the music
        audioSource.Pause();
    }

    public void resumeMusic() {
        // Resume the music
        audioSource.UnPause();
    }

    public bool isMusicPlaying() {
        // Check if the music is playing
        return audioSource.isPlaying;
    }

    public void setMusicLoop(bool loop) {
        // Set the music to loop
        audioSource.loop = loop;
    }
}
