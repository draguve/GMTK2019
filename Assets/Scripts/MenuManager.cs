using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameObject PlayPanel,OptionsPanel,CreditsPanel,ExitPanel;
    public TextMeshProUGUI PlayText,OptionsText,CreditsText,ExitText;
    private Animator _playAnimator,_optionsAnimator,_creditsAnimator,_exitAnimator;
    private Animator _playTextAnimator,_optionsTextAnimator,_creditsTextAnimator,_exitTextAnimator;
    private Animator _playContentAnimator,_optionsContentAnimator,_creditsContentAnimator,_exitContentAnimator;

    public GameObject PlayContent, OptionsContent, CreditsContent, ExitContent;

    private int _screenWidth;
    
    private bool _play,_credits,_options,_exit;

    private int state;
    // Start is called before the first frame update
    void Start()
    {
        state = 1;
        _play = true;
        _credits = false;
        _options = false;
        _exit = false;
        _screenWidth = Screen.width;
        _playAnimator = PlayPanel.GetComponent<Animator>();
        _optionsAnimator = OptionsPanel.GetComponent<Animator>();
        _creditsAnimator = CreditsPanel.GetComponent<Animator>();
        _exitAnimator = ExitPanel.GetComponent<Animator>();
        
        _playTextAnimator = PlayText.GetComponent<Animator>();
        _optionsTextAnimator = OptionsText.GetComponent<Animator>();
        _creditsTextAnimator = CreditsText.GetComponent<Animator>();
        _exitTextAnimator = ExitText.GetComponent<Animator>();
        
        _playContentAnimator = PlayText.GetComponent<Animator>();
        _optionsContentAnimator = OptionsText.GetComponent<Animator>();
        _creditsContentAnimator = CreditsText.GetComponent<Animator>();
        _exitContentAnimator = ExitText.GetComponent<Animator>();
        
        _playTextAnimator.SetBool("Play",true);
        _creditsTextAnimator.SetBool("Credits", false);
        _optionsTextAnimator.SetBool("Options", false);
        _exitTextAnimator.SetBool("Exit", false);
        
        _playAnimator.SetBool("Play",true);
        _creditsAnimator.SetBool("Credits", false);
        _optionsAnimator.SetBool("Options", false);
        _exitAnimator.SetBool("Exit", false);
        
        _playContentAnimator.SetBool("Play",true);
        _creditsContentAnimator.SetBool("Credits", false);
        _optionsContentAnimator.SetBool("Options", false);
        _exitContentAnimator.SetBool("Exit", false);

        PlayContent.active = false;
        OptionsContent.active = false;
        CreditsContent.active = false;
        ExitContent.active = false;
        
        
        if (_playAnimator == null)
        {
            Debug.Log("play animator is null");
        }
        
    }
    

    void ChangeState(int x)
    {
        switch (x)
                {
                    case 1:
                        _play = true;
                        _credits = false;
                        _options = false;
                        _exit = false;
                        _playTextAnimator.SetBool("Play",true);
                        _creditsTextAnimator.SetBool("Credits", false);
                        _optionsTextAnimator.SetBool("Options", false);
                        _exitTextAnimator.SetBool("Exit", false);
                        
                        _playAnimator.SetBool("Play",true);
                        _creditsAnimator.SetBool("Credits", false);
                        _optionsAnimator.SetBool("Options", false);
                        _exitAnimator.SetBool("Exit", false);
                        
                        _playContentAnimator.SetBool("Play",true);
                        _creditsContentAnimator.SetBool("Credits", false);
                        _optionsContentAnimator.SetBool("Options", false);
                        _exitContentAnimator.SetBool("Exit", false);
                        
                        PlayContent.active = true;
                        OptionsContent.active = false;
                        CreditsContent.active = false;
                        ExitContent.active = false;
                        
                        //state = state+x;
                        break;
                    case 2:
                        _play = false;
                        _credits = false;
                        _options = true;
                        _exit = false;
                        _playTextAnimator.SetBool("Play",false);
                        _creditsTextAnimator.SetBool("Credits", false);
                        _optionsTextAnimator.SetBool("Options", true);
                        _exitTextAnimator.SetBool("Exit", false);
                        
                        _playAnimator.SetBool("Play",false);
                        _creditsAnimator.SetBool("Credits", false);
                        _optionsAnimator.SetBool("Options", true);
                        _exitAnimator.SetBool("Exit", false);
                        
                        _playContentAnimator.SetBool("Play",false);
                        _creditsContentAnimator.SetBool("Credits", false);
                        _optionsContentAnimator.SetBool("Options", true);
                        _exitContentAnimator.SetBool("Exit", false);
                        
                        PlayContent.active = false;
                        OptionsContent.active = true;
                        CreditsContent.active = false;
                        ExitContent.active = false;
                        
                        //state = state+x;
                        break;
                    case 3:
                        _play = false;
                        _credits = true;
                        _options = false;
                        _exit = false;
                        _playTextAnimator.SetBool("Play",false);
                        _creditsTextAnimator.SetBool("Credits", true);
                        _optionsTextAnimator.SetBool("Options", false);
                        _exitTextAnimator.SetBool("Exit", false);
                        
                        _playAnimator.SetBool("Play",false);
                        _creditsAnimator.SetBool("Credits", true);
                        _optionsAnimator.SetBool("Options", false);
                        _exitAnimator.SetBool("Exit", false);
                        
                        _playContentAnimator.SetBool("Play",false);
                        _creditsContentAnimator.SetBool("Credits", true);
                        _optionsContentAnimator.SetBool("Options", false);
                        _exitContentAnimator.SetBool("Exit", false);
                        
                        PlayContent.active = false;
                        OptionsContent.active = false;
                        CreditsContent.active = true;
                        ExitContent.active = false;
                        
                        
                       // state = state+x;
                        break;
                    case 4:
                        _play = false;
                        _credits = false;
                        _options = false;
                        _exit = true;
                        _playTextAnimator.SetBool("Play",false);
                        _creditsTextAnimator.SetBool("Credits", false);
                        _optionsTextAnimator.SetBool("Options", false);
                        _exitTextAnimator.SetBool("Exit", true);
                        
                        _playAnimator.SetBool("Play",false);
                        _creditsAnimator.SetBool("Credits", false);
                        _optionsAnimator.SetBool("Options", false);
                        _exitAnimator.SetBool("Exit",true);
                        
                        _playContentAnimator.SetBool("Play",false);
                        _creditsContentAnimator.SetBool("Credits", false);
                        _optionsContentAnimator.SetBool("Options", false);
                        _exitContentAnimator.SetBool("Exit", true);
                        
                        
                        PlayContent.active = false;
                        OptionsContent.active = false;
                        CreditsContent.active = false;
                        ExitContent.active = true;
                        
                        //state = (state+x)%4;
                        break;
                    case 0:
                        state = 4;
                        _play = false;
                        _credits = false;
                        _options = false;
                        _exit = true;
                        _playTextAnimator.SetBool("Play",false);
                        _creditsTextAnimator.SetBool("Credits", false);
                        _optionsTextAnimator.SetBool("Options", false);
                        _exitTextAnimator.SetBool("Exit", true);
                        
                        _playAnimator.SetBool("Play",false);
                        _creditsAnimator.SetBool("Credits", false);
                        _optionsAnimator.SetBool("Options", false);
                        _exitAnimator.SetBool("Exit",true);
                        
                        _playContentAnimator.SetBool("Play",false);
                        _creditsContentAnimator.SetBool("Credits", false);
                        _optionsContentAnimator.SetBool("Options", false);
                        _exitContentAnimator.SetBool("Exit", true);
                        
                        
                        PlayContent.active = false;
                        OptionsContent.active = false;
                        CreditsContent.active = false;
                        ExitContent.active = true;
                        
                        //state = (state+x)%4;
                        break;
                        
                }
    }
    
    
    

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        
            if ((Input.GetKeyDown(KeyCode.D)))
            {
                state = (state + 1)%4;
                ChangeState(state);
                
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                state = (state - 1);
                if (state <= 0)
                {
                    state = 4;
                }
                ChangeState(state);
                
            }
            else if ((Input.mousePosition.x > 0) && (Input.mousePosition.x < (_screenWidth / 4)))
            {
                state = 1;
                ChangeState(state);
            }
            else if ((Input.mousePosition.x > _screenWidth/4) && (Input.mousePosition.x < (_screenWidth / 2)))
            {
                state = 2;
                ChangeState(state);
            }
            else if ((Input.mousePosition.x > (_screenWidth/2)) && (Input.mousePosition.x < (3*_screenWidth) / 4))
            {
                state = 3;
                ChangeState(state);
            }
            else if ((Input.mousePosition.x > (3*_screenWidth/4)) && (Input.mousePosition.x < _screenWidth ))
            {
                state = 4;
                ChangeState(state);
            }
        
    }
}
