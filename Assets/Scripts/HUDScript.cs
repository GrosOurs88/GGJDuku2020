using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public Sprite tape;
    public Sprite fireExtinguisher;
    public Sprite multiTool;

    public TextMeshProUGUI toolDurabilityText;
    public Image toolImage;

    private Tool.Tools previousTool;

    private GameManager gameManager => GameManager.instance;
    private void Start()
    {
        previousTool = Tool.Tools.None;
        toolImage.sprite = null;
        toolDurabilityText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (previousTool != gameManager.currentTool)
        {
            previousTool = gameManager.currentTool;

            switch (previousTool)
            {
                case Tool.Tools.Tape:
                    toolImage.sprite = tape;
                    break;
                case Tool.Tools.FireExtinguisher:
                    toolImage.sprite = fireExtinguisher;
                    break;
                case Tool.Tools.ElectricFixer:
                    toolImage.sprite = multiTool;
                    break;
                case Tool.Tools.None:
                    toolImage.sprite = null;
                    break;
            }
        }


        toolDurabilityText.text = gameManager.CurrentToolDurability.ToString("F0") + "%";
    }
}
