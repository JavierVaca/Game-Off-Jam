using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopSelector : MonoBehaviour
{
    List<Troop> selectedSoldiers;
    List<Troop> selectedVehicles;
    public RectTransform selectionBox;
    public LayerMask unitLayerMask;
    public LayerMask terrainLayerMask;
    private Camera cam;
    public Player player;
    private Vector2 startPos;
    private TroopMover troopMover;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        selectedSoldiers = new List<Troop>();
        selectedVehicles = new List<Troop>();

        player = GetComponent<Player>();
        troopMover = new TroopMover();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ToggleSelectionVisual(false);
            selectedSoldiers = new List<Troop>();
            selectedVehicles = new List<Troop>();
            
            TrySelect(Input.mousePosition);
            startPos = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
            ToggleSelectionVisual(true);
        }

        if(Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }

        if(Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 600, terrainLayerMask.value))
            {
                troopMover.MoveTroopsToPoint(hit.point, selectedSoldiers, selectedVehicles);
            }
        }
    }

    private void TrySelect(Vector3 mousePosition)
    {
        Ray ray = cam.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 600, unitLayerMask.value))
        {
            Troop troop = hit.collider.GetComponent<Troop>();
            if(player.IsPlayer(troop))
            {
                selectedSoldiers.Add(troop);
                troop.ToggleSelectionVisual(true);
            }
        }
    }

    void UpdateSelectionBox(Vector2 curMousePos)
    {
        if(!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);
        
        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);

        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta/2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta/2);

        foreach(Troop troop in player.GetTroops())
        {
            Vector3 screenPos = cam.WorldToScreenPoint(troop.transform.position);

            if(screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                if(troop is Soldier)
                    selectedSoldiers.Add(troop);
                else
                    selectedVehicles.Add(troop);
            }
        }
    }

    private void ToggleSelectionVisual(bool selected)
    {
        foreach(Troop troop in selectedSoldiers)
        {
            troop.ToggleSelectionVisual(selected);
        }
        foreach(Troop troop in selectedVehicles)
        {
            troop.ToggleSelectionVisual(selected);
        }
    }
}
