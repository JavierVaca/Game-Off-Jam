using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopSelector : MonoBehaviour
{
    List<Troop> selectedTroops;
    public RectTransform selectionBox;
    public LayerMask unitLayerMask;
    private Camera cam;
    public Player player;
    private Vector2 startPos;
    

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        selectedTroops = new List<Troop>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ToggleSelectionVisual(false);
            selectedTroops = new List<Troop>();
            
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
    }

    private void TrySelect(Vector3 mousePosition)
    {
        Ray ray = cam.ScreenPointToRay(mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 600, Color.yellow);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 600, unitLayerMask.value))
        {
            Troop troop = hit.collider.GetComponent<Troop>();
            if(player.IsPlayer(troop))
            {
                selectedTroops.Add(troop);
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

        foreach(Troop troop in player.troops)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(troop.transform.position);

            if(screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y)
            {
                selectedTroops.Add(troop);
            }
        }
    }

    private void ToggleSelectionVisual(bool selected)
    {
        foreach(Troop troop in selectedTroops)
        {
            troop.ToggleSelectionVisual(selected);
        }
    }
}
