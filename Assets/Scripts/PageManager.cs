using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    [SerializeField]
    private InputField inputNamePlayer1;
    [SerializeField]
    private InputField inputNamePlayer2;
    [SerializeField]
    private Text errorHintPlayer1;
    [SerializeField]
    private Text errorHintPlayer2;
    public string namePlayer1;
    public string namePlayer2;
    public Color colorPlayer1;
    public Color colorPlayer2;
    public bool isEscapeToExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isEscapeToExit)
            {
                Application.Quit();
            }
            else
            {
                BackToMenu();
            }
        }
    }

    public void StartGame()
    {

        if (isEscapeToExit)
        {
            if (inputNamePlayer1.text == "")
            {
                errorHintPlayer1.text = "Player name can't be empty!";
            }
            if (inputNamePlayer2.text == "")
            {
                errorHintPlayer2.text = "Player name can't be empty!";
            }

            if (inputNamePlayer1.text != "")
            {
                errorHintPlayer1.text = "";
                if (inputNamePlayer2.text != "")
                {
                    errorHintPlayer2.text = "";
                    if (!inputNamePlayer1.text.Equals(inputNamePlayer2.text))
                    {
                        errorHintPlayer1.text = "";
                        namePlayer1 = inputNamePlayer1.text;
                        PlayerPrefs.SetString("Player1", namePlayer1);
                        namePlayer2 = inputNamePlayer2.text;
                        PlayerPrefs.SetString("Player2", namePlayer2);
                        SceneManager.LoadScene("Main");
                    }
                    else
                    {
                        errorHintPlayer2.text = "Player name can't be same!";
                    }
                } 
            }
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
        
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void SetColorPlayer1(Image colorResource)
    {
        colorPlayer1 = colorResource.color;
        float rColorPlayer1 = colorPlayer1.r;
        float gColorPlayer1 = colorPlayer1.g;
        float bColorPlayer1 = colorPlayer1.b;
        PlayerPrefs.SetFloat("rColor1", rColorPlayer1);
        PlayerPrefs.SetFloat("gColor1", gColorPlayer1);
        PlayerPrefs.SetFloat("bColor1", bColorPlayer1);
    }

    public void SetColorPlayer2(Image colorResource)
    {
        colorPlayer2 = colorResource.color;
        float rColorPlayer2 = colorPlayer2.r;
        float gColorPlayer2 = colorPlayer2.g;
        float bColorPlayer2 = colorPlayer2.b;
        PlayerPrefs.SetFloat("rColor2", rColorPlayer2);
        PlayerPrefs.SetFloat("gColor2", gColorPlayer2);
        PlayerPrefs.SetFloat("bColor2", bColorPlayer2);

    }

    public void OpenURL(string url)
    {
        Application.OpenURL(url) ;
    }
}
