using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoordinatesFieldEncoder : MonoBehaviour
{

    [SerializeField] private int coordinateSize;
    [SerializeField] private TMP_InputField input;

    public void OnTypeCoordinates(string coordinates)
    {
        if(coordinates.Length == coordinateSize)
        {
            coordinates += "-";
        }
        else
        {
            if (coordinates.Length < coordinateSize)
            {
                if (coordinates.Contains("-"))
                {
                    coordinates = coordinates.Remove(coordinates.IndexOf("-"));
                }
            }
        }

        input.text = coordinates;
       // input.MoveToEndOfLine(false,false);
    }
}
