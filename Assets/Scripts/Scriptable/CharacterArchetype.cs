using System.Collections.Generic;
using Tools.Utils;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterArchetype", menuName = "CharacterArchetype", order = 1)]
public class CharacterArchetype : ScriptableObject
{
	public List<string> names = new List<string>();
	public List<Sprite> bodySprites = new List<Sprite>();
	public List<Sprite> portraitSprites = new List<Sprite>();

	[Header("Stats")]
	[IntRangeSlider(0, 30)] public IntRange health = new IntRange(5, 10);
	[IntRangeSlider(0, 30)] public IntRange stamina = new IntRange(5, 10);
	[IntRangeSlider(0, 30)] public IntRange force = new IntRange(5, 10);
	[IntRangeSlider(0, 30)] public IntRange culture = new IntRange(5, 10);
	[IntRangeSlider(0, 30)] public IntRange gold = new IntRange(5, 10);
}