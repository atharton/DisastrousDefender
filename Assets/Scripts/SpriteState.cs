using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpriteState 
{
    public Sprite[] spriteList;
    private int currentIndex = 0;

    public Sprite GetCurrentSprite()
    {
        return spriteList[currentIndex];
    }
    public Sprite GetNextSprite()
    {
        if (currentIndex + 1 >= spriteList.Length) currentIndex = 0;
        else currentIndex++;
        return GetCurrentSprite();
    }
    public void SetCurrentSprite(int index)
    {
        currentIndex = index;
    }
}
