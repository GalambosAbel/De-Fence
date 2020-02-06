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
                    ""name"": ""Save"",
                    ""type"": ""Button"",
                    ""id"": ""24117448-fe4a-4514-b1a5-a534dcd900b3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Load"",
                    ""type"": ""Button"",
                    ""id"": ""a94f8e29-0888-4749-aca7-bca509f2b293"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GenerateBoard"",
                    ""type"": ""Button"",
                    ""id"": ""98d7f5e0-1424-44ac-a765-56850abed61b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b4380d1d-df56-46c0-bb00-00f40042307b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Save"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fdf98908-470e-4aef-a0a0-1f967ab3d84b"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Load"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9cd03e36-129c-486b-8863-f4f3936c68e1"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GenerateBoard"",
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
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Save = m_Menu.FindAction("Save", throwIfNotFound: true);
        m_Menu_Load = m_Menu.FindAction("Load", throwIfNotFound: true);
        m_Menu_GenerateBoard = m_Menu.FindAction("GenerateBoard", throwIfNotFound: true);
        // Tile
        m_Tile = asset.FindActionMap("Tile", throwIfNotFound: true);
        m_Tile_Click = m_Tile.FindAction("Click", throwIfNotFound: true);
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
    private readonly InputAction m_Menu_Save;
    private readonly InputAction m_Menu_Load;
    private readonly InputAction m_Menu_GenerateBoard;
    public struct MenuActions
    {
        private @InputManager m_Wrapper;
        public MenuActions(@InputManager wrapper) { m_Wrapper = wrapper; }
        public InputAction @Save => m_Wrapper.m_Menu_Save;
        public InputAction @Load => m_Wrapper.m_Menu_Load;
        public InputAction @GenerateBoard => m_Wrapper.m_Menu_GenerateBoard;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Save.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnSave;
                @Save.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnSave;
                @Save.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnSave;
                @Load.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnLoad;
                @Load.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnLoad;
                @Load.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnLoad;
                @GenerateBoard.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnGenerateBoard;
                @GenerateBoard.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnGenerateBoard;
                @GenerateBoard.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnGenerateBoard;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Save.started += instance.OnSave;
                @Save.performed += instance.OnSave;
                @Save.canceled += instance.OnSave;
                @Load.started += instance.OnLoad;
                @Load.performed += instance.OnLoad;
                @Load.canceled += instance.OnLoad;
                @GenerateBoard.started += instance.OnGenerateBoard;
                @GenerateBoard.performed += instance.OnGenerateBoard;
                @GenerateBoard.canceled += instance.OnGenerateBoard;
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
    public interface IMenuActions
    {
        void OnSave(InputAction.CallbackContext context);
        void OnLoad(InputAction.CallbackContext context);
        void OnGenerateBoard(InputAction.CallbackContext context);
    }
    public interface ITileActions
    {
        void OnClick(InputAction.CallbackContext context);
    }
}
