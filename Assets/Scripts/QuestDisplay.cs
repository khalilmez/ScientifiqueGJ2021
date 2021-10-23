using System.Collections;
using System.Collections.Generic;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;
public class QuestDisplay : MonoBehaviour
{
	public static QuestDisplay Instance { get; private set; }

	public Quest quest;
	public TextMeshProUGUI Title;
	public TextMeshProUGUI Description;
	public Image background;
	public List<Choice> choices;
	public GameObject choicesList;
	public Dictionary<string, Choice> choicesDictionary;

	[SerializeField] private Dependency<CanvasGroup> _group;

	private CanvasGroup group => _group.Resolve(this);

	public bool IsActive => group.interactable == true;

	private void Awake() => Instance = this;

	public void Init()
	{
		group.interactable = true;
		group.alpha = 1f;

		choicesDictionary = new Dictionary<string, Choice>();
		Title.text = quest.title;
		Description.text = quest.description;
		choices = quest.choices;
		int i = 0;
		foreach (Transform child in choicesList.transform)
		{

			if (i < choices.Count && choices[i] != null)
			{
				child.gameObject.SetActive(true);
				child.gameObject.transform.Find("Label").GetComponent<TextMeshProUGUI>().text = choices[i].text;
				choicesDictionary.Add(child.name, choices[i]);
			}
			else
			{
				child.gameObject.SetActive(false);
			}
			i++;
		}
	}

	public void Submit()
	{
		if (choices.IsEmpty())
		{
			group.interactable = false;
			group.alpha = 0f;
		}
		else
		{
			Toggle toggle = choicesList.GetComponent<ToggleGroup>().ActiveToggles().RandomOrDefault();
			quest = choicesDictionary[toggle.name].conclusion;
			Init();
		}
	}
}
