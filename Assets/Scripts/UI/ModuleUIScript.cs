using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleUIScript : MonoBehaviour
{
    private string modulePourcentText;
    private TextMeshPro modulePourcentTextMesh;

    public float palier1 = 50;
    public float palier2 = 25;
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

        if(test < palier2)
        {
            modulePourcentTextMesh.color = Color.red;
        }

        else if (test > palier2 && test <= palier1)
        {
            modulePourcentTextMesh.color = Color.yellow;
        }

        else
        {
            modulePourcentTextMesh.color = Color.green;
        }
    }
}
