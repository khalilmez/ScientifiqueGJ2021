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

public class ArchetypeSlot : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private TextMeshProUGUI characterName;
	[SerializeField] private TextMeshProUGUI characterClass;
	[SerializeField] private TextMeshProUGUI healthText;
	[SerializeField] private TextMeshProUGUI staminaText;
	[SerializeField] private TextMeshProUGUI forceText;
	[SerializeField] private TextMeshProUGUI cultureText;
	[SerializeField] private TextMeshProUGUI goldText;
	[SerializeField] private Image body;
	[SerializeField] private Dependency<ArchetypeSelection> _archetypeSelection;

	private ArchetypeSelection archetypeSelection => _archetypeSelection.Resolve(this);
}
