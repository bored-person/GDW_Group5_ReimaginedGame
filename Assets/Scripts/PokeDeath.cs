using UnityEngine;

public class PokeDeath : MonoBehaviour
{
    private Pokemon deadPokemon;
    
    // get the deadpokemon 
    public void GetPokemon(Pokemon pokemon)
    {
        //play the death animation
        Debug.Log($"{pokemon} Died!");
        deadPokemon = pokemon;
        deadPokemon.rb.gravityScale = 2;
        deadPokemon.rb.linearVelocity = new Vector2(Random.Range(-10, 11), 10);
    }

    void Update()
    {
        if (deadPokemon != null)
        {
            if (deadPokemon.transform.position.y < -5)
            {
                Destroy(deadPokemon.gameObject);
                Destroy(deadPokemon);
            }
        }


    }
}
