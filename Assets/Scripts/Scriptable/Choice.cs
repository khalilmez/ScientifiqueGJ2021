using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using Tools.Utils;
using UnityEngine;
using static Facade;

[CreateAssetMenu(fileName = "Choice", menuName = "Choice", order = 1)]
public class Choice : ScriptableObject
{
	public string text;
	// Bonuses
	public Quest conclusion;

	[Space]
	public int bonusHealth;
	public int bonusGold;
}
