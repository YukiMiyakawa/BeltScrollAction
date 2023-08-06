using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �{�^�����ʃN���X
/// </summary>
public class CommonCustomButtom : SceneNameManager,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    protected Graphic graphic;
    protected Color thisColor;

    protected enum ButtonMove
    {
        BrightUp, BrightDown, ScaleDown
    }
    [SerializeField] protected ButtonMove buttonMove;

    protected virtual void Start()
    {
        graphic = GetComponent<Graphic>();
        thisColor = graphic.color;
    }

    // �^�b�v  
    public virtual void OnPointerClick(PointerEventData eventData)
    {
        ClickMethod();
    }
    // �^�b�v�_�E��  
    public void OnPointerDown(PointerEventData eventData)
    {
        switch (buttonMove)
        {
            case ButtonMove.BrightUp:
                graphic.DOColor(new Color(thisColor.r + 0.1f, thisColor.g + 0.1f, thisColor.b + 0.1f), 0.1f);
                break;
            case ButtonMove.BrightDown:
                graphic.DOColor(new Color(thisColor.r - 0.1f, thisColor.g - 0.1f, thisColor.b - 0.1f), 0.1f);
                break;
            case ButtonMove.ScaleDown:
                transform.DOScale(0.95f, 0.24f).SetEase(Ease.OutCubic);
                break;
            default:
                graphic.DOColor(new Color(thisColor.r + 0.1f, thisColor.g + 0.1f, thisColor.b + 0.1f), 0.1f);
                break;
        }

        DownMethod();
    }
    // �^�b�v�A�b�v  
    public void OnPointerUp(PointerEventData eventData)
    {
        switch (buttonMove)
        {
            case ButtonMove.BrightUp:
                graphic.DOColor(thisColor, 0.1f);
                break;
            case ButtonMove.BrightDown:
                graphic.DOColor(thisColor, 0.1f);
                break;
            case ButtonMove.ScaleDown:
                transform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                break;
            default:
                graphic.DOColor(thisColor, 0.1f);
                break;
        }

        UpMethod();
    }

    public virtual void ClickMethod()
    {

    }

    public virtual void DownMethod()
    {

    }

    public virtual void UpMethod()
    {

    }
}
