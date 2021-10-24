using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapConfig", menuName = "MapConfig", order = 1)]
public class MapConfig : ScriptableObject
{
	public int healthConsumption = 1;
	public List<WinCondition> winConditions = new List<WinCondition>();

	[Space]
	public float cellActivationTiming = 0.035f;
}