using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private BoxCollider objectCollider;
    private GameObject player;
    private GameObject bot;
    private CapsuleCollider playerCollider;
    private bool winYet = false;
    private int ranking = 1;
    public ParticleSystem winEffect1;
    public ParticleSystem winEffect2;

    private void Start()
    {
        objectCollider = GetComponent<BoxCollider>();
        player = GameObject.Find("Player");
        playerCollider = player.GetComponent<CapsuleCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == playerCollider && !winYet)
        {
            player.GetComponent<PlayerControllerRace>().OnWin(ranking);
            winYet = true;
            ranking += 1;
            winEffect1.Play();
            winEffect2.Play();
        }
        else  if (other != playerCollider) {
            other.GetComponent<BotControllerRace>().OnWin(ranking);
            ranking += 1;
        }
    }
}
