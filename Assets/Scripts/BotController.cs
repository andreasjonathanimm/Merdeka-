using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    private Rigidbody rb;
    private bool controlsEnabled = true;
    private float speed;
    private float delay;
    private int random;
    private bool winYet = false;
    public float speedMin = 3f;
    public float speedMax = 6f;
    public float delayMin = 0.25f;
    public float delayMax = 0.75f;
    public int difficulty = 4; // lower is harder (1 is always forward)

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
            if (random == 0)
            {
                rb.AddForce(Vector3.up * speed, ForceMode.Impulse);
                rb.AddForce(Vector3.forward * speed, ForceMode.Impulse);
                StartCoroutine(delayMove());
            }
            else
            {
                rb.AddForce(Vector3.back * speed / 2, ForceMode.Impulse);
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
