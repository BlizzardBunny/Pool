using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform ballsParent;
    public GameObject dialogueBox, lvlSelectPnl, confirmPnl;
    public TMPro.TextMeshProUGUI instructions, levelLbl, movesLbl, scoreLbl, dialogueLbl, retryContLbl, confirmLbl;
    public Button retryContBtn, lvlSelectBtn, mainMenuBtn, closeDialogueBtn, closeLvlSelectBtn, yesBtn, noBtn, menuBtn;
    public Button[] lvlSelectBtns;

    private int scoreGoal, moveCap;

    public static int lvl = 1;
    public static int ballsIn = 0;
    public static int moves = 0;
    public static bool hasFallen = false; //cue ball in hole

    // Start is called before the first frame update
    void Start()
    {
        #region Add Listeners
        retryContBtn.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        mainMenuBtn.onClick.AddListener(GoToMainMenu);
        menuBtn.onClick.AddListener(() => dialogueBox.SetActive(true));
        closeDialogueBtn.onClick.AddListener(()=>dialogueBox.SetActive(false));
        closeLvlSelectBtn.onClick.AddListener(() => lvlSelectPnl.SetActive(false));
        lvlSelectBtn.onClick.AddListener(()=>lvlSelectPnl.SetActive(true));
        noBtn.onClick.AddListener(()=>confirmPnl.SetActive(false));
        #endregion        

        Init();
    }

    private void Init()
    {
        confirmPnl.SetActive(false);
        lvlSelectPnl.SetActive(false);
        dialogueBox.SetActive(false);

        if (lvl == 1)
        {
            scoreGoal = 3;
            moveCap = 5;

            instructions.text =
                "Use only " + moveCap + " moves.\n" +
                "Pocket " + scoreGoal + " balls.";
        }
        else if (lvl == 2)
        {
            scoreGoal = 3;
            moveCap = 3;

            instructions.text =
                "Use only " + moveCap + " moves.\n" +
                "Pocket " + scoreGoal + " balls.";
        }
        else if (lvl == 3)
        {
            scoreGoal = 3;
            moveCap = 5;

            instructions.text =
                "Use only " + moveCap + " moves.\n" +
                "Pocket " + scoreGoal + " balls.\n" +
                "Do not pocket the cue ball.";
        }
        else if (lvl == 4)
        {
            scoreGoal = 3;
            moveCap = 3;

            instructions.text =
                "Use only " + moveCap + " moves.\n" +
                "Pocket " + scoreGoal + " balls.\n" +
                "Do not pocket the cue ball.";
        }
        else if (lvl == 5)
        {
            scoreGoal = 5;
            moveCap = 5;

            instructions.text =
                "Use only " + moveCap + " moves.\n" +
                "Pocket " + scoreGoal + " balls.\n" +
                "Do not pocket the cue ball.";
        }
        else
        {
            lvl = 1;
            ResolveStage();
            SceneManager.LoadScene("MainMenu");
        }

        levelLbl.text = "LEVEL " + lvl;
    }

    private void Win()
    {
        lvl++;
        dialogueLbl.text = "STAGE COMPLETE";
        retryContLbl.text = "NEXT LEVEL";
        ResolveStage();
    }

    private void Lose()
    {
        dialogueLbl.text = "STAGE LOST";
        retryContLbl.text = "RETRY LEVEL";
        ResolveStage();
    }

    private void ResolveStage()
    {
        hasFallen = false;
        CameraSwitch.isPointA = true;
        closeDialogueBtn.interactable = false;
        dialogueBox.SetActive(true);
        ballsIn = 0;
        moves = 0;
    }

    private void GoToMainMenu()
    {
        confirmLbl.text = "Return to Menu?";
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("MainMenu");
        });
        confirmPnl.SetActive(true);
    }

    public void ChooseLvl(int level)
    {
        confirmLbl.text = "Start a new level?";
        yesBtn.onClick.RemoveAllListeners();
        yesBtn.onClick.AddListener(() =>
        {
            lvl = level;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            ResolveStage();
        });
        confirmPnl.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Screen.SetResolution(Screen.width, ((Screen.width * 16) / 9), false);
        if (Screen.fullScreenMode != FullScreenMode.Windowed)
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        if (!dialogueBox.activeSelf)
        {
            movesLbl.text = "Moves: " + moves;
            scoreLbl.text = "Score: " + ballsIn;

            if (ballsIn >= scoreGoal)
            {
                Win();
            }

            if (lvl >= 3 && hasFallen)
            {
                Lose();
            }

            if (moves > moveCap)
            {
                Lose();
            }
        }
    }
}
