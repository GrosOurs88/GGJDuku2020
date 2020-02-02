using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModuleUIScript : MonoBehaviour
{
    private string modulePourcentText;

    private TextMesh modulePourcentTextMesh;


    private void Start()
    {
        modulePourcentTextMesh = GetComponent<TextMesh>();
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
