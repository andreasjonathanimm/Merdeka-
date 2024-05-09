using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinCondition : MonoBehaviour
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
    public AudioSource musicPlayer;
    public AudioClip winMusic;

    // UI
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
            musicPlayer.clip = winMusic;
            musicPlayer.Play();
            musicPlayer.loop = false;

            // Display win text
            winText.text = "You got " + rankText(ranking) + " place!";
            winText.gameObject.SetActive(true);
            ranking += 1;
        }
        else  if (other != playerCollider) {
            other.GetComponent<BotControllerRace>().OnWin(ranking);
            ranking += 1;
        }
    }
}
