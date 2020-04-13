// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/InputManager.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputManager : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputManager()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputManager"",
    ""maps"": [
        {
            ""name"": ""Menu"",
            ""id"": ""87ca7107-d1af-4eae-aed3-0a288067a327"",
            ""actions"": [
                {
                    ""name"": ""Resume"",
                    ""type"": ""Button"",
                    ""id"": ""26d8b26e-93a0-428f-8c23-f606a6b8e305"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""53c084f0-94ab-40cc-a534-313430683ff0"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Resume"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Tile"",
            ""id"": ""653dde73-0d32-4499-8f1b-8f1ab7a04582"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""6f32ad53-b1db-4e10-8cc2-d206acf7d795"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9524b064-b590-422f-8a74-302e3ad3ba41"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bdbc91a6-5344-49dc-957e-952b31da98d3"",
                    ""path"": ""<Touchscreen>/primaryTouch/tap"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Gameplay"",
            ""id"": ""8376d0a9-5049-432e-ad77-4723e3dcb1fb"",
            ""actions"": [
                {
                    ""name"": ""Confirm Turn"",
                    ""type"": ""Button"",
                    ""id"": ""69b88014-4aab-4eea-86e6-cfc7873d4f16"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""9994e53f-435f-44d7-bef1-b075828e7e4e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""QuickSave"",
                    ""type"": ""Button"",
                    ""id"": ""334d9584-d9bd-41f9-b9d2-e5c8030a9815"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4bbdb1fe-68d8-4c69-be85-6035ca0d9a52"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""00d81810-94fb-42fa-8d69-c55cc8c4dded"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""820addd8-3236-4765-b5ab-392b6938ce6d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c947662-63c8-4cb2-a30d-df5b069362db"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""QuickSave"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Resume = m_Menu.FindAction("Resume", throwIfNotFound: true);
        // Tile
        m_Tile = asset.FindActionMap("Tile", throwIfNotFound: true);
        m_Tile_Click = m_Tile.FindAction("Click", throwIfNotFound: true);
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_ConfirmTurn = m_Gameplay.FindAction("Confirm Turn", throwIfNotFound: true);
        m_Gameplay_Pause = m_Gameplay.FindAction("Pause", throwIfNotFound: true);
        m_Gameplay_QuickSave = m_Gameplay.FindAction("QuickSave", throwIfNotFound: true);
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

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Resume;
    public struct MenuActions
    {
        private @InputManager m_Wrapper;
        public MenuActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Resume => m_Wrapper.m_Menu_Resume;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Resume.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnResume;
                @Resume.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnResume;
                @Resume.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnResume;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Resume.started += instance.OnResume;
                @Resume.performed += instance.OnResume;
                @Resume.canceled += instance.OnResume;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);

    // Tile
    private readonly InputActionMap m_Tile;
    private ITileActions m_TileActionsCallbackInterface;
    private readonly InputAction m_Tile_Click;
    public struct TileActions
    {
        private @InputManager m_Wrapper;
        public TileActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_Tile_Click;
        public InputActionMap Get() { return m_Wrapper.m_Tile; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TileActions set) { return set.Get(); }
        public void SetCallbacks(ITileActions instance)
        {
            if (m_Wrapper.m_TileActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_TileActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_TileActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_TileActionsCallbackInterface.OnClick;
            }
            m_Wrapper.m_TileActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
            }
        }
    }
    public TileActions @Tile => new TileActions(this);

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_ConfirmTurn;
    private readonly InputAction m_Gameplay_Pause;
    private readonly InputAction m_Gameplay_QuickSave;
    public struct GameplayActions
    {
        private @InputManager m_Wrapper;
        public GameplayActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @ConfirmTurn => m_Wrapper.m_Gameplay_ConfirmTurn;
        public InputAction @Pause => m_Wrapper.m_Gameplay_Pause;
        public InputAction @QuickSave => m_Wrapper.m_Gameplay_QuickSave;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @ConfirmTurn.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnConfirmTurn;
                @ConfirmTurn.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnConfirmTurn;
                @ConfirmTurn.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnConfirmTurn;
                @Pause.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPause;
                @QuickSave.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnQuickSave;
                @QuickSave.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnQuickSave;
                @QuickSave.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnQuickSave;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ConfirmTurn.started += instance.OnConfirmTurn;
                @ConfirmTurn.performed += instance.OnConfirmTurn;
                @ConfirmTurn.canceled += instance.OnConfirmTurn;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @QuickSave.started += instance.OnQuickSave;
                @QuickSave.performed += instance.OnQuickSave;
                @QuickSave.canceled += instance.OnQuickSave;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IMenuActions
    {
        void OnResume(InputAction.CallbackContext context);
    }
    public interface ITileActions
    {
        void OnClick(InputAction.CallbackContext context);
    }
    public interface IGameplayActions
    {
        void OnConfirmTurn(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnQuickSave(InputAction.CallbackContext context);
    }
}
