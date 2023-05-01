using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathUI : MonoBehaviour
{

	public delegate void deathUIEvent();
	public static deathUIEvent RespawnPlayer;

	public void TriggerRespawn()
	{
		RespawnPlayer?.Invoke();
	}

}
