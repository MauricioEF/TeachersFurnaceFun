using UnityEngine;

[DefaultExecutionOrder(-1000)]
public class InputRoot : MonoBehaviour
{
    //Class Declarations
    public static InputRoot Instance
    {
        get;
        private set;
    }
    public PlayerInputActions PlayerInput;


    // Lifecycle methods
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        PlayerInput = new PlayerInputActions();
        PlayerInput.Enable();
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
        PlayerInput?.Dispose();
    }
}
