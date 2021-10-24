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
	public static QuestDisplay Instance { get; private set; }

	[Header("Animations")]
	[SerializeField] private float fadDuration = 0.2f;

	[Header("References")]
	public TextMeshProUGUI Title;
	public TextMeshProUGUI Description;
	public Image background;
	public List<GameObject> choicesList = new List<GameObject>();
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
			group.DOFade(1f, 0.2f).OnComplete(() => { group.interactable = true; });
		}

		choicesDictionary = new Dictionary<string, Choice>();
		Title.text = Quest.title;
		Description.text = Quest.description;
		choicesList.ForEach(x => x.SetActive(false));

		choices = Quest.choices;
		if (choices.IsEmpty())
		{
			choicesList[0].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Continuer..";
			choicesList[0].SetActive(true);
		}
		else
		{
			int i = 0;
			foreach (GameObject choice in choicesList)
			{
				if (i < choices.Count && choices[i] != null)
				{
					choice.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = choices[i].text;
					choicesDictionary.Add(choice.name, choices[i]);
					if ((Player.Gold + choices[i].bonusGold) < 0)
					{
						choice.GetComponent<Button>().interactable = false;
					}
					else
					{
						choice.GetComponent<Button>().interactable = true;
					}
					choice.SetActive(true);
				}
				else
				{
					choice.SetActive(false);
				}
				i++;
			}
		}
	}

	public void Submit(GameObject button)
	{
		if (choicesDictionary.ContainsKey(button.name))
		{
			Player.Health += choicesDictionary[button.name].bonusHealth;
			Player.Gold += choicesDictionary[button.name].bonusGold;
			if (choicesDictionary[button.name].conclusion != null)
			{
				Quest = choicesDictionary[button.name].conclusion;
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
		group.DOFade(0f, fadDuration);
	}
}
