using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerRace : MonoBehaviour
{
    private Rigidbody rb;
    private bool controlsEnabled = true;
    private bool winYet = false;
    public Slider thresholdSlider;
    public float timingThreshold = 80f;
    public float speed = 5f;
    public float delay = 0.25f;

    private IEnumerator ResetControls() {
        yield return new WaitForSeconds(delay);
        controlsEnabled = true;
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    public void OnButtonPress() {
        if (controlsEnabled && !winYet) {
            if (thresholdSlider.value >= timingThreshold) {
                rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
                rb.AddForce(Vector3.forward * speed, ForceMode.Impulse);
                controlsEnabled = false;
                StartCoroutine(ResetControls());
            } else {
                rb.AddForce(Vector3.back * speed / 1.5f, ForceMode.Impulse);
                controlsEnabled = false;
                StartCoroutine(ResetControls());
            }
        }
    }

    public void OnWin(int ranking) {
        Debug.Log("You win! Your ranking is: " + ranking);
        winYet = true;
    }
}
