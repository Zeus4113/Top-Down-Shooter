using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Asset Library", menuName ="Data Assets/Asset Library", order = 1)]
public class AssetPicker : ScriptableObject
{
	[SerializeField] private GameObject[] Assets;

	public GameObject GetRandomAsset()
	{
		return Assets[Random.Range(0, Assets.Length)];
	}
}
