using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Tools;
using Tools.Utils;
using UnityEngine;
using UnityEngine.UI;
using static Facade;

[System.Serializable]
public class WinCondition
{
	public string description;
	public int healthRequired;
	public int goldRequired;
}