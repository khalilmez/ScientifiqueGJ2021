using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Tools;
using Tools.Utils;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

// See Design Pattern example: https://www.notion.so/Index-8c49dc7f08e241238ca8b933268d2661
public sealed class PrefabsData : BaseIndex
{
	private static PrefabsData _instance;
	public static PrefabsData Instance => GetOrLoad(ref _instance);

	// Set up your references below!
	[Header("Prefabs")]
	public PlayerController playerPrefab;

	[Header("Character Archetype")]
	public CharacterArchetype aristocratDetails;
	public CharacterArchetype companionDetails;
	public CharacterArchetype merchantDetails;
	public CharacterArchetype priestDetails;
	public CharacterArchetype scholarDetails;

	[Header("Cell Type Details")]
	public CellTypeDetails pathDetails;
	public CellTypeDetails romaDetails;
}