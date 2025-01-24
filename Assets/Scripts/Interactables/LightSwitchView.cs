using System.Collections.Generic;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    public delegate void LightSwitchDelegate(); //signature of delegate
    public static LightSwitchDelegate lightSwitch; //instance of delegate
    private void Start() => currentState = SwitchState.Off;

    /* private void OnEnable()
     {
         lightSwitch += OnLightSwitchToggled;
     }*/
    private void OnEnable() => lightSwitch += OnLightSwitchToggled;
    private void OnDisable() => lightSwitch -= OnLightSwitchToggled;
   /* public void Interact()
    {
        //Todo - Implement Interaction
        lightSwitch.Invoke();

    }*/
    public void Interact() => lightSwitch?.Invoke();
    private void toggleLights()
    {
        bool lights = false;

        switch (currentState)
        {
            case SwitchState.On:
                currentState = SwitchState.Off;
                lights = false;
                break;
            case SwitchState.Off:
                currentState = SwitchState.On;
                lights = true;
                break;
            case SwitchState.Unresponsive:
                break;
        }
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }
    private void OnLightSwitchToggled()
    {
        toggleLights();
        GameService.Instance.GetInstructionView().HideInstruction(); // hiding instrcutions after the button is pressed
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
    }
}
