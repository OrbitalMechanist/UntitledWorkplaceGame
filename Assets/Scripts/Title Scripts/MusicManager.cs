using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static void beginMusic() {
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().PlayMusic();
    }

    public static void stopMusic() {
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().StopMusic();
    }

    public static void fadeMusic() {
        GameObject.FindGameObjectWithTag("Music").GetComponent<MusicClass>().FadeOutMusic();
    }
}
