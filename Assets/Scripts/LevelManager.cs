using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint; //so the enemies know where to spawn from
    public Transform[] path; //so the enemies know where to go

    private void Awake()
    {
        main = this;
    }
}
