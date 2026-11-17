using NUnit.Framework;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoseCheck : MonoBehaviour
{
    public DamageCalculator dc;

    private void LoseGame()
    {
        SceneManager.LoadScene(1);
    }
    private void WinGame()
    {
        SceneManager.LoadScene(2);
    }
    // Update is called once per frame
public void CheckWinLose()
    {
        Debug.Log("CHECKING");
        if (dc.playerPokemonList.Count <= 0)
        {
            Debug.Log("LOST THE GAME");
            LoseGame();
        }
        if (dc.enemyPokemonList.Count <= 0)
        {
            Debug.Log("Won THE GAME");
            WinGame();
        }
    }
}
