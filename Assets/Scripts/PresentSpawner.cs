using UnityEngine;

public class PresentSpawner : MonoBehaviour
{
    public GameObject present;//префаб подарка
    private GameObject currentPres;//переменная отслеживания текущего подарка
    private Vector2 position; //переменная для спавна текущего подарка
    public bool LastPresent;
    private bool OnLeftSide = true;//проверка откуда будет вылетать враг
    public float speed;//скорость полета врага
    private float timer = 5f;//время до первого вылета врага
    private const float TIMER_TO_RSPWN = 12f;//время между спавнами следущих врагов
    private Vector2 ToWhere;//конечная точка для перемещения врага
    public GameObject Enemy;//префаб врага
    private GameObject CurrentEnemy;
    // рандомный выбор точки спавна подарка
    private float[][] ranges = new float[14][];//массив границ для рандома
    private float[][] range14 = new float[11][];//массив координат для спавна между стенами

    private void Start()
    {
        // создаем границы рандомных чисел чтоб в дальнейшем спавнить подарки в этих границах
        ranges[0] = new float[4] { -10.5f, 0.45f, 5.05f, 5.75f };
        ranges[1] = new float[4] { 0.45f, 11.1f, 5.05f, 5.75f };
        ranges[2] = new float[4] { -10.65f, -9.8f, -1.35f, 5.05f };
        ranges[3] = new float[4] { -7.8f, -4.77f, 1.42f, 3.1f };
        ranges[4] = new float[4] { -3.59f, -0.66f, 1.4f, 3.8f };
        ranges[5] = new float[4] { 1.26f, 4.24f, 1.4f, 3.8f };
        ranges[6] = new float[4] { 5.55f, 8.35f, 1.42f, 3.1f };
        ranges[7] = new float[4] { 10.4f, 11f, -1.35f, 5.05f };
        ranges[8] = new float[4] { -9f, -1f, -1.3f, -0.4f };
        ranges[9] = new float[4] { 1.7f, 10.3f, -1.3f, -0.4f };
        ranges[10] = new float[4] { -10.55f, -7.1f, -3.1f, -5.55f };
        ranges[11] = new float[4] { -5f, -0.7f, -3.1f, -5.55f };
        ranges[12] = new float[4] { 1.25f, 5.5f, -3.1f, -5.55f };
        ranges[13] = new float[4] { 7.6f, 11.1f, -3.1f, -5.55f };

        range14[0] = new float[2] { -8.85f, 3f };
        range14[1] = new float[2] { 9.41f, 3f };
        range14[2] = new float[2] { -4.64f, 0.59f };
        range14[3] = new float[2] { 5.17f, 0.59f };
        range14[4] = new float[2] { -7.5f, -2.22f };
        range14[5] = new float[2] { -1.17f, -2.22f };
        range14[6] = new float[2] { 0.32f, -2.22f };
        range14[7] = new float[2] { 2f, -2.22f };
        range14[8] = new float[2] { 7.95f, -2.22f };
        range14[9] = new float[2] { -6f, -5f };
        range14[10] = new float[2] { 6.62f, -5f };
    }
    //выбор места и непосредственный спавн врагов
    private void Spawn()
    {
        int randomPos = UnityEngine.Random.Range(0, 15);
        if (randomPos < 14)
        {
            position = new Vector2(Random.Range(ranges[randomPos][0], ranges[randomPos][1]), Random.Range(ranges[randomPos][2], ranges[randomPos][3]));
        }
        else
        {
            int rnd = UnityEngine.Random.Range(0, 11);
            position = new Vector2(range14[rnd][0], range14[rnd][1]);
        }
        Debug.Log("spwn");
        currentPres = Instantiate(present, position, Quaternion.identity);
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        if (currentPres == null)
        {
            //при отсуствии текущего подарка создать новый в рандомном месте
            Debug.Log("null");

            Spawn();
        }
        timer -= Time.deltaTime;//подсчет до вылета врага
        if (timer <= 0)
        {
            //респавн врага
            Destroy(CurrentEnemy);
            if (OnLeftSide)
            {
                CurrentEnemy = Instantiate(Enemy, new Vector2(-15f, currentPres.transform.position.y), Quaternion.identity);

                ToWhere = new Vector2(15, currentPres.transform.position.y);
                OnLeftSide = false;
            }
            else if (!OnLeftSide)
            {
                CurrentEnemy = Instantiate(Enemy, new Vector2(15f, currentPres.transform.position.y), Quaternion.identity);
                ToWhere = new Vector2(-15, currentPres.transform.position.y);

                OnLeftSide = true;
            }
            timer = TIMER_TO_RSPWN;
        }
        if (CurrentEnemy != null)
        {
            //движение врага в указанную мной точку в другом конце карты
            CurrentEnemy.transform.position = Vector2.MoveTowards(CurrentEnemy.transform.position, ToWhere, step);
        }
    }
}