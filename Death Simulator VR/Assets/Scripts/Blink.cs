using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour {
    private Animator topLidAnimator;
    private Animator bottomLidAnimator;

    private AudioSource[] rainSounds;
    private ParticleSystem[] rainSystems;

    private GameObject bedroom;
    private GameObject forest;
    private GameObject deskRoom;
    private GameObject fly;

    [SerializeField]
    private bool needsToBlink;
    [SerializeField]
    private bool closeEyes;
    private bool activateStorm;
    private bool disableStorm;

    [SerializeField]
    private float timer;

    void Start() {
        // set animators
        topLidAnimator = GameObject.Find("Top Lid").GetComponent<Animator>();
        bottomLidAnimator = GameObject.Find("Bottom Lid").GetComponent<Animator>();

        // set rain objects
        rainSounds = GameObject.Find("RainPrefab").GetComponents<AudioSource>();
        rainSystems = FindObjectsOfType<ParticleSystem>();

        // set main objects
        bedroom = GameObject.Find("Bedroom");
        forest = GameObject.Find("Forest");
        deskRoom = GameObject.Find("DeskRoom");
        fly = GameObject.Find("Fly");

        // set bools
        needsToBlink = false;

        activateStorm = false;
        disableStorm = true;

        // set start scene
        bedroom.SetActive(true);
        forest.SetActive(false);
        deskRoom.SetActive(false);
        fly.SetActive(false);
    }

    void Update() {
        // keep time
        timer += Time.deltaTime;

        // keep eyes updated
        updatePlayerEyes();

        // first blink - room to storm
        if (timer >= 10.0f && timer <= 11.0f) needsToBlink = true;
        else needsToBlink = false;
        if (timer >= 11.5f && timer <= 12.0f) {
            activateStorm = true;
            bedroom.SetActive(false);
            forest.SetActive(true);
        }

        // second blink - storm to room
        if (timer >= 20.0f && timer <= 21.0f) needsToBlink = true;
        if (timer >= 21.5f && timer <= 22.0f) {
            needsToBlink = false;
            disableStorm = true;
            bedroom.SetActive(true);
            forest.SetActive(false);
        }


        // third blink - room to desk w/ will
        if (timer >= 30.0f && timer <= 31.0f) needsToBlink = true;
        if (timer >= 31.5f && timer <= 32.0f) {
            bedroom.SetActive(false);
            deskRoom.SetActive(true);
            needsToBlink = false;
            
        }

        // fourth blink - will to room w/ fly
        if (timer >= 40.0f && timer <= 41.0f) needsToBlink = true;
        if (timer >= 41.5f && timer <= 42.0f) {
            needsToBlink = false;
            deskRoom.SetActive(false);
            bedroom.SetActive(true);
            fly.SetActive(true);
        }

        // eyes close
        if (timer >= 50.0f && timer <= 51.0f) {
            closeEyes = true;
        }
        if (timer >= 54.0f && timer <= 56.0f) {
            fly.SetActive(false);
        }

        // keep scene updated
        updateStorm();
    }

    // storm toggle
    void updateStorm() {
        if (activateStorm) {
            for (int i = 0; i < rainSounds.Length; i++) {
                rainSounds[i].Play();
            }
            for (int i = 0; i < rainSystems.Length; i++) {
                rainSystems[i].Play();
            }
            activateStorm = false;
        }
        if (disableStorm) {
            for (int i = 0; i < rainSounds.Length; i++) {
                rainSounds[i].Stop();
            }
            for (int i = 0; i < rainSystems.Length; i++) {
                rainSystems[i].Stop();
            }
            disableStorm = false;
        }
    }

    
    void updatePlayerEyes() {
        // eyelid animation control
        topLidAnimator.SetBool("needsToBlink", needsToBlink);
        bottomLidAnimator.SetBool("needsToBlink", needsToBlink);

        // close player eyes forever
        topLidAnimator.SetBool("closeEyes", closeEyes);
        bottomLidAnimator.SetBool("closeEyes", closeEyes);
    }

}
