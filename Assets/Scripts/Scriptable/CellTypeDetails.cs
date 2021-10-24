using System.Collections.Generic;
using Tools.Utils;
using UnityEngine;

[CreateAssetMenu(fileName = "CellTypeDetails", menuName = "CellTypeDetails", order = 1)]
public class CellTypeDetails : ScriptableObject
{
	public GameObject prefab;
	public List<AudioExpress> audiosEntrance = new List<AudioExpress>();
	public List<AudioExpress> jingles = new List<AudioExpress>();
	public List<ParticleSystem> entranceEffects = new List<ParticleSystem>();
}