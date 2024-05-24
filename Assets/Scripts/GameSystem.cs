using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : MonoBehaviour
{
    public float volume = 0.5f;

    private AudioSource audioSource;

    private Canvas systemCanvas;

    private GameObject pauseMenu;
    private bool isPaused = false;
    private Button pauseButton;
    private Button resumeButton;
    private Button settingsButton;
    private Button mainMenuButton;

    private GameObject settingsPanel;
    private bool isSettingsOpen = false;
    private Slider volumeSlider;
    private Button exitSettingsButton;

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
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("volume")) {
            volume = PlayerPrefs.GetFloat("volume");
            audioSource.volume = volume;
        }

        // Find the settings panel and set it to inactive
        settingsPanel = GameObject.Find("SettingsPanel");
        settingsPanel.SetActive(false);

        // Find the volume slider and exit settings button
        volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        volumeSlider.value = volume;
        volumeSlider.onValueChanged.AddListener(delegate { adjustVolume(volumeSlider.value); });
        exitSettingsButton = GameObject.Find("ExitSettingsButton").GetComponent<Button>();
        exitSettingsButton.onClick.AddListener(delegate { closeSettings(); });

        // Find the pause button and add a listener to it
        pauseButton = GameObject.Find("PauseButton").GetComponent<Button>();
        if (pauseButton != null) {
            pauseButton.onClick.AddListener(delegate { pauseGame(); });
        }

        // Find the resume button and add a listener to it
        resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        if (resumeButton != null) {
            resumeButton.onClick.AddListener(delegate { resumeGame(); });
        }

        // Find the settings button and add a listener to it
        settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        if (settingsButton != null) {
            settingsButton.onClick.AddListener(delegate { openSettings(); });
        }
    }

    public void pauseGame() {
        // Pause the game
        Time.timeScale = 0;
        isPaused = true;

        // Open the pause menu
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(true);

        // Find the resume, settings, and exit buttons
        resumeButton = GameObject.Find("ResumeButton").GetComponent<Button>();
        resumeButton.onClick.AddListener(delegate { resumeGame(); });
        settingsButton = GameObject.Find("SettingsButton").GetComponent<Button>();
        settingsButton.onClick.AddListener(delegate { openSettings(); });
        mainMenuButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
        mainMenuButton.onClick.AddListener(delegate { Application.LoadLevel("MainMenu"); });
    }

    public void resumeGame() {
        // Resume the game
        Time.timeScale = 1;
        isPaused = false;

        // Close the pause menu
        pauseMenu.SetActive(false);
    }

    public void openSettings() {
        // Open the settings panel
        settingsPanel.SetActive(true);
        isSettingsOpen = true;
    }

    public void closeSettings() {
        // Close the settings panel
        settingsPanel.SetActive(false);
        isSettingsOpen = false;
    }

    public void mainMenu() {
        // Load the main menu
        Application.LoadLevel("MainMenu");
    }

    public void adjustVolume(float newVolume) {
        // Globally adjust the volume of the game
        volume = newVolume;
        audioSource.volume = volume;

        // Save the volume to PlayerPrefs
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void playSound(AudioClip sound) {
        // Play a sound effect
        audioSource.PlayOneShot(sound);
    }

    public void playMusic(AudioClip music) {
        // Play a music track
        audioSource.clip = music;
        audioSource.Play();
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
