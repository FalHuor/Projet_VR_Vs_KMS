using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void setTeam(string team)
    {
        this.team = team;
        Debug.Log($"This player come from {team}");
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
            if (Area.GetComponent<vr_vs_kms.ContaminationArea>().CapturedBy == "Scientist")
            {
                ScientistContaminationArea += 1;
            }
            else if (Area.GetComponent<vr_vs_kms.ContaminationArea>().CapturedBy == "Virus")
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
        CanvasEndGame.SetActive(true);
        //AudioListener.pause = true;
        if (this.team == team)
        {
            //Debug.Log(Resources.Load<Sprite>("Assets/VR-Vs-KMS/Textures/Victory"));
            EndGameImage.sprite = EndGameImageVictory;
            BackGroundImage.color = victoryColor;
            EndGameText.text = "You have won, congratulations";
            TypeOfVictoryText.text = typeOfVictory;
            WinAudio.Play();
        } 
        else
        {
            EndGameImage.sprite = EndGameImageDefeat;
            BackGroundImage.color = defeatColor;
            EndGameText.text = "You have Loose ... Too bad ... try again";
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
    }

    IEnumerator WaitSomeSecond(int timer)
    {
        for (int i = timer; i > 0; i--)
        {
            TextTimer.text = $"Next Game Start in {i} s";
            yield return new WaitForSecondsRealtime(1);
        }
        TextTimer.text = $"Next Game Start in 0 s";
        ResetGame();
    }
}
