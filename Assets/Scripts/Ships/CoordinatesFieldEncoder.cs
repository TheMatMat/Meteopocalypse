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

        input.text = coordinates;
    }
}
