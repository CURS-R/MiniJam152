using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheeseCount : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextCheeseUI;
    GameManager gamManegerRef;

    private int cheeseCount;

   public int CheeseCountVar
    {
        get { return cheeseCount; }

        set
        {
            cheeseCount = value;

            TextCheeseUI.text = gamManegerRef.Cheeses.Count().ToString();
            PlayerPrefs.SetInt("Cheese Count", CheeseCountVar);
        }
    }
}
