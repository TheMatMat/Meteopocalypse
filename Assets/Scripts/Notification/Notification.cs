using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notification : MonoBehaviour
{
    [SerializeField] int _id;
    public int ID { get { return _id; } set { _id = value; } }

    [SerializeField] Mission _mission;
    public Mission Mission { get { return _mission; } set { _mission = value;  } }

    [SerializeField] Image bgImage;
    [SerializeField] TextMeshProUGUI headerTMP, messageTMP;
    [SerializeField] Slider slider;

    public RectTransform _rectTransform;

    void Awake()
    {
        _rectTransform = this.gameObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        float t = _mission.RemainingTime / _mission.MaxTime;
        slider.value = t;
    }

    public void SetText()
    {
        headerTMP.text = _mission.Planet.Data._name + " : " + _mission.Planet.planetCoordinates.ToString();
        messageTMP.text = _mission.MissionText;
    }

    public void Appear()
    {
        this.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
    }

    public void Dissapear()
    {
        this.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(() => Destroy(this.gameObject));
    }

    public void ColorPulse(Color color)
    {
        Color baseColor = bgImage.color;
        Sequence colorPulseSeq = DOTween.Sequence();

        colorPulseSeq.Append(bgImage.DOColor(color, 0.2f));
        colorPulseSeq.Append(bgImage.DOColor(baseColor, 0.2f)); 
        colorPulseSeq.Append(bgImage.DOColor(color, 0.2f))
            .OnComplete(() => Dissapear());
    }

    public void MoveUp(float deltaY, float time)
    {
       // Debug.Log(_rectTransform.localPosition.y + " - " + deltaY);
        float endPosY = _rectTransform.localPosition.y + deltaY;
        _rectTransform.DOLocalMoveY(endPosY, time);
    }

    public void MoveIn(float delta)
    {
        float posX = this.transform.position.x;

        this.transform.DOMoveX(posX - delta, 2.0f);
    }
}
