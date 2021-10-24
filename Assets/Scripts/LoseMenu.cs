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

	[Header("Sounds")]
	[SerializeField] private AudioExpress jingleLose;
	[SerializeField] private AudioExpress musiClose;

	private void Awake() => Instance = this;

	public override void Show()
	{
		base.Show();
		HUDContent.Hide();

		Music.FadOut();
		jingleLose.Play();

		Music.MusicOverride = musiClose.Play();
		Music.MusicOverride.volume = 0f;
		Music.MusicOverride.FadeIn(2.5f);
	}
}