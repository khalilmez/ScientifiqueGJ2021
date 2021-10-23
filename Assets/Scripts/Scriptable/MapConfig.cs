using UnityEngine;

[CreateAssetMenu(fileName = "MapConfig", menuName = "MapConfig", order = 1)]
public class MapConfig : ScriptableObject
{
	public int healthConsumption = 1;

	[Header("Cell")]
	public float cellActivationTiming = 0.05f;


}