using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PullGame : MonoBehaviour
{
    private bool winYet = false;
    private bool controlsEnabled = true;
    public float power = 10f;
    public float delay = 0.25f;

    // Players and bots
    public GameObject[] players;
    public GameObject[] bots;
    public BoxCollider border;
    
    // Sounds
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip pullSound;
    public AudioClip music;
    public AudioSource audioSource;

    // UI
    public TMP_Text winText;

    private IEnumerator controlDelay()
    {
        controlsEnabled = false;
        yield return new WaitForSeconds(delay);
        controlsEnabled = true;
    }

    private void OnButtonPress() {
        if (controlsEnabled && !winYet) {
            foreach (GameObject player in players) {
                player.GetComponent<Rigidbody>().AddForce(Vector3.left * power, ForceMode.Impulse);
                audioSource.PlayOneShot(pullSound);
            }
            foreach (GameObject bot in bots) {
                bot.GetComponent<Rigidbody>().AddForce(Vector3.right * power, ForceMode.Impulse);
            }
            StartCoroutine(controlDelay());
        }
    }
}
