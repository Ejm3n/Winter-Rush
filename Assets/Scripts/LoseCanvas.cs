
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseCanvas : MonoBehaviour
{
    public GameObject player;

    private float timer = 1f;
 
    
    [SerializeField] GameObject loseCan;
    [SerializeField] GameObject PlayCan;
    private GameObject currentScreen;

    private void Start()
    {
        loseCan.SetActive(false);//работа с канвасами
        PlayCan.SetActive(true);
        currentScreen = PlayCan;
    }
    private void Update()
    {
        if(player == null)
        {
            timer -= Time.deltaTime;
            Debug.Log("1");
        }
        if(timer<=0)
        {         
            ChangeState(loseCan );
            Debug.Log("2");
            Time.timeScale = 0;
        }
        
    }
    public void OnClickToMainMenu()
    {

        SceneManager.LoadScene(0);

    }

    // Update is called once per frame
    public void OnClickRestart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void ChangeState(GameObject state)//просто сменщик канвасов
    {
        if (currentScreen != null)
        {
            currentScreen.SetActive(false);
            state.SetActive(true);
            currentScreen = state;
            Debug.Log("5");
        }
    }


    
}
