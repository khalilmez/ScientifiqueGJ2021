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

public class ArchetypeChoice : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private TextMeshProUGUI characterName;
	[SerializeField] private TextMeshProUGUI characterClass;
	[SerializeField] private TextMeshProUGUI healthText;
	[SerializeField] private TextMeshProUGUI staminaText;
	[SerializeField] private TextMeshProUGUI forceText;
	[SerializeField] private TextMeshProUGUI cultureText;
	[SerializeField] private TextMeshProUGUI goldText;
	[SerializeField] private Image bodyImage;
	[SerializeField] private Dependency<ArchetypeSelection> _archetypeSelection;

	private CharacterArchetype archetype;
	private int health;
	private int stamina;
	private int force;
	private int culture;
	private int gold;
	private Sprite body;

	private ArchetypeSelection archetypeSelection => _archetypeSelection.Resolve(this);

	public CharacterArchetype Archetype
	{
		get => archetype;
		set
		{
			archetype = value;

			// Stats Assignation
			characterName.text = archetype.names.Random();
			characterClass.text = archetype.name;
			Health = archetype.health.RandomValue;
			Stamina = archetype.stamina.RandomValue;
			Force = archetype.force.RandomValue;
			Culture = archetype.culture.RandomValue;
			Gold = archetype.gold.RandomValue;
			if (!archetype.bodySprites.IsEmpty())
			{
				Body = archetype.bodySprites.Random();
			}
			if (!archetype.portraitSprites.IsEmpty())
			{
				// We are assuming that portrait has same index as body
				Portrait = archetype.portraitSprites[archetype.bodySprites.IndexOf(Body)];
			}
		}
	}

	public int Health
	{
		get => health;
		private set
		{
			health = value;
			healthText.text = health.ToString();
		}
	}
	public int Stamina
	{
		get => stamina;
		private set
		{
			stamina = value;
			staminaText.text = stamina.ToString();
		}
	}
	public int Force
	{
		get => force;
		private set
		{
			force = value;
			forceText.text = force.ToString();
		}
	}
	public int Culture
	{
		get => culture;
		private set
		{
			culture = value;
			cultureText.text = culture.ToString();
		}
	}
	public int Gold
	{
		get => gold;
		private set
		{
			gold = value;
			goldText.text = gold.ToString();
		}
	}
	public Sprite Body
	{
		get => body;
		private set
		{
			body = value;
			bodyImage.sprite = body;
		}
	}
	public Sprite Portrait { get; private set; }

	public void Select()
	{
		archetypeSelection.ArchetypeChoosen = this;
	}
}
