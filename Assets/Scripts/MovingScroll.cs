using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingScroll : MonoBehaviour
{
    private bool direction = true;
    private Slider slider;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (direction) {
            slider.value += speed;
        }
        else {
            slider.value -= speed;
        }

        if (slider.value >= 100) {
            direction = false;
        }
        else if (slider.value <= 0) {
            direction = true;
        }
    }
}
