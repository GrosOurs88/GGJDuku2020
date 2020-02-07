using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    public enum Tools
    {
        Tape,
        FireExtinguisher,
        ElectricFixer,
        None
    }

    public Tools tool = Tools.None;


    private GameManager gameManager => GameManager.instance;


    public void OnClick()
    {
        gameManager.currentTool = tool;
    }
}
