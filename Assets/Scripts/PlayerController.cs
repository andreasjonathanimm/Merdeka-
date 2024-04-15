using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float timingThreshold = 0.8f;
    public float speed = 5f;
    private float clickTime;
    private bool controlsEnabled = true;
    private Animator animator;
    public GameObject thresholdScroll;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && controlsEnabled)
        {
            clickTime = Time.time;
            if (Time.time - clickTime <= timingThreshold)
            {
                controlsEnabled = false;
                transform.position += new Vector3(0, 0, 1) * speed;
                animator.SetTrigger("jumpTrigger");
            }
            else {
                controlsEnabled = false;
                animator.SetTrigger("fallTrigger");
            }
        }
    }

    public void OnJumpAnimationEnd() {
        controlsEnabled = true;
    }

    public void OnFallAnimationEnd() {
        controlsEnabled = true;
    }
}
