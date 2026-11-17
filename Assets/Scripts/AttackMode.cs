using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class AttackMode : MonoBehaviour
{
    /// <summary>
    /// In this script you run the attack quick time event. 
    /// It Awakes every time it runs where the bar moves unitl stoped by the player pressing SPACE
    /// When the player stops the bar it determines if the player hit the enemy and then sends the information to the damage calculator.
    /// </summary>
    /// 
    [Header("Prefabs")]
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject aimer;

    [SerializeField] private float speed = 10;
    private float randomSpeed = 1;
    [SerializeField] private DamageCalculator dc;
    [SerializeField] private InputActionAsset inputAction;
    public TextMeshProUGUI text;
    private string doesAttackHit = "";

    private InputAction enterAction;
    private bool stop = false;
    private int moveNum;
    public bool CalculateHit()
    {
        //Gets the distance from the players 'crosshair' and checks if it is closer to the target than a random number between .3-5
        //the way to determine if it hits can be changed later
        float distance = aimer.transform.position.x - target.transform.position.x;
        float randomDistance = Random.Range(0.3f, 5);
        if (distance < randomDistance && distance > (-randomDistance))
        {
            return true;
        }
        return false;
    }
    public void Awake()
    {
        //gets player actoins
        var playerActoinMap = inputAction.FindActionMap("Player");
        enterAction = playerActoinMap.FindAction("Enter");
        enterAction.Enable();
        //create a random speed added to the crosshair
        randomSpeed = Random.Range(-8, 10);
        //Resets the crosshairs position
        aimer.transform.position = new Vector2(-10, aimer.transform.position.y);
        stop = false;
    }



    // Update is called once per frame
    void Update()
    {
        //stops the crosshair from moveing infinitly
        if (aimer.transform.position.x > 10)
        {
            stop = true;
            text.text = "Miss!";
        }
        //if the crosshair is moveing
        if (stop == false)
        {
            //move the crosshair
            aimer.transform.position = new Vector2(aimer.transform.position.x + (Time.deltaTime * (speed + randomSpeed)), aimer.transform.position.y);
            //if player inputs
            if (enterAction.WasPressedThisFrame())
            {
                TryHit();
            }
        }

        //attemping to make thing work automaticaly if u go too far
        void TryHit()
        {
            //stop the crosshair
            stop = true;
            //check if player hits enemy
            if (CalculateHit())
            {
                //sen info to damage calculator
                dc.Damage(moveNum, dc.playerPokemonList, dc.enemyPokemonList);
            }
            else
            {
                //tell the player they missed
                Debug.Log("YOU MISSED KID!");
                text.text = "Miss!";
            }
            //one line of code
            //now player know when they miss or hit
            text.gameObject.SetActive(true);
        }
        
    }
    public void SendInfo(int moveNumber)
    {
        //used to save info to give to damage calculator later
        moveNum = moveNumber;
    }
}
