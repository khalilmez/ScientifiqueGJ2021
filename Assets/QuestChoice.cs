using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Tools;
using Tools.Utils;
using UnityEngine;
using UnityEngine.UI;
using static Facade;

public class QuestChoice : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI contentText;
	[SerializeField] private Button button;
	[SerializeField] private QuestChoiceBonus bonusHealthDisplay;
	[SerializeField] private QuestChoiceBonus bonusGoldDisplay;

	private string text;
	private Choice content;
	private bool interactible;
	private int bonusHealth;
	private int bonusGold;

	public Choice Content
	{
		get => content;
		set
		{
			content = value;

			Text = content.text;
			BonusHealth = content.bonusHealth;
			BonusGold = content.bonusGold;
		}
	}

	public string Text
	{
		get => text;
		set
		{
			text = value;
			contentText.text = text;
		}
	}

	public bool Interactible
	{
		get => interactible;
		set
		{
			interactible = value;
			button.interactable = interactible;
		}
	}

	public int BonusHealth
	{
		get => bonusHealth;
		set
		{
			bonusHealth = value;
			if (content != null && content.diplayStats)
			{
				bonusHealthDisplay.Value = bonusHealth;
			}
			else
			{
				bonusHealthDisplay.gameObject.SetActive(false);
			}
		}
	}

	public int BonusGold
	{
		get => bonusGold;
		set
		{
			bonusGold = value;
			if (content != null && content.diplayStats)
			{
				bonusGoldDisplay.Value = bonusGold;
			}
			else
			{
				bonusGoldDisplay.gameObject.SetActive(false);
			}
		}
	}

	public void Init()
	{
		content = null;
		Text = "";
		BonusHealth = 0;
		BonusGold = 0;
	}

	public void Submit()
	{
		QuestDisplay.Instance.Submit(this);
	}
}
