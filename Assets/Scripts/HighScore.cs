using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    //вывод на канвас смерти рекорд
    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetInt("Score").ToString();
    }
}