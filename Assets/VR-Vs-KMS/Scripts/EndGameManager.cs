using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using vr_vs_kms;

public class EndGameManager : MonoBehaviour
{

    private List<GameObject> contaminationAreas = new List<GameObject>();
    public Transform ContaminationTm;
    public int ScoreScientistTeam;
    public int ScoreVirusTeam;
    private string typeOfVictory;
    private string victoryString;
    private string team;
    public Text EndGameText;
    public Text TypeOfVictoryText;
    public Text TextTimer;
    public Image BackGroundImage;
    public Image EndGameImage;
    public GameObject CanvasEndGame;

    private GameObject player;

    public Sprite EndGameImageVictory;
    public Sprite EndGameImageDefeat;

    public AudioSource WinAudio;
    public AudioSource LooseAudio;

    public int TimeBeforeReplay;

    private Color victoryColor = new Color32(50, 150, 255, 230);
    private Color defeatColor = new Color32(255, 50, 50, 230);

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform ChildContamination in ContaminationTm)
        {
            contaminationAreas.Add(ChildContamination.gameObject);
        }
        Debug.Log($"Nombre de zone = {contaminationAreas.Count}");
        ScoreVirusTeam = 0;
        ScoreScientistTeam = 0;
        Debug.Log("Color : " + victoryColor);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setUserGameObject(GameObject user)
    {
        player = user;
    }

    public void setTeam(string team)
    {
        this.team = team;
        Debug.Log($"This player come from {team}");

        if (team == "Virus")
        {
            Debug.Log(player.name);
            Debug.Log("On essai de changer les variables");
            CanvasEndGame = player.transform.Find("UICamera").transform.Find("CanvasEndGame").gameObject;
            Debug.Log(CanvasEndGame.gameObject.tag);
            GameObject BackGroundGo = CanvasEndGame.transform.Find("BackGroundImage").gameObject;
            BackGroundImage = BackGroundGo.GetComponent<Image>();
            EndGameText = BackGroundGo.transform.Find("VictoryText").gameObject.GetComponent<Text>();
            TypeOfVictoryText = BackGroundGo.transform.Find("TypeOfVictoryText").gameObject.GetComponent<Text>();
            TextTimer = BackGroundGo.transform.Find("TextTimer").gameObject.GetComponent<Text>();
            EndGameImage = BackGroundGo.transform.Find("ImageStatus").gameObject.GetComponent<Image>();
        }
    }

    public void playerDie(string team)
    {
        Debug.Log($"A player from {team} team ha been slain !");
        if (team == "Virus")
        {
            ScoreScientistTeam++;
            if (ScoreScientistTeam >= AppConfig.Inst.NbContaminatedPlayerToVictory)
            {
                LauchVictory("Scientist", "All viruses have been eliminated");
            }
        }
        else if (team == "Scientist")
        {
            ScoreVirusTeam++;
            if (ScoreVirusTeam >= AppConfig.Inst.NbContaminatedPlayerToVictory)
            {
                LauchVictory("Virus", "All hostiles have been contaminated");
            }
        }
        Debug.Log($"Scientist : {ScoreScientistTeam} | Virus : {ScoreVirusTeam}");
    }

    public void checkContaminationArea()
    {
        int ScientistContaminationArea = 0;
        int VirusContaminationArea = 0;
        int NeutralArea = 0;
        foreach (GameObject Area in contaminationAreas)
        {
            if (Area.GetComponent<ContaminationArea>().CapturedBy == "Scientist")
            {
                ScientistContaminationArea += 1;
            }
            else if (Area.GetComponent<ContaminationArea>().CapturedBy == "Virus")
            {
                VirusContaminationArea += 1;
            }
            else
            {
                NeutralArea += 1;
            }
        }

        Debug.Log($"Scientist Zone = {ScientistContaminationArea} | Virus Zone = {VirusContaminationArea} | Neutral Zone = {NeutralArea}");

        if (NeutralArea == 0 && ScientistContaminationArea == 0)
        {
            LauchVictory("Virus", "All contaminations zone have been taken");
        }
        else if (NeutralArea == 0 && VirusContaminationArea == 0)
        {
            LauchVictory("Scientist", "All contaminations zone have been taken");
        }

    }

    private void LauchVictory(string team, string typeOfVictory)
    {
        Debug.Log($"{team} win the game with Contamination Area");
        Debug.Log(CanvasEndGame.activeInHierarchy);
        CanvasEndGame.SetActive(true);
        Debug.Log(CanvasEndGame.activeInHierarchy);
        //AudioListener.pause = true;
        if (this.team == team)
        {
            EndGameImage.sprite = EndGameImageVictory;
            BackGroundImage.color = victoryColor;
            EndGameText.text = "You won, congratulations";
            TypeOfVictoryText.text = typeOfVictory;
            WinAudio.Play();
        } 
        else
        {
            EndGameImage.sprite = EndGameImageDefeat;
            BackGroundImage.color = defeatColor;
            EndGameText.text = "You Lost ... Too bad ... try again";
            TypeOfVictoryText.text = typeOfVictory;
            LooseAudio.Play();
        }
        Time.timeScale = 0;
        StartCoroutine(WaitSomeSecond(TimeBeforeReplay));
        //AudioListener
    }

    public void ResetGame()
    {
        Debug.Log("GAME RESET");
        ScoreVirusTeam = 0;
        ScoreScientistTeam = 0;

        player.GetComponent<UserManager>().ResetPlayer();

        foreach (GameObject Area in contaminationAreas)
        {
            Area.GetComponent<ContaminationArea>().resetArea();
        }

        CanvasEndGame.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator WaitSomeSecond(int timer)
    {
        TextTimer.text = "";
        yield return new WaitForSecondsRealtime(5);

        for (int i = timer; i > 0; i--)
        {
            TextTimer.text = $"Next Game Start in {i} s";
            yield return new WaitForSecondsRealtime(1);
        }
        TextTimer.text = $"Next Game Start in 0 s";
        yield return new WaitForSecondsRealtime(1);
        ResetGame();
    }


}
