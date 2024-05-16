using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerClimb : MonoBehaviour
{
    private Rigidbody rb;
    private bool controlsEnabled = true;
    private bool winYet = false;
    private bool jump = false;
    public Slider thresholdSlider;
    public float[] timingThresholds;
    public float speed = 0.8f;
    public float delay = 0.25f;
    public float limit = 1.5f;
    public AudioClip jumpSound;
    public AudioClip fallSound;
    public AudioSource audioSource;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void checkThreshold() {
        for (int i = 0; i < timingThresholds.Length; i+=2) {
            if (thresholdSlider.value >= timingThresholds[i] && thresholdSlider.value <= timingThresholds[i+1]) {
                jump = true;
                break;
            }
        }
        if (jump) {
            audioSource.clip = jumpSound;
            audioSource.Play();
            rb.AddForce(new Vector3(0, speed, 0), ForceMode.Impulse);
            StartCoroutine(ResetControls());
        } else {
            audioSource.clip = fallSound;
            audioSource.Play();
            if (transform.position.y > limit) {
                rb.AddForce(new Vector3(0, -speed/2, 0), ForceMode.Impulse);
            }
            StartCoroutine(ResetControls());
        }
        jump = false;
    }

    private IEnumerator ResetControls() {
        yield return new WaitForSeconds(delay);
        controlsEnabled = true;
    }

    public void OnButtonPress() {
        if (controlsEnabled && !winYet) {
            checkThreshold();
        }
    }

    public void OnWin(int ranking) {
        Debug.Log("You win! Your ranking is: " + ranking);
        winYet = true;
        thresholdSlider.gameObject.SetActive(false);
    }
}
