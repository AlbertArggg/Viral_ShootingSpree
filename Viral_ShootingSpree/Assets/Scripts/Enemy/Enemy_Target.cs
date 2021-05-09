using UnityEngine;
using UnityEngine.UI;

public class Enemy_Target : MonoBehaviour
{
    public float health = 100f;
    public Rigidbody enemyRB;
    public AudioSource sdfx;
    public AudioClip takeDam;
    public AudioClip death;
    public Animator anim;
    public bool dead;
    public GameObject GH;
    public GameObject spawner;
    

    private void Awake()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        GH = GameObject.FindGameObjectWithTag("GH");
        Debug.Log(GH.name);
        enemyRB = this.gameObject.GetComponent<Rigidbody>();
        enemyRB.isKinematic = false;
    }
    public void TakeDamage(float _damage)
    {
        health -= _damage;

        if (health <= 0 && dead == false)
        {
            anim.SetTrigger("Dead");
            sdfx.PlayOneShot(death);
            die();
        }

        else
        {
            int i = Random.Range(1, 5);
            if (i == 1 && dead == false)
            {
                sdfx.PlayOneShot(takeDam);
            }
        }
    }

    public void die()
    {
        if (dead == false)
        {
            spawner.GetComponent<WaveSystem>().KillEnemies();
            GH.GetComponent<GameGenHandler>().AddKills();
            if (GH.GetComponent<GameGenHandler>() != null)
            {
                Debug.Log("Not Null");
            }
            this.gameObject.GetComponent<EnemyBehaviour>().DeadBitch();
            Destroy(gameObject, 5f);
            dead = true;
        } 
    }
}
