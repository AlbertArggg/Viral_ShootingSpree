using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    bool done = false;
    public GameObject GenHan;
    public GameObject kills;
    public GameObject DNA;
    public GameObject roundsFired;
    public GameObject roundsHit;

    public string[] infections = new string [20];

    public Text ks;
    public Text dna;
    public Text rounds;
    public Text rH;
    public Text bac1;
    public Text bac2;
    public Text bac3;
    public Text bac4;

    public int ksInt;
    public int dnaInt;
    public int roundsInt;
    public int rHInt;
    public float acc;
    public int accInt;


    private void Awake()
    {
        setNames();
        ks = kills.GetComponent<Text>();
        dna = DNA.GetComponent<Text>();
        rounds = roundsFired.GetComponent<Text>();
        rH = roundsHit.GetComponent<Text>();
    }

    private void setNames()
    {
        infections[0] = "meningitis";
        infections[1] = "Salmonella";
        infections[2] = "E. coli";
        infections[3] = "gonorrhea";
        infections[4] = "chlamydia";
        infections[5] = "syphilis";
        infections[6] = "Clostridium difficile";
        infections[7] = "tuberculosis";
        infections[8] = "anthrax";
        infections[9] = "cholera";
        infections[10] = "influenza";
        infections[11] = "measles";
        infections[12] = "polio";
        infections[13] = "papillomavirus";
        infections[14] = "hepatitis A";
        infections[15] = "west Nile Virus";
        infections[16] = "covid";
        infections[17] = "rabies";
        infections[18] = "ebola";
        infections[19] = "Cryptococcus";

        bac1.text = infections[Random.Range(0, 4)];
        bac2.text = infections[Random.Range(5, 9)];
        bac3.text = infections[Random.Range(10, 14)];
        bac4.text = infections[Random.Range(14, 19)];
    }

    public void calculations()
    {
        if (done == false)
        {
            ksInt = GenHan.GetComponent<GameGenHandler>().NumKills();
            dnaInt = Random.Range(0, ksInt) * 3;
            roundsInt = GenHan.GetComponent<GameGenHandler>().NumRounds();
            rHInt = GenHan.GetComponent<GameGenHandler>().NumHits();
            rHInt = (rHInt / 2);
            float rhF = (float)rHInt;
            float rdF = (float)roundsInt;
            acc = (rhF/rdF)*100;
            accInt = (int)acc;
            if (accInt > 100 || accInt < 0)
            {
                accInt = 100;
            }
            done = true;
        }

    }

    public void Update()
    {
        ks.text = ksInt+ "";
        dna.text = dnaInt + "";
        rounds.text = roundsInt + "";
        rH.text = accInt + "%";
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Backspace))
        {
            FindObjectOfType<Settings>().GetComponent<Settings>().inGameBool(false);
            Application.LoadLevel(0);
        }
    }
}
