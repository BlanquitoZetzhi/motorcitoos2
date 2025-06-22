using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class menuManager : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("inGame");
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
