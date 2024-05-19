//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/InputSystem/PlayerInputsV1.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputsV1: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputsV1()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputsV1"",
    ""maps"": [
        {
            ""name"": ""BasicInputs"",
            ""id"": ""fff1e486-0f66-4968-a18e-68ff19f8bcd0"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""ed83400a-b046-4e4f-8a49-3406ed494f0d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Push"",
                    ""type"": ""Button"",
                    ""id"": ""33bddab9-a1d9-4870-aac5-4e152598bdc2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""CameraControl"",
                    ""type"": ""Value"",
                    ""id"": ""5e2357b9-2c4b-4f27-937a-8fe60ac30b57"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""AdvanceDialogue"",
                    ""type"": ""Button"",
                    ""id"": ""bbe6c047-4aa7-4a3c-93f4-4cd27775be9b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b60d0b88-4b9d-43eb-a684-0eab0d214c28"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Push"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""baa8433f-db55-40da-8e8a-3d2e807b33d5"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Push"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92f2124a-165d-4b9a-a9c8-d8b60c19ddf1"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Push"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e97b8e5e-dcf9-4931-ac48-560078f88b4e"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""0b3655d8-f253-49e0-8e86-7e148f68caab"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""466543c8-b3e4-43e4-9c86-d6f02be6ba1b"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""67c3545c-df57-4ba9-ac35-bab769f40c87"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1dcbd8ed-fc79-4bcc-b800-abe075b41ace"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""10b6e298-7c89-47f0-b744-2bcf9bdc8f9a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""18d4c0f8-fa7d-4776-a73a-7835704bc041"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e4f2d57-484e-4854-ad6c-cb908b5bdd7b"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraControl"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8bb0f52-dcaa-4cd8-975e-38f54b0954c5"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AdvanceDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c612e63-c9a1-487d-986e-1e10b9044c09"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AdvanceDialogue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // BasicInputs
        m_BasicInputs = asset.FindActionMap("BasicInputs", throwIfNotFound: true);
        m_BasicInputs_Move = m_BasicInputs.FindAction("Move", throwIfNotFound: true);
        m_BasicInputs_Push = m_BasicInputs.FindAction("Push", throwIfNotFound: true);
        m_BasicInputs_CameraControl = m_BasicInputs.FindAction("CameraControl", throwIfNotFound: true);
        m_BasicInputs_AdvanceDialogue = m_BasicInputs.FindAction("AdvanceDialogue", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // BasicInputs
    private readonly InputActionMap m_BasicInputs;
    private List<IBasicInputsActions> m_BasicInputsActionsCallbackInterfaces = new List<IBasicInputsActions>();
    private readonly InputAction m_BasicInputs_Move;
    private readonly InputAction m_BasicInputs_Push;
    private readonly InputAction m_BasicInputs_CameraControl;
    private readonly InputAction m_BasicInputs_AdvanceDialogue;
    public struct BasicInputsActions
    {
        private @PlayerInputsV1 m_Wrapper;
        public BasicInputsActions(@PlayerInputsV1 wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_BasicInputs_Move;
        public InputAction @Push => m_Wrapper.m_BasicInputs_Push;
        public InputAction @CameraControl => m_Wrapper.m_BasicInputs_CameraControl;
        public InputAction @AdvanceDialogue => m_Wrapper.m_BasicInputs_AdvanceDialogue;
        public InputActionMap Get() { return m_Wrapper.m_BasicInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BasicInputsActions set) { return set.Get(); }
        public void AddCallbacks(IBasicInputsActions instance)
        {
            if (instance == null || m_Wrapper.m_BasicInputsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BasicInputsActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Push.started += instance.OnPush;
            @Push.performed += instance.OnPush;
            @Push.canceled += instance.OnPush;
            @CameraControl.started += instance.OnCameraControl;
            @CameraControl.performed += instance.OnCameraControl;
            @CameraControl.canceled += instance.OnCameraControl;
            @AdvanceDialogue.started += instance.OnAdvanceDialogue;
            @AdvanceDialogue.performed += instance.OnAdvanceDialogue;
            @AdvanceDialogue.canceled += instance.OnAdvanceDialogue;
        }

        private void UnregisterCallbacks(IBasicInputsActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Push.started -= instance.OnPush;
            @Push.performed -= instance.OnPush;
            @Push.canceled -= instance.OnPush;
            @CameraControl.started -= instance.OnCameraControl;
            @CameraControl.performed -= instance.OnCameraControl;
            @CameraControl.canceled -= instance.OnCameraControl;
            @AdvanceDialogue.started -= instance.OnAdvanceDialogue;
            @AdvanceDialogue.performed -= instance.OnAdvanceDialogue;
            @AdvanceDialogue.canceled -= instance.OnAdvanceDialogue;
        }

        public void RemoveCallbacks(IBasicInputsActions instance)
        {
            if (m_Wrapper.m_BasicInputsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBasicInputsActions instance)
        {
            foreach (var item in m_Wrapper.m_BasicInputsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BasicInputsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BasicInputsActions @BasicInputs => new BasicInputsActions(this);
    public interface IBasicInputsActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnPush(InputAction.CallbackContext context);
        void OnCameraControl(InputAction.CallbackContext context);
        void OnAdvanceDialogue(InputAction.CallbackContext context);
    }
}
