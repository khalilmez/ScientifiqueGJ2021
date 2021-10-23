using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Tools;
using Tools.Utils;
using UnityEngine;
using UnityEngine.UI;
using static Facade;

public class InputController : MonoBehaviour
{
	[SerializeField] private LayerMask cellMask;
	[SerializeField] private Dependency<Camera> _camera;
	[SerializeField] private GameObject pauseMenu;
	private Camera cam => _camera.Resolve(this);

	private RaycastHit hit;
	private Ray ray;
	private bool isPaused;
	private void Awake()
    {
		isPaused = false;
    }

    private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			PauseGame();
        }
		if (Level.LevelState == LevelState.BoardPlaying && !QuestDisplay.Instance.IsActive && !isPaused)
		{
			// Left Click
			if (Input.GetMouseButtonDown(0))
			{
				ray = cam.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, cellMask, QueryTriggerInteraction.Collide))
				{
					if (hit.collider.TryGetComponent(out Cell cell))
					{
						Player.Move(cell);
					}
				}
			}
			else
			{
				// Mouse Hovering
				ray = cam.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hit, Mathf.Infinity, cellMask, QueryTriggerInteraction.Collide))
				{
					if (hit.collider.TryGetComponent(out Cell cell))
					{
						Map.UnHoverAllCell();
						cell.IsHover = true;
					}
				}
				else
				{
					Map.UnHoverAllCell();
				}
			}
		}
	}

    public void PauseGame()
	{
		isPaused = !isPaused;
		if (isPaused)
		{
			pauseMenu.SetActive(true);
		}
		else
		{
			pauseMenu.SetActive(false);
		}
	}
}