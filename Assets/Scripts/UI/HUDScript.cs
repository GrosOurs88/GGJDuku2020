using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    public static HUDScript instance;

    public Sprite tape;
    public Sprite fireExtinguisher;
    public Sprite multiTool;

    public TextMeshProUGUI toolDurabilityText;
    public Image toolImage;

    public TextMeshProUGUI moduleTargetText;

    private Tool.Tools previousTool;
    private Camera camera;

    private GameManager gameManager => GameManager.instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        previousTool = Tool.Tools.None;
        toolImage.sprite = null;
        toolDurabilityText.text = "";
        camera = Camera.main;
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

    public void MousOverModule(MonoBehaviour target)
    {
        string changeText = "";

        if (target is Module module)
        {
            switch (module)
            {
                case null:
                    break;
                case EnergyGeneratorModule energyGeneratorModule:
                    changeText = "Generator";
                    break;
                case EnergyTransmitterModule energyTransmitterModule:
                    changeText = "Energy Repartitor";
                    break;
                case EngineModule engineModule:
                    changeText = "Engine";
                    break;
                case GlassModule glassModule:
                    changeText = "Glass Canopy";
                    break;
                case InfoScreenModule infoScreenModule:
                    changeText = "Information Screen";
                    break;
                case OxygenModule oxygenModule:
                    changeText = "Oxygen";
                    break;
            }
        }

        else if (target is Tool tool)
        {
            switch (tool.tool)
            {
                case Tool.Tools.Tape:
                    changeText = "Duck Tape";
                    break;
                case Tool.Tools.FireExtinguisher:
                    changeText = "Fire Extinguisher";
                    break;
                case Tool.Tools.ElectricFixer:
                    changeText = "Electric Fixer";
                    break;
            }
        }
        
        if (moduleTargetText.text != changeText)
            moduleTargetText.text = changeText;
    }
}
