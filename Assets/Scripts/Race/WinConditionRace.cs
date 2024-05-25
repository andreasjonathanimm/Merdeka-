using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinConditionRace : MonoBehaviour
{
    private BoxCollider objectCollider;
    private GameObject player;
    private GameObject bot;
    private CapsuleCollider playerCollider;
    private bool winYet = false;
    private int ranking = 1;

    // Particles
    public ParticleSystem winEffect1;
    public ParticleSystem winEffect2;

    // Audio
    private GameSystem system;
    public AudioClip music;
    public AudioClip winMusic;

    // UI
    public TMP_Text countdownText;
    public TMP_Text winText;

    private string rankText(int rank)
    {
        if (rank == 1)
        {
            return "1st";
        }
        else if (rank == 2)
        {
            return "2nd";
        }
        else if (rank == 3)
        {
            return "3rd";
        }
        else
        {
            return rank.ToString() + "th";
        }
    }

    private void Start()
    {
        objectCollider = GetComponent<BoxCollider>();
        player = GameObject.Find("Player");
        playerCollider = player.GetComponent<CapsuleCollider>();
        system = GameObject.Find("System").GetComponent<GameSystem>();

        system.playMusic(music);
        system.setMusicLoop(true);

        StartCoroutine(gameCountdown(3));
        StartCoroutine(textCountdown(3));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider && !winYet)
        {
            player.GetComponent<PlayerControllerRace>().OnWin(ranking);
            winYet = true;

            // Play win effects
            winEffect1.Play();
            winEffect2.Play();

            // Play win music
            system.playMusic(winMusic);
            system.setMusicLoop(false);

            // Display win text
            winText.text = "You got " + rankText(ranking) + " place!";
            winText.gameObject.SetActive(true);
            ranking += 1;

            StartCoroutine(winDelay());
        }
        else  if (other != playerCollider) {
            other.GetComponent<BotControllerRace>().OnWin(ranking);
            ranking += 1;
        }
    }

    private IEnumerator winDelay()
    {
        yield return new WaitForSeconds(5);
        system.loadGameMenu();
    }

    private IEnumerator gameCountdown(int seconds)
    {
        foreach (BotControllerRace bot in GameObject.FindObjectsOfType<BotControllerRace>())
        {
            bot.stopControls();
        }
        GameObject.FindObjectsOfType<PlayerControllerRace>()[0].GetComponent<PlayerControllerRace>().stopControls();
        yield return new WaitForSeconds(seconds);
        foreach (BotControllerRace bot in GameObject.FindObjectsOfType<BotControllerRace>())
        {
            bot.startControls();
        };
        GameObject.FindObjectsOfType<PlayerControllerRace>()[0].GetComponent<PlayerControllerRace>().startControls();
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
}
