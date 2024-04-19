using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float timingThreshold = 80f;
    public float speed = 5f;
    private float clickTime;
    private float value;
    private bool controlsEnabled = true;
    private bool direction = true;
    private Animator animator;
    public Slider thresholdSlider;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        value = thresholdSlider.value;
        if (Input.GetMouseButtonDown(0) && controlsEnabled)
        {
            if (value > timingThreshold)
            {
                animator.SetTrigger("jumpTrigger");
                controlsEnabled = false;
            }
            else
            {
                animator.SetTrigger("fallTrigger");
                controlsEnabled = false;
            }
        }
    }

    private void FixedUpdate() {
        if (direction) {
            thresholdSlider.value += 0.01f * thresholdSlider.maxValue;
        }
        else {
            thresholdSlider.value -= 0.01f * thresholdSlider.maxValue;
        }
        if (value >= thresholdSlider.maxValue) {
            direction = false;
        }
        else if (value <= 0) {
            direction = true;
        }
    }

    public void OnJumpAnimationEnd() {
        controlsEnabled = true;
    }

    public void OnFallAnimationEnd() {
        controlsEnabled = true;
    }
}
