using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menuManager : MonoBehaviour
{
    public void Jugar()
    {
        if (StaminaManager.Instance.TryUseStamina())
        {
            SceneManager.LoadScene("inGame");
        }
        else
        {
            Debug.Log("No tenés stamina para jugar");
            // posible pop up de que no se puede jugar
        }
    }

    public void Perder()
    {
        SceneManager.LoadScene("Perder");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Tienda()
    {
        SceneManager.LoadScene("Tienda");
    }

    public void Opciones()
    {
        SceneManager.LoadScene("Opciones");
    }
}
