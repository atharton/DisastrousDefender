using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderSpawner : MonoBehaviour
{
    [SerializeField] Defender defender;
    float[] yCoordinates;
    float yOffset = +0.5625f/2;
    float startingY = -2.125f; // 0.5625 is offset
    float yIncrement = 0.5625f;
    int yBoxCount = 5;
    int laneNoToSet=1;

    private void Start()
    {
        InitiateYCoordinates();
    }

    private void InitiateYCoordinates()
    {
        yCoordinates = new float[yBoxCount + 1];
        for (int index = 0; index <= yBoxCount; index++)
        {
            yCoordinates[index] = startingY + index * yIncrement;
        }
    }

    private void OnMouseDown()
    {
        SpawnDefender(GetSquareClicked());
    }

    private void SpawnDefender(Vector2 spawnPosition)
    {
        if (defender != null)
        {
            Defender newDefender = Instantiate(
                defender,
                spawnPosition,
                Quaternion.identity) as Defender;
            newDefender.transform.parent = transform;
            newDefender.SetLaneNo(laneNoToSet);
        }
        else return;
    }

    private Vector2 GetSquareClicked()
    {
        Vector2 clickPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(clickPos);
        Vector2 squarePos = SnapToGrid(worldPos);

        return squarePos;
    }
    private Vector2 SnapToGrid(Vector2 rawWorldPos)
    {
        float newX = Mathf.RoundToInt(rawWorldPos.x);
        float newY = GetYGridPos(rawWorldPos.y)+yOffset;
        return new Vector2(newX, newY);
    }

    private float GetYGridPos (float rawYPos)     
    {
        for (int index = 0; index<yBoxCount; index++)
        {
            float lowerCoordinate = yCoordinates[index];
            float upperCoordinate = yCoordinates[index+1];
            if (rawYPos >= lowerCoordinate && rawYPos < upperCoordinate) {
                float gridYPos = (lowerCoordinate + upperCoordinate) / 2;
                SetLaneNo(yBoxCount-index);
                return gridYPos;
            }

        }
        return rawYPos;
    }

    private void SetLaneNo(int lane)
    {
        laneNoToSet = lane;
    }

    public void SetSelectedDefender(Defender defenderSelected)
    {
        defender = defenderSelected;
    }

}
