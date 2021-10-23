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
using static Facade;

public class LevelController : SceneBase
{
	public static LevelController Instance { get; private set; }

	public delegate void LevelEventHandler();

	public event LevelEventHandler OnChosingArchetype;
	public event LevelEventHandler OnBoardPreparation;
	public event LevelEventHandler OnBoardPlaying;

	private LevelState levelState;

	public LevelState LevelState
	{
		get => levelState;

		set
		{
			levelState = value;
			switch (levelState)
			{
				case LevelState.Introduction:
					break;
				case LevelState.ChosingArchetype:
					OnChosingArchetype?.Invoke();
					break;
				case LevelState.BoardPreparation:
					OnBoardPreparation?.Invoke();
					break;
				case LevelState.BoardPlaying:
					OnBoardPlaying?.Invoke();
					break;
			}
		}
	}

	protected override void Awake()
	{
		base.Awake();
		Instance = this;
		LevelState = LevelState.Introduction;
	}

	protected override void Start()
	{
		base.Start();
		StartCoroutine(StartCore());
	}

	private IEnumerator StartCore()
	{
		yield return new WaitForSeconds(0.5f);
		LevelState = LevelState.BoardPlaying;
	}

	protected override void Update()
	{
		base.Update();
	}

	protected void OnDestroy()
	{
		if (Instance == this)
		{
			Instance = null;
		}
	}
}