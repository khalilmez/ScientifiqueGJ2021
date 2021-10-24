using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;
using static Facade;

public class QuestDisplay : MonoBehaviour
{
	private Vector3 animationDefault = Vector3.one * 0.95f;

	public static QuestDisplay Instance { get; private set; }

	[Header("Animations")]
	[SerializeField] private float animationDuration = 0.2f;

	[Header("References")]
	public TextMeshProUGUI Title;
	public TextMeshProUGUI Description;
	public List<QuestChoice> choicesList = new List<QuestChoice>();
	public Dictionary<string, Choice> choicesDictionary;

	[SerializeField] private Dependency<CanvasGroup> _group;

	private List<Choice> choices;

	private CanvasGroup group => _group.Resolve(this);
	public Quest Quest { get; set; }
	public bool IsActive => group.interactable == true;

	private void Awake() => Instance = this;

	public void Init()
	{
		if (!IsActive)
		{
			group.DOKill();
			transform.localScale = animationDefault;
			transform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutSine);
			group.DOFade(1f, animationDuration).OnComplete(() => { group.interactable = true; });
		}

		choicesDictionary = new Dictionary<string, Choice>();
		Title.text = Quest.title;
		Description.text = Quest.description;

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

	public void Submit(QuestChoice choice)
	{
		if (choice.Content != null)
		{

			Player.Health += choice.BonusHealth;
			Player.Gold += choice.BonusGold;
			if (choice.Content.conclusion != null)
			{
				Quest = choice.Content.conclusion;
				Init();
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

	private void Close()
	{
		group.DOKill();
		group.interactable = false;
		transform.DOScale(animationDefault, animationDuration).SetEase(Ease.OutSine);
		group.DOFade(0f, animationDuration);
	}
}
