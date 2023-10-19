using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISystemElementInfo : MonoBehaviour
{
    
    
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private List<Image> icons; 

    [SerializeField]private GameObject layoutGroup;

    public GameObject LayoutGroup
    {
        get => layoutGroup;
        set => layoutGroup = value;
    }

    public void SetText(string _text)
    {
        text.text = _text;
    }

    public void ResetIcons()
    {
        foreach (Image icon in icons.ToList())
        {
            icons.Remove(icon);
            Destroy(icon.gameObject);
        }
        
    }

    public void RemoveIcon(Sprite sprite)
    {
        Image targetIcon = icons.FirstOrDefault(icon => icon.sprite == sprite);

        if (targetIcon != null)
        {
            icons.Remove(targetIcon);
            Destroy(targetIcon.gameObject);
        }
    }

    public void ReplaceIcon(Sprite lastIcon, Sprite newIcon)
    {
        Image targetIcon = icons.FirstOrDefault(icon => icon.sprite == lastIcon);
        
        if (targetIcon != null)
        {
            targetIcon.sprite = newIcon;
        }
    }

    public void AddIcon(Sprite sprite)
    {
        GameObject imageInstance = new GameObject("Image");
        Image image = imageInstance.AddComponent<Image>();
        image.sprite = sprite;

        imageInstance.GetComponent<RectTransform>().sizeDelta = new Vector2(2, 2);
        
        icons.Add(image);
        imageInstance.transform.parent = layoutGroup.transform;
        
        Debug.Log("add img");
    }
    
}
