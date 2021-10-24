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

public class WinMenu : PopupSingleton
{
	public static WinMenu Instance { get; private set; }

	[SerializeField] private Transform winConditionsHolder;

	private void Awake() => Instance = this;

	public override void Show()
	{
		base.Show();
		HUDContent.Hide();

		foreach (var condition in Map.Config.winConditions)
		{
			var c = Instantiate(Prefabs.winConditionSlotPrefab, winConditionsHolder);
			c.Setup(condition);
		}
	}
}