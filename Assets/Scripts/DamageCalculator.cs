using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DamageCalculator : MonoBehaviour
{
    [Header("PlayerPokemon")]
    //get the players pokemon
    [SerializeField] private Pokemon playerPokemon1;
    [SerializeField] private Pokemon playerPokemon2;
    [SerializeField] private Pokemon playerPokemon3;
    [Header("EnemyPokemon")]
    // get the enemy pokemon
    [SerializeField] private Pokemon enemyPokemon1;
    [SerializeField] private Pokemon enemyPokemon2;
    [SerializeField] private Pokemon enemyPokemon3;
    [Header("Spawn")]
    // Choose where the active pokemon will be
    [SerializeField] private GameObject spawn1;
    [SerializeField] private GameObject spawn2;
    // create a list of player pokemon and enemy pokemon
    public List<Pokemon> playerPokemonList = new List<Pokemon>();
    public List<Pokemon> enemyPokemonList = new List<Pokemon>();
    private bool playersTurn = true;
    
    //text the displays the players damage
    public TextMeshProUGUI damageText;
    //text the displays the players moves
    public TextMeshProUGUI textMove1;
    public TextMeshProUGUI textMove2;
    public TextMeshProUGUI textMove3;
    public TextMeshProUGUI textMove4;
    public TextMeshProUGUI pokemonName1;
    public TextMeshProUGUI pokemonName2;
    public PokeDeath deathAnimation;


    public void ChangePokemon(int pokemonInList)
    {
        Pokemon newFirstPokemon = playerPokemonList[pokemonInList];
        List<Pokemon> newList = new List<Pokemon>();
        newList.Add(newFirstPokemon);
        foreach (Pokemon p in playerPokemonList)
        {
            if (p != newFirstPokemon)
            {
                p.transform.position = new Vector2(-10,-10);
                newList.Add(p);
            }
        }
        playerPokemonList = newList;
        pokemonName1.text = playerPokemonList[1].name;
        pokemonName2.text = playerPokemonList[2].name;
    }

    public void EnemyDamage()
    {
        Damage(Random.Range(0,4),enemyPokemonList,playerPokemonList);
    }
    public void Damage(int moveInList, List<Pokemon> listOfPokemon, List<Pokemon> otherListPokemon)
    {
        //This Function is to apply damage from one pokemon to another
        // you input the move,list of pokemon that is attacking, and list of pokemon that are being attavked

        //Get the move from the pokemon 
        //GameObject move = listOfPokemon[moveInList].move1;
        Pokemon poke = listOfPokemon[0];
        GameObject move = poke.moveList[moveInList];
        moveBase moveScript = move.GetComponent<moveBase>();
        //get the enemy pokemon
        Pokemon enemy = otherListPokemon[0];
        float damageMultiply = 1f;
        string damagestring = "";
        string deathstring = "";

        Debug.Log(enemy);
        Debug.Log(move);
        Debug.Log(poke);
        //check if the move is strong or weak vs the enemy
        foreach (string type in enemy.ressistantToList)
        {
            if (type == moveScript.moveType)
            {
                damageMultiply = damageMultiply/2;
                damagestring = "Not Effective! ";
            }
        }
        foreach (string type in enemy.weakToList)
        {
            if (type == moveScript.moveType)
            {
                damageMultiply = damageMultiply*2;
                damagestring = "Super Effective! ";
            }
        }

        // do the damage
        otherListPokemon[0].currentHealth -= (moveScript.moveDamage*damageMultiply);
        // if the payer dies then play the death animation 
        if (otherListPokemon[0].currentHealth <= 0)
        {
            Debug.Log($"{otherListPokemon[0]} DIED!!");
            deathAnimation.GetPokemon(otherListPokemon[0]);
            deathstring = $"{otherListPokemon[0].name} Died!";
            otherListPokemon.Remove(otherListPokemon[0]);
            //Debug.Log(otherListPokemon);
        }

        //change the text to dusplay information
        damageText.text = $" {moveScript.moveName} Hit For {moveScript.moveDamage * damageMultiply}! : {damagestring}\n{deathstring}";



    }
    void Awake()
    {
        // put the pokemon in a list
        playerPokemonList.Add(playerPokemon1);
        playerPokemonList.Add(playerPokemon2);
        playerPokemonList.Add(playerPokemon3);

        enemyPokemonList.Add(enemyPokemon1);
        enemyPokemonList.Add(enemyPokemon2);
        enemyPokemonList.Add(enemyPokemon3);

        pokemonName1.text = playerPokemonList[1].name;
        pokemonName2.text = playerPokemonList[2].name;
    }

    // Update is called once per frame
    void Update()
    {
        // get the moves to display their names
        if (playerPokemonList.Count > 0)
        {
            Pokemon playerPokemon = playerPokemonList[0];
            moveBase moveBase1 = playerPokemon.move1.GetComponent<moveBase>();
            textMove1.text = moveBase1.moveName;
            moveBase moveBase2 = playerPokemon.move2.GetComponent<moveBase>();
            textMove2.text = moveBase2.moveName;
            moveBase moveBase3 = playerPokemon.move3.GetComponent<moveBase>();
            textMove3.text = moveBase3.moveName;
            moveBase moveBase4 = playerPokemon.move4.GetComponent<moveBase>();
            textMove4.text = moveBase4.moveName;
        }



        if (playerPokemonList.Count>0)
        {
            playerPokemonList[0].transform.position = spawn1.transform.position;
        }
        if (enemyPokemonList.Count > 0)
        {
            enemyPokemonList[0].transform.position = spawn2.transform.position;
        }
        //move the pokemon to the screen
        //if (enemyPokemonList.Count != 0)
        //{
        //    //enemyPokemonList[0].transform.position = spawn2.transform.position;
        //    playerPokemonList[0].transform.position = spawn1.transform.position;
        //    //if the pokemon dies
        //    if (enemyPokemonList[0].currentHealth <= 0)
        //    {
        //        //remove pokemon from list and kill them
        //        Pokemon pokemon = enemyPokemonList[0];
        //        GameObject pokemonObject = pokemon.GetComponent<GameObject>();
        //        enemyPokemonList.Remove(enemyPokemonList[0]);
        //        //Destroy(pokemonObject);
        //    }
        //    else
        //    {
        //        enemyPokemonList[0].transform.position = spawn2.transform.position;
        //    }


        //}
        //else
        //{
        //    Debug.Log("YOU WIN!");
        //}

    }
}
