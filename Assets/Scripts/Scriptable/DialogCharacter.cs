using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using Tools.Utils;
using UnityEngine;
using static Facade;

[CreateAssetMenu(fileName = "DialogCharacter", menuName = "DialogCharacter", order = 1)]
public class DialogCharacter : ScriptableObject
{
	public string characterName;
	public Sprite sprite;
}
