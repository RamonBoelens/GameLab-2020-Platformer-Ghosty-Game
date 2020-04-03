using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CSVScriptReader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextAsset csvdata = Resources.Load<TextAsset>("letter_frequency");

        string[] data = csvdata.text.Split(new char[] { '\n' });
        for (int o = 0; o < data.Length; o++)
        {
            Debug.Log("Value" + o.ToString() + data[2].ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {


    }
}
