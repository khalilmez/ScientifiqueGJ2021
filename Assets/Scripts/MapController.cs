using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tools;
using Tools.Utils;
using UnityEngine;
using static Facade;

public class MapController : MonoBehaviour
{
	public static MapController Instance { get; private set; }

	[SerializeField] private MapConfig config;
	[SerializeField] private Cell playerSpawn;

	public MapConfig Config => config;
	public List<Cell> Cells { get; private set; } = new List<Cell>();

	private void Awake()
	{
		Instance = this;
		Level.OnBoardPlaying += SpawnPlayer;

		Cells = FindObjectsOfType<Cell>().ToList();
	}

	private void SpawnPlayer()
	{
		var player = Instantiate(Prefabs.playerPrefab);
		player.transform.position = playerSpawn.transform.position;
		player.Init();
	}

	public void UnSelectAllCell()
	{
		Cells.ForEach(x => x.IsSelected = false);
	}

	public void UnHoverAllCell()
	{
		Cells.ForEach(x => x.IsHover = false);
	}

	public void ActiveCrossCells(Vector3 position)
	{
		GetCrossCells(position).ForEach(x => x.IsSelected = true);
	}

	public List<Cell> GetCrossCells(Vector3 position)
	{
		var cells = new List<Cell>();
		cells.Add(GetCell(position + new Vector3(1, 0, 0)));
		cells.Add(GetCell(position + new Vector3(-1, 0, 0)));
		cells.Add(GetCell(position + new Vector3(0, 0, 1)));
		cells.Add(GetCell(position + new Vector3(0, 0, -1)));
		cells = cells.WithoutNullValues().ToList();
		return cells;
	}

	private Cell GetCell(Vector3 position)
	{
		return Cells.Where(x => x.transform.position == position).FirstOrDefault();
	}

	[ContextMenu("Organize Cells")]
	private void OrganizeCells()
	{
		List<Cell> Cells = FindObjectsOfType<Cell>().ToList();
		Cells.ForEach(x => x.name = $"({x.transform.position.x}, {x.transform.position.z})");
		Cells = Cells.OrderBy(x => x.transform.position.x).ThenBy(x => x.transform.position.z).ToList();
		Cells.ForEach(x => x.transform.SetSiblingIndex(Cells.IndexOf(x)));
	}
}
