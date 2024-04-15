using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public GameObject player;
    public float Xoffset = 10f;
    public float Yoffset = 10f;
    public float Zoffset = 10f;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x + Xoffset, player.transform.position.y + Yoffset, player.transform.position.z + Zoffset);
    }
}
