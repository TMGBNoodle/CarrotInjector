using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput input;

    public InputActionAsset playerActions;

    public InputAction move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = GetComponent<PlayerInput>();
        playerActions = input.actions;

    }

    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
