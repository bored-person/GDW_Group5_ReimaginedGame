using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Inputs
    [SerializeField] private InputActionAsset inputAction;
    private Vector2 moveInput;
    private InputAction moveAction;
    private InputAction enterAction;
    //
    [SerializeField] private GameObject selfObject;
    [SerializeField] private Pokemon selfPokemon;



    
}
