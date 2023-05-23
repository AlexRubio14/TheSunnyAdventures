using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLevel : MonoBehaviour
{
    playerController playerController;

    [SerializeField]
    private string scene;

    [SerializeField]
    GameObject tower;

    [SerializeField]
    GameObject door;

    [SerializeField]
    GameObject key;

    [SerializeField]
    GameObject lockKey;

    [SerializeField]
    float down;
    [SerializeField]
    private float velocity; // segundos en subir i bajar 

    private bool move_title;
    private bool directionUp;
    private Vector2 starterPos;
    private Vector2 enderPos;
    private float proces;


    private void Start()
    {
        starterPos = tower.transform.position;
        enderPos = tower.transform.position - new Vector3(0, down);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<playerController>();
    }

    private void Update()
    {
        if (move_title)
        {
            if (directionUp)
            {
                proces -= Time.deltaTime / velocity;
                tower.transform.position = Vector2.Lerp(starterPos, enderPos, proces);
                if (proces <= 0)
                {
                    move_title = false;
                }
            }
            else
            {
                proces += Time.deltaTime / velocity; // valor entre: 0 - 1
                tower.transform.position = Vector2.Lerp(starterPos, enderPos, proces); // interpolar: passar de punto A a B 
            }
            proces = Mathf.Clamp01(proces); // asegurar que proces no se pase de su valor 0 - 1 
        }
        DoorOpen();
        KeyAndLock(); 
    }


    private void DoorOpen()
    {
        if (scene == "Level 2" && !ChangeLevel.instance.canEnter)
        {
            Destroy(door);  
        }
    }

    private void KeyAndLock()
    {
        if (scene == "Level 1" && ChangeLevel.instance.canEnter)
        {
            Destroy(key);
            Destroy(lockKey);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            move_title = true;
            directionUp = false;

            if (playerController.GetAnimAttack())
            {
                if (scene == "Level 1")
                {
                    LevelManager.instance.currentLevel = scene;
                    FindObjectOfType<MusicController>().ChangeMusic(1);
                    SceneManager.LoadScene(scene);
                }
                if (scene == "Level 2" && ChangeLevel.instance.canEnter)
                {
                    LevelManager.instance.currentLevel = scene;
                    FindObjectOfType<MusicController>().ChangeMusic(1);
                    SceneManager.LoadScene(scene);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            directionUp = true;
        }
    }
   
}