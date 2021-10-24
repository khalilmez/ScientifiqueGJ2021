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
		StartCoroutine(DisplayQuest());

		choicesDictionary = new Dictionary<string, Choice>();
		Title.text = quest.title;
		Description.text = quest.description;
		choices = quest.choices;
		int i = 0;
		foreach (Transform child in choicesList.transform)
		{

			if (i < choices.Count && choices[i] != null)
			{
				child.gameObject.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = choices[i].text;
				choicesDictionary.Add(child.name, choices[i]);
                if ((Player.Gold + choices[i].bonusGold) < 0)
                {
					child.gameObject.GetComponent<Button>().interactable = false;
                }
                else
				{
					child.gameObject.GetComponent<Button>().interactable = true;
				}
				child.gameObject.SetActive(true);
			}
			else
			{
				child.gameObject.SetActive(false);
			}
			i++;
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
				quest = choicesDictionary[button.name].conclusion;
				Init();
			}
			else
			{
				group.interactable = false;
				group.alpha = 0f;
			}
        }
        else
		{
			group.interactable = false;
			group.alpha = 0f;
		}
		/*if (choices.IsEmpty())
		{
			group.interactable = false;
			group.alpha = 0f;
		}
		else
		{
			Toggle toggle = choicesList.GetComponent<ToggleGroup>().ActiveToggles().RandomOrDefault();

			Player.Health += choicesDictionary[toggle.name].bonusHealth;
			Player.Gold += choicesDictionary[toggle.name].bonusGold;

			if (!choicesDictionary.ContainsKey(toggle.name) || choicesDictionary[toggle.name].conclusion == null)
			{
				group.interactable = false;
				group.alpha = 0f;
			}
			else
			{
				quest = choicesDictionary[toggle.name].conclusion;


				Init();
			}
		}*/
	}
	private IEnumerator DisplayQuest()
	{
		for(int i = 0; i < 10; i++)
        {
			group.alpha += 0.1f;
			yield return new WaitForSeconds(0.1f);
		}
	}
}
