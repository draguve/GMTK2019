using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent onButtonPressed;
    public MMFeedback onPressedFeedback;
    
    public UnityEvent onButtonReleased;

    public ButtonState state;
    
    // Start is called before the first frame update
    void Start()
    {
        state = ButtonState.notPressed;
        onPressedFeedback.Initialization();
    }

    public void ButtonPressed()
    {
        if (state == ButtonState.notPressed)
        {
            state = ButtonState.pressed;
            onButtonPressed.Invoke();
            onPressedFeedback.Play(transform.position);
        }
    }

    public void ButtonReleased()
    {
        if (state == ButtonState.pressed)
        {
            state = ButtonState.notPressed;
            onButtonReleased.Invoke();
        }
    }
}

public enum ButtonState
{
    pressed,
    notPressed
}