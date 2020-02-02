using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleUIScript : MonoBehaviour
{
    private string modulePourcentText;
    private TextMeshPro modulePourcentTextMesh;


    private void Start()
    {
        modulePourcentTextMesh = GetComponent<TextMeshPro>();
        modulePourcentText = modulePourcentTextMesh.text; 
    }

    public void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        modulePourcentText = modulePourcentTextMesh.text;
        float test = float.Parse(modulePourcentText);

        if(test < 25f)
        {
            modulePourcentTextMesh.color = Color.red;
        }

        else if (test > 25f && test <= 50f)
        {
            modulePourcentTextMesh.color = Color.yellow;
        }

        else
        {
            modulePourcentTextMesh.color = Color.green;
        }
    }
}
