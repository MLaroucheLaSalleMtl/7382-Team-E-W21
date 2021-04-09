using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM_Script : MonoBehaviour
{
    [SerializeField] private AudioSource DefaultMusic;
    [SerializeField] private AudioSource EngagedMusic;
    [SerializeField] private AudioSource BossMusic;    

    private bool inBossRoom;
    private bool enemiesAlive;




    private void Start() 
    {
        DefaultMusic.Stop();
        EngagedMusic.Stop();
        BossMusic.Stop();
        enemiesAlive = true;
    }
    void _updateMusic()
    {        
        if(!inBossRoom && !enemiesAlive && !DefaultMusic.isPlaying)
        {
            DefaultMusic.Play();
            EngagedMusic.Stop();
            BossMusic.Stop();
        }
        else if(!inBossRoom && enemiesAlive && !EngagedMusic.isPlaying)
        {
            DefaultMusic.Stop();
            EngagedMusic.Play();
            BossMusic.Stop();
        }
        else if (inBossRoom && !BossMusic.isPlaying)
        {
            DefaultMusic.Stop();
            EngagedMusic.Stop();
            BossMusic.Play();           
        }
    }

    public void SetBoss(bool b)
    {
        inBossRoom = b;
    }

    public void SetEnemies(bool b)
    {
        enemiesAlive = b;
    }
    // Update is called once per frame
    void Update()
    {
        enemiesAlive = GameManager.instance.enemiesMusic;
        _updateMusic();
    }
}
