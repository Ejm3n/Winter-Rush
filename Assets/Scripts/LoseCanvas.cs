
using UnityEngine;
using UnityEngine.SceneManagement;


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
            //после смерти игрока отсчитываем одну секунду и включаем экран смерти
            timer -= Time.deltaTime;
            Debug.Log("1");
        }
        if(timer<=0)
        {         
            ChangeState(loseCan );
            Debug.Log("2");
            Time.timeScale = 0;
        }
        if ((currentScreen == loseCan) && (Input.GetKeyDown(KeyCode.Return)))
        {
            //рестарт игры можно сделать нажав Enter
            OnClickRestart();
        }
    }
    //переход на сцену главного меню
    public void OnClickToMainMenu()
    {

        SceneManager.LoadScene(0);

    }

    // перезагрузка уровня
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
