using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Pokemon : MonoBehaviour
{
    void AddToListMoves(GameObject item)
    {
        if (item != null)
        {
            moveList.Add(item);
        }
    }
    void AddToListWeak(string item)
    {
        if (item != null)
        {
            weakToList.Add(item);
        }
    }
    void AddToListResist(string item)
    {
        if (item != null)
        {
            ressistantToList.Add(item);
        }
    }

    public GameObject selfObject;
    [SerializeField] private string elementType1;
    [SerializeField] private string elementType2;
    [SerializeField] private float maxHealth = 100;
    [SerializeField] public float currentHealth;

    [SerializeField] private GameObject healthBar;
    private float desiredHealthWidth;
    [Header("MOVES")]
    [SerializeField] public GameObject move1;
    [SerializeField] public GameObject move2;
    [SerializeField] public GameObject move3;
    [SerializeField] public GameObject move4;

    public List<GameObject> moveList = new List<GameObject>();
    private SpriteRenderer spriteRenderer;


    [Header("WEAK TO")]
    [SerializeField] private string weakToType1;
    [SerializeField] private string weakToType2;
    [SerializeField] private string weakToType3;
    [SerializeField] private string weakToType4;
    public List<string> weakToList = new List<string>();
    [Header("RESISTANT TO")]
    [SerializeField] private string resistantToType1;
    [SerializeField] private string resistantToType2;
    [SerializeField] private string resistantToType3;
    [SerializeField] private string resistantToType4;
    public List<string> ressistantToList = new List<string>();
    public Rigidbody2D rb;
    private GameObject healthShown;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        healthShown = Instantiate(healthBar, new Vector3(transform.position.x + (healthBar.transform.localScale.x * ((float)currentHealth / (float)maxHealth) / 2) - 0.5f, transform.position.y + 1, transform.position.z), Quaternion.identity);

        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        //adds all the moves to a list so that we can acssess them later, 
        //this is how i would do it
        //if you have a better way feel free to do it 
        AddToListMoves(move1);
        AddToListMoves(move2);
        AddToListMoves(move3);
        AddToListMoves(move4);
        //add to a list to check later when being attacked
        AddToListWeak(weakToType1);
        AddToListWeak(weakToType2);
        AddToListWeak(weakToType3);
        AddToListWeak(weakToType4);
        //add to a list to check later when being attacked
        AddToListResist(resistantToType1);
        AddToListResist(resistantToType2);
        AddToListResist(resistantToType3);
        AddToListResist(resistantToType4);

    }
    void FixedUpdate()
    {
        // this is the code for creating the health bar, 
        // i did it by creating the health bar with current health and then deleting it 
        // i thought this would be the easiest way but it is definetly not 
        // probably should have just done one box that shrinks with health but it works

        if (currentHealth > 0)
        {
            healthShown.transform.position = new Vector3(transform.position.x + (healthBar.transform.localScale.x * ((float)currentHealth / (float)maxHealth)/2)-0.5f, transform.position.y + 1, transform.position.z);
            healthShown.transform.localScale = new Vector3(healthBar.transform.localScale.x*((float)currentHealth/(float)maxHealth), healthShown.transform.localScale.y, healthShown.transform.localScale.z);
            //Destroy(healthShown, 0.05f);
        }
        else
        {
            Destroy(healthShown);
        }
    }
}
