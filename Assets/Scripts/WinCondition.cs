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
            player.GetComponent<PlayerController>().OnWin(ranking);
            winYet = true;
            ranking += 1;
        }
        else  if (other != playerCollider) {
            other.GetComponent<BotController>().OnWin(ranking);
            ranking += 1;
        }
    }
}
