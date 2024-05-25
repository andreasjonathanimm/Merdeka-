using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PullGame : MonoBehaviour
{
    private GameSystem system;

    private bool gameStarted = false;
    private bool winYet = false;
    private bool controlsEnabled = true;
    public float delay = 0.1f;
    public float ropeMoveSpeed = 0.1f;
    public int difficulty = 500;

    // Players and bots
    public GameObject[] players;
    public GameObject[] bots;
    public GameObject rope;
    public BoxCollider border;
    
    // Sounds
    public AudioClip music;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip pullSound;
    public AudioClip endSound;

    // UI
    public TMP_Text countdownText;
    public TMP_Text winText;

    private void OnWin() {
        if (winYet) return;
        winYet = true;
        winText.text = "You Win!";
        winText.gameObject.SetActive(true);
        system.playSound(winSound);
        system.playMusic(endSound);
        system.setMusicLoop(false);

        StartCoroutine(winDelay());
    }

    private void OnLose() {
        // Ensuring no spam triggers
        if (winYet) return;
        winYet = true;
        winText.text = "You Lose!";
        winText.gameObject.SetActive(true);
        system.playSound(loseSound);
        system.playMusic(endSound);
        system.setMusicLoop(false);

        StartCoroutine(winDelay());
    }

    private void BotMove() {
        // Move the rope and players to the bots side
        rope.transform.position += new Vector3(0, 0, ropeMoveSpeed);
        foreach(GameObject player in players) {
            player.transform.position += new Vector3(0, 0, ropeMoveSpeed);
        }
        foreach(GameObject bot in bots) {
            bot.transform.position += new Vector3(0, 0, ropeMoveSpeed);
        }
    }

    private IEnumerator gameCountdown(int seconds) {
        controlsEnabled = false;
        yield return new WaitForSeconds(3);
        controlsEnabled = true;
        gameStarted = true;
    }

    private IEnumerator textCountdown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            int index = i;
            countdownText.text = index.ToString() + "...";
            system.playSound(system.buttonSound);
            yield return new WaitForSeconds(1);
        }
        countdownText.text = "Go!";
        countdownText.gameObject.SetActive(false);
    }

    private IEnumerator controlDelay()
    {
        controlsEnabled = false;
        yield return new WaitForSeconds(delay);
        controlsEnabled = true;
    }

    private IEnumerator winDelay()
    {
        yield return new WaitForSeconds(5);
        system.loadGameMenu();
    }

    public void OnButtonPress() {
        if (controlsEnabled && !winYet && gameStarted) {
            rope.transform.position -= new Vector3(0, 0, ropeMoveSpeed);
            foreach (GameObject player in players) {
                player.transform.position -= new Vector3(0, 0, ropeMoveSpeed);
            }
            foreach (GameObject bot in bots) {
                bot.transform.position -= new Vector3(0, 0, ropeMoveSpeed);
            }
            system.playSound(pullSound);
            StartCoroutine(controlDelay());
        }
    }

    private void Start() {
        system = GameObject.Find("System").GetComponent<GameSystem>();
        system.playMusic(music);

        // Countdown before game starts
        StartCoroutine(gameCountdown(3));
        StartCoroutine(textCountdown(3));
    }

    private void Update() {
        if (!winYet && gameStarted) {
            // Bot pulls randomly
            int random = Random.Range(0, difficulty);
            if (random < 5) {
                BotMove();
            }
            if (border.bounds.Contains(players[0].transform.position)) {
                OnLose();
            }
            else if (border.bounds.Contains(bots[0].transform.position)) {
                OnWin();
            }
        }
    }
}
