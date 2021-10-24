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

public class QuestChoiceBonus : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI valueText;
	[Space]
	[SerializeField] private Color positiveBonus = Color.green;
	[SerializeField] private Color negativeBonus = Color.red;

	private int bonusValue;

	public int Value
	{
		get => bonusValue;
		set
		{
			bonusValue = value;
			gameObject.SetActive(bonusValue != 0);
			if (bonusValue > 0)
			{
				valueText.text = $"+{bonusValue}";
			}
			else
			{
				valueText.text = bonusValue.ToString();
			}
			valueText.color = bonusValue > 0 ? positiveBonus : negativeBonus;
		}
	}
}