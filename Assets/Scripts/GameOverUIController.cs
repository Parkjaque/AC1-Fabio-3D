using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUIController : MonoBehaviour
{
    public Player player;
    public void OnClickRespawn()
    {
        Debug.Log("Pimba");
        player.Respawn();
    }
}
