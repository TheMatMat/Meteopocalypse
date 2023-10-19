using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class NumberSpriteCreator : MonoBehaviour
{
    [Serializable]
    public struct NumberSprite
    {
        public string numberText;
        public Sprite numberSprite;
    }
    
    
    private int _number;
    [SerializeField] private List<NumberSprite> numbersSprite;
    

    public int Number
    {
        get => _number;
        set
        {
            _number = value;
            DestroySprites();
            CreateSprite(_number);
        }
    }


    private void DestroySprites()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }


    private void CreateSprite(int val)
    {
        int hundred = val / 100;
        val -= hundred * 100;
        int ten = val / 10;
        val -= ten * 10;
        int unit = val;

        if (hundred != 0)
        {
            CreateObject(hundred);
            CreateObject(ten);
            CreateObject(unit);
            Debug.Log("result " + ( hundred * 100));
            Debug.Log("result2 " + ( ten * 10));
            Debug.Log("hundred " + hundred + " ten " + ten + " unit " + unit);
        }
        else if (ten != 0)
        {
            CreateObject(ten);
            CreateObject(unit);
        }
        else
        {
            CreateObject(unit);
        }
    }

    private void CreateObject(int val)
    {
        GameObject imageInstance = new GameObject("Image");
        imageInstance.transform.parent = transform;

        Image image = imageInstance.AddComponent<Image>();
        image.sprite = numbersSprite.FirstOrDefault(numberSprite => numberSprite.numberText == val.ToString()).numberSprite;
        image.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 150);
    }
    
}
