using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Tools;
using Tools.Utils;
using UnityEngine;
using static Facade;

public class WinConditionSlot : MonoBehaviour
{
	[SerializeField] private Color positiveScore = Color.green;
	[SerializeField] private Color negativeScore = Color.red;

	[Header("References")]
	[SerializeField] private TextMeshProUGUI descriptionText;
	[SerializeField] private TextMeshProUGUI healthRequiredText;
	[SerializeField] private TextMeshProUGUI goldRequiredText;

	public void Setup(WinCondition condition)
	{
		descriptionText.text = condition.description;

		healthRequiredText.text = $"{condition.healthRequired}+";
		healthRequiredText.color = Player.Health >= condition.healthRequired ? positiveScore : negativeScore;

		goldRequiredText.text = $"{condition.goldRequired}+";
		goldRequiredText.color = Player.Gold >= condition.goldRequired ? positiveScore : negativeScore;
	}
}
