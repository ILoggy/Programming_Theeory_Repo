using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenuController : MonoBehaviour
{
    public Button.ButtonClickedEvent onClick;

    //Instruction MENU
    //easyButton
    //mediumButton
    //hardButton

    //MAIN MENU
    public Button exitGameButton;
    public Button toInstructionMenuButton;
    public Button backButton;
    public Button easyButton;

    public GameObject instructionMenu;
    public GameObject MainMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        toInstructionMenuButton.onClick.AddListener(ToInstructionMenu);
        exitGameButton.onClick.AddListener(GameExit);
        backButton.onClick.AddListener(ToMainMenu);
        easyButton.onClick.AddListener(EasyGameStart);
    }

    private void ToMainMenu()
    {
        instructionMenu.gameObject.SetActive(false);
        MainMenu.gameObject.SetActive(true);
    }

    private void ToInstructionMenu()
    {
        MainMenu.gameObject.SetActive(false);
        instructionMenu.gameObject.SetActive(true);
    }

    private void EasyGameStart()
    {
        SceneManager.LoadScene(1);
    }

    private void GameExit()
    {
        Debug.Log("Game is closing");
        Application.Quit();
    }
}
