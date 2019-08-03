using System;
using DG.Tweening;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverAnimator : MonoBehaviour,IPointerEnterHandler,IPointerClickHandler,IPointerExitHandler,ISelectHandler,ISubmitHandler,IDeselectHandler
{

    public float hoverAnimationTime = 0.5f;
    public float hoverScale = 1.2f;
    public int hoverVibrato = 10;
    [Range(0f,1f)]
    public float hoverElasticity = 1f;
    public Vector3 hoverOffset;
    public AnimationCurve hoverCurve;
    public AnimationCurve hoverStartCurve;
    
    public MMFeedback onMouseEnter;
    public UnityEvent onMouseEnterEvent;
    public MMFeedback onClick;
    public UnityEvent onClickEvent;
    public MMFeedback onMouseExit;
    public UnityEvent onMouseExitEvent;

    public bool changeMinOnHover = false;
    
    [Condition("changeMinOnHover", true)]
    public float increaseInWidth = 0f;
    [Condition("changeMinOnHover", true)]
    public float increaseInHeight = 0f;

    public TextMeshProUGUI buttonText;
    public String buttonTextContent;
    
    [InspectorButton("SetText")]
    public bool setTextButton;
    
    private Sequence _tween;
    private bool _entered = false;
    private LayoutElement _buttonElement;
    private Vector2 _initialElementMin;


    private void Start()
    {
        onMouseEnter.Initialization();
        onMouseExit.Initialization();
        onClick.Initialization();
        _tween = DOTween.Sequence();
        _buttonElement = GetComponentInParent<LayoutElement>();
        _initialElementMin = new Vector2(_buttonElement.minWidth,_buttonElement.minHeight);
    }
    

    public void ItemSelected()
    {
        if (!_entered)
        {
            _entered = true;
            _tween.Kill(true);
            _tween.Append(transform.DOLocalMove(hoverOffset,hoverAnimationTime,transform).SetEase(hoverCurve));
            _tween.Append(transform.DOScale(Vector3.one * hoverScale, hoverAnimationTime).SetEase(hoverStartCurve));
            if (changeMinOnHover)
            {
                _tween.Append(_buttonElement.DOMinSize(_initialElementMin + Vector2.right * increaseInWidth +
                                                       Vector2.up * increaseInHeight,hoverAnimationTime,true)
                    .SetEase(hoverStartCurve));
            }
            onMouseEnter.Play(transform.position);
            onMouseEnterEvent.Invoke();
        }
    }

    public void ItemDeselected()
    {
        if (_entered)
        {
            _entered = false;
            _tween.Kill(true);
            _tween.Append(transform.DOLocalMove(Vector3.zero,hoverAnimationTime,transform).SetEase(hoverCurve));
            _tween.Append(transform.DOScale(Vector3.one, hoverAnimationTime).SetEase(hoverCurve));
            onMouseExit.Play(transform.position);
            if (changeMinOnHover)
            {
                _tween.Append(_buttonElement.DOMinSize(_initialElementMin,hoverAnimationTime,true)
                    .SetEase(hoverCurve));
            }
            onMouseExitEvent.Invoke();
        }
    }

    public void ItemPressed()
    {
        onClick.Play(transform.position);
        ItemDeselected();
        onClickEvent.Invoke();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemSelected();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ItemPressed();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemDeselected();
    }

    public void OnSelect(BaseEventData eventData)
    {
        ItemSelected();
    }

    public void OnSubmit(BaseEventData eventData)
    {
        ItemPressed();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        ItemDeselected();
    }

    public void SetText()
    {
        SetButtonText(buttonTextContent);
    }

    public void SetButtonText(string text)
    {
        buttonText.SetText(text);
    }
}
