using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotControllerClimb : MonoBehaviour
{
    private Rigidbody rb;
    private bool controlsEnabled = true;
    private float speed;
    private float delay;
    private int random;
    private bool winYet = false;
    public float speedMin = 0.5f;
    public float speedMax = 1f;
    public float delayMin = 0.2f;
    public float delayMax = 0.5f;
    public int difficulty = 4; // lower is harder (3 is always up, 4 is 75% up)

    private IEnumerator delayMove()
    {
        controlsEnabled = false;
        yield return new WaitForSeconds(delay);
        controlsEnabled = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = Random.Range(speedMin, speedMax);
        delay = Random.Range(delayMin, delayMax);
    }

    // Update is called once per frame
    void Update()
    {
        randomMove();
    }

    private void randomMove()
    {
        // Randomly go forward or backward
        if (controlsEnabled && !winYet) {
            random = Random.Range(0, difficulty);
            if (random < 3)
            {
                rb.AddForce(new Vector3(0, speed, 0), ForceMode.Impulse);
                StartCoroutine(delayMove());
            }
            else
            {
                rb.AddForce(new Vector3(0, -speed/2, 0), ForceMode.Impulse);
                StartCoroutine(delayMove());
            }
        }
    }

    public void OnWin(int ranking)
    {
        Debug.Log("Bot " + gameObject.name + " wins! Ranking: " + ranking);
        winYet = true;
    }
}
