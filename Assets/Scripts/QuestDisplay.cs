using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tools;
using Tools.Utils;
using UnityEngine;
using UnityEngine.UI;
using static Facade;

public class QuestDisplay : PopupSingleton
{
	public static QuestDisplay Instance { get; private set; }

	[Header("Animations")]
	[SerializeField] private float displayCharacterRate = 0.02f;

	[Header("Sounds")]
	[SerializeField] private AudioExpress openSound;
	[SerializeField] private AudioExpress writingSound;
	[SerializeField] private AudioExpress closeSound;

	[Header("References")]
	public TextMeshProUGUI Title;
	public TextMeshProUGUI description;
	public List<QuestChoice> choicesList = new List<QuestChoice>();
	public Dictionary<string, Choice> choicesDictionary;

	private List<Choice> choices;
	private Coroutine displayDescriptionCore;
	[SerializeField] GameObject Character;
	public Quest Quest { get; set; }

	private void Awake() => Instance = this;

	public override void Show()
	{
		base.Show();

		choicesDictionary = new Dictionary<string, Choice>();
		Title.text = Quest.title;
		Character.GetComponent<Image>().sprite = Quest.character.sprite;
		displayDescriptionCore = StartCoroutine(ShowDescriptionCore());

		if (IsActive)
		{
			Title.color = Title.color.WithAlpha(0f);
			Title.DOFade(1f, 0.2f);
		}
		else
		{
			openSound.Play();
		}
		writingSound.Play();

		// Reset Choices
		choicesList.ForEach(x => x.gameObject.SetActive(false));
		choicesList.ForEach(x => x.Init());

		choices = Quest.choices;
		if (choices.IsEmpty())
		{
			choicesList[0].Text = "Continuer..";
			choicesList[0].gameObject.SetActive(true);
		}
		else
		{
			int i = 0;
			foreach (QuestChoice choice in choicesList)
			{
				if (i < choices.Count && choices[i] != null)
				{
					choice.Content = choices[i];
					choice.Interactible = (Player.Gold + choices[i].bonusGold) > 0;
					choicesDictionary.Add(choice.name, choices[i]);
					choice.gameObject.SetActive(true);
				}
				else
				{
					choice.gameObject.SetActive(false);
				}

				i++;
			}
		}
	}

	private IEnumerator ShowDescriptionCore()
	{
		description.text = "";
		foreach (var c in Quest.description)
		{
			description.text += c;
			yield return new WaitForSeconds(displayCharacterRate);
		}
	}

	private void StopDescriptionCore()
	{
		if (displayDescriptionCore != null)
		{
			StopCoroutine(displayDescriptionCore);
		}
	}

	public void Submit(QuestChoice choice)
	{
		Music.PlayClick();

		StopDescriptionCore();
		if (choice.Content != null)
		{

			Player.Health += choice.BonusHealth;
			Player.Gold += choice.BonusGold;

			if (choice.Content.conclusion != null && Player.Health > 0)
			{
				Quest = choice.Content.conclusion;
				Show();
			}
			else
			{
				Close();
			}
		}
		else
		{
			Close();
		}
	}

	public override void Close()
	{
		Player.ActiveCrossCells();
		closeSound.Play();
		base.Close();
	}
}
