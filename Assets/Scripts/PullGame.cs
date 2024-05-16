using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PullGame : MonoBehaviour
{
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
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip pullSound;
    public AudioClip endSound;
    public AudioSource audioSource;
    public GameObject musicPlayer;

    // UI
    public TMP_Text winText;

    private void OnWin() {
        if (winYet) return;
        winYet = true;
        winText.text = "You Win!";
        winText.gameObject.SetActive(true);
        audioSource.PlayOneShot(winSound);
        musicPlayer.GetComponent<AudioSource>().PlayOneShot(endSound);
        musicPlayer.GetComponent<AudioSource>().Stop();
    }

    private void OnLose() {
        // Ensuring no spam triggers
        if (winYet) return;
        winYet = true;
        winText.text = "You Lose!";
        winText.gameObject.SetActive(true);
        audioSource.PlayOneShot(loseSound);
        musicPlayer.GetComponent<AudioSource>().PlayOneShot(endSound);
        musicPlayer.GetComponent<AudioSource>().Stop();
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

    private IEnumerator controlDelay()
    {
        controlsEnabled = false;
        yield return new WaitForSeconds(delay);
        controlsEnabled = true;
    }

    public void OnButtonPress() {
        if (controlsEnabled && !winYet) {
            rope.transform.position -= new Vector3(0, 0, ropeMoveSpeed);
            audioSource.PlayOneShot(pullSound);
            foreach (GameObject player in players) {
                player.transform.position -= new Vector3(0, 0, ropeMoveSpeed);
            }
            foreach (GameObject bot in bots) {
                bot.transform.position -= new Vector3(0, 0, ropeMoveSpeed);
            }
            StartCoroutine(controlDelay());
        }
    }

    private void Update() {
        if (!winYet) {
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
