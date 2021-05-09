using UnityEngine;
using UnityEngine.UI;

public class GameGenHandler : MonoBehaviour
{
    public static GameGenHandler Instance { get; set; }

    public GameObject kills;
    private Text UIKills;
    public GameObject m4;
    private Text UIM4;
    public GameObject plr;
    private Text UIplr;
    public GameObject MiniGun;
    private Text UIMiniGun;
    public GameObject FlameThrower;
    private Text UIFlamethrower;

    public int M4A1Ammo = 0;
    public int PLRAmmo = 0;
    public int MiniGunAmmo = 0;
    public int FlameThrowerAmmo = 0;
    public int killsCount = 0;
    public int roundsFired= 0;
    public int roundsHit = 0;


    public void Awake()
    {
        Instance = this;
        UIKills = kills.GetComponent<Text>();
        UIM4 = m4.GetComponent<Text>();
        UIplr = plr.GetComponent<Text>();
        UIMiniGun = MiniGun.GetComponent<Text>();
        UIFlamethrower = FlameThrower.GetComponent<Text>();
    }
    public void AddKills()
    {
        killsCount++;
    }

    public void addhits()
    {
        roundsHit++;
    }

    public void addFired()
    {
        roundsFired++;
    }

    public void Update()
    {
        UIKills.text = killsCount + "";
        UIM4.text = M4A1Ammo + "";
        UIplr.text = PLRAmmo + "";
        UIMiniGun.text = MiniGunAmmo + "";
        UIFlamethrower.text = FlameThrowerAmmo + "";
    }

    public int SetM4Ammo() { return M4A1Ammo; }
    public int SetPLRAmmo() { return PLRAmmo; }
    public int SetMINIGUNAmmo() { return MiniGunAmmo; }
    public int SetFLAMEAmmo() { return FlameThrowerAmmo; }
    public int NumKills() { return killsCount; }
    public int NumRounds() { return roundsFired; }
    public int NumHits() { return roundsHit; }

    public void RAM4(bool pu, int _ammo) 
    {
        if (pu == true)
        {
            M4A1Ammo += _ammo;
        }
        else
        {
            M4A1Ammo = _ammo;
        }
    }
    public void RAPLR(bool pu, int _ammo)
    {
        if (pu == true)
        {
            PLRAmmo += _ammo;
        }
        else
        {
            PLRAmmo = _ammo;
        }
    }
    public void RAMNGN(bool pu, int _ammo) 
    { 
        if (pu == true)
        {
            MiniGunAmmo += _ammo;
        }
        else
        {
            MiniGunAmmo--;
            if (MiniGunAmmo < 0) { MiniGunAmmo = 0; }
        }
    }
    public void RAFT(bool pu, int _ammo) 
    { 
        if (pu == true)
        {
            FlameThrowerAmmo += _ammo;
        }
        else
        {
            FlameThrowerAmmo--;
            if (FlameThrowerAmmo < 0) { FlameThrowerAmmo = 0; }
        }
    }
}
