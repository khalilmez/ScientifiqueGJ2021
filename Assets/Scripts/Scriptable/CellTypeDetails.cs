using System.Collections.Generic;
using Tools.Utils;
using UnityEngine;

[CreateAssetMenu(fileName = "CellTypeDetails", menuName = "CellTypeDetails", order = 1)]
public class CellTypeDetails : ScriptableObject
{
	public List<Quest> quests = new List<Quest>();
	public List<Sprite> spritesBuilding = new List<Sprite>();
	public List<AudioExpress> audiosEntrance = new List<AudioExpress>();
	public List<ParticleSystem> entranceEffects = new List<ParticleSystem>();
}