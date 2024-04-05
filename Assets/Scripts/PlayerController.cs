using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float timingThreshold = 0.8f;
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
                transform.Translate(Vector3.forward * Time.deltaTime * 5f);
            }
            else {
                controlsEnabled = false;
                animator.SetTrigger("fallTrigger");
            }
        }
    }

    public void OnFallAnimationEnd() {
        controlsEnabled = true;
    }
}
