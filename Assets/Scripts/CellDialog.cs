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

public class CellDialog : MonoBehaviour
{
	[SerializeField] private Quest questCell;

	private bool isAvailable;

	private void Start()
	{
		isAvailable = true;
	}

	public void Show()
	{
		if (!isAvailable)
			return;

		isAvailable = false;
		QuestDisplay.Instance.quest = questCell;
		QuestDisplay.Instance.Init();
	}
}