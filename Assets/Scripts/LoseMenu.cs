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

public class LoseMenu : PopupSingleton
{
	public static LoseMenu Instance { get; private set; }

	private void Awake() => Instance = this;

	public override void Show()
	{
		base.Show();
		HUDContent.Hide();
	}
}