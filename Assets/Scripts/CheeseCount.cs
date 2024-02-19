using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheeseCount : MonoBehaviour
{
    public GameManager GameManagerObj;
    int temp = 0;
    public TextMeshProUGUI ValueText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameManagerObj.Cheeses.Count().ToString());
        
        temp = GameManagerObj.Cheeses.Count;
        ValueText.text = $"Cheese: {GameManagerObj.Cheeses.Count}";
    }
}
