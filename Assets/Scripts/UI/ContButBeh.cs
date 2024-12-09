using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContButBeh : MonoBehaviour
{
    void Start()
    {
        Debug.Log(DataTerminals.IsFirstTerminalWasFirstTimeVisit());
        if (DataTerminals.IsFirstTerminalWasFirstTimeVisit())
        {
            gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }
    }
}
