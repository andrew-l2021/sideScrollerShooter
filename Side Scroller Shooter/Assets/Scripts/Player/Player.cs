using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Inspector variables (these variables should never be modified, only called)
    [Header("Movement and Damage")]
    [SerializeField] float speed = 3;
    [SerializeField] float fireRate;
    [SerializeField] float damagePercentage = 1.00F;

    [Header("Combinations")]
    [SerializeField] public float qBar = 100;
    [SerializeField] public float wBar = 100;
    [SerializeField] public float eBar = 100;
    [SerializeField] int maxComboInput = 2; //The maximum number of letters that can be inputted into a combo window

    [Header("QWE Bars (Higher Rate = Faster Regeneration)")]
    [SerializeField] public float maxQRate; //Point increase per second
    [SerializeField] public float maxWRate;
    [SerializeField] public float maxERate;

    //Instance variables
    [HideInInspector] public float timer = 0;
    Gun[] guns;

    //Movement variables
    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    bool pressq;
    bool pressw;
    bool presse;
    bool pressSpace;
    bool presst;

    //Combo variables
    public float currentq { get; private set; }
    public float currentw { get; private set; }
    public float currente { get; private set; }
    [HideInInspector] public ArrayList comboLettersList = new ArrayList(); //ONLY USED SO COMBO CAN BE DISPLAYED ON-SCREEN

    //Shooting variables
    bool shoot;
    float lastShot = 0;

    //Blaster variables
    QBlaster qBlasterObject;
    WBlaster wBlasterObject;
    EBlaster eBlasterObject;

    //Property variables
    float currentSpeed;
    float currentFireRate;
    public float currentDamagePercentage { get; private set; }

    //Regeneration variables
    [HideInInspector] public float timeLastQBarChange = 0;
    [HideInInspector] public float timeLastWBarChange = 0;
    [HideInInspector] public float timeLastEBarChange = 0;
    [HideInInspector] public float currentQRate;
    [HideInInspector] public float currentWRate;
    [HideInInspector] public float currentERate;

    //Buff Timer variables
    float speedBuffTime = 0;
    float fireRateBuffTime = 0;
    float damageBuffTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Gun initialization
        guns = transform.GetComponentsInChildren<Gun>();

        //Blaster initialization
        qBlasterObject = transform.GetComponentInChildren<QBlaster>();
        wBlasterObject = transform.GetComponentInChildren<WBlaster>();
        eBlasterObject = transform.GetComponentInChildren<EBlaster>();
        
        currentSpeed = speed;
        currentFireRate = fireRate;
        currentDamagePercentage = damagePercentage;
        currentq = qBar;
        currentw = wBar;
        currente = eBar;
        currentQRate = maxQRate / 2;
        currentWRate = maxWRate / 2;
        currentERate = maxERate / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0) //Checks if game is not paused
        {
            moveUp = Input.GetKey(KeyCode.UpArrow);
            moveDown = Input.GetKey(KeyCode.DownArrow);
            moveLeft = Input.GetKey(KeyCode.LeftArrow);
            moveRight = Input.GetKey(KeyCode.RightArrow);
            pressq = Input.GetKeyDown(KeyCode.Q);
            pressw = Input.GetKeyDown(KeyCode.W);
            presse = Input.GetKeyDown(KeyCode.E);
            pressSpace = Input.GetKeyDown(KeyCode.Space);
            presst = Input.GetKeyDown(KeyCode.T);

            timer += Time.deltaTime;

            //counts time for speed up buff
            if (speedBuffTime > 0)
            {
                speedBuffTime -= Time.deltaTime;

                if (speedBuffTime <= 0)
                {
                    //when the the timer is up end the speedboost
                    currentSpeed = speed;
                }
            }

            //counts time for fire rate buff
            if (fireRateBuffTime > 0)
            {
                fireRateBuffTime -= Time.deltaTime;

                if (fireRateBuffTime <= 0)
                {
                    //when the the timer is up end the speedboost
                    currentFireRate = fireRate;
                }
            }

            //counts time for damage buff
            if (damageBuffTime > 0)
            {
                damageBuffTime -= Time.deltaTime;

                if (damageBuffTime <= 0)
                {
                    //when the the timer is up end the speedboost
                    currentDamagePercentage = damagePercentage;
                }
            }

            //Shooting
            shoot = Input.GetKey(KeyCode.R);
            if (shoot) //Spam shooting or Hold "R" shooting (limited to fireRate)
            {
                if (timer > currentFireRate + lastShot)
                {
                    foreach (Gun gun in guns)
                    {
                        gun.Shoot();
                    }
                    lastShot = timer;
                }
            }
            else
            {
                lastShot = timer - currentFireRate;
            }

            //Combos

            if (presst) //Clears combo array
            {
                comboLettersList.Clear();
                printArray();
            }

            if (pressq)
            {
                if (comboLettersList.Count >= maxComboInput)
                {
                    comboLettersList.RemoveAt(0);
                }
                comboLettersList.Add("q");
            }

            if (pressw)
            {
                if (comboLettersList.Count >= maxComboInput)
                {
                    comboLettersList.RemoveAt(0);
                }
                comboLettersList.Add("w");
            }

            if (presse)
            {
                if (comboLettersList.Count >= maxComboInput)
                {
                    comboLettersList.RemoveAt(0);
                }
                comboLettersList.Add("e");
            }

            if (pressSpace)
            {
                StartCoroutine(ExecuteCombo());
            }

            //QWE Bar Regeneration

            if (timer - timeLastQBarChange > 5 && currentq < qBar)
            {
                currentq += currentQRate * Time.deltaTime;
                if (currentQRate < maxQRate)
                {
                    currentQRate *= 1.0001f;
                }
            }
            if (timer - timeLastWBarChange > 5 && currentw < wBar)
            {
                currentw += currentWRate * Time.deltaTime;
                if (currentWRate < maxWRate)
                {
                    currentWRate *= 1.0001f;
                }
            }
            if (timer - timeLastEBarChange > 5 && currente < eBar)
            {
                currente += currentERate * Time.deltaTime;
                if (currentERate < maxERate)
                {
                    currentERate *= 1.0001f;
                }
            }
        }
    }

    IEnumerator ExecuteCombo() //Reads the array from LEFT to RIGHT, ORDER MATTERS
                                       //If there are more than 3 letters of the same kind in a row, it will split into a 3-letter combo
                                       //and read the remaining letters (ex: "eeee" will translate to a 3-letter combo, then a 1-letter combo)
                                       //Ex: "wwwwew" will translate to "www", then "w", then "e", then "w"
                                       //Each combo will be deployed several frames apart to prevent overlapping
                                       //If a combo can't be deployed due to missing Q/W/E bar power, it will be skipped
    {
        string temporaryCombo = "";

        while (comboLettersList.Count != 0)
        {
            //If "temporaryCombo" is empty or the next letter in array matches previous letter, remove first index and add to "temporaryCombo"
            if (temporaryCombo == "" || temporaryCombo.Substring(temporaryCombo.Length - 1) == comboLettersList[0].ToString())
            {
                temporaryCombo += comboLettersList[0].ToString();
                comboLettersList.RemoveAt(0);

                Debug.Log(temporaryCombo);
            } else
            {
                Debug.Log(temporaryCombo);
                switch (temporaryCombo)
                {
                    case "q":
                        if (currentq >= 5)
                        {
                            Debug.Log("q combo!");

                            qBlasterObject.singleCombo(); //"Q" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currentq -= 5;
                            timeLastQBarChange = timer;
                            currentQRate = maxQRate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for q combo!");
                        }
                        break;
                    case "qq":
                        if (currentq >= 20)
                        {
                            Debug.Log("qq combo!");

                            qBlasterObject.doubleCombo(); //"QQ" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currentq -= 20;
                            timeLastQBarChange = timer;
                            currentQRate = maxQRate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for qq combo!");
                        }
                        break;
                    case "qqq":
                        if (currentq >= 45)
                        {
                            Debug.Log("qqq combo!");

                            qBlasterObject.tripleCombo(); //"QQQ" combo execution

                            yield return StartCoroutine(WaitFrames(50)); //Gives a 50-frame window between each combo
                            currentq -= 45;
                            timeLastQBarChange = timer;
                            currentQRate = maxQRate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for qqq combo!");
                        }
                        break;
                    case "w":
                        if (currentw >= 5)
                        {
                            Debug.Log("w combo!");

                            wBlasterObject.singleCombo(); //"W" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currentw -= 5;
                            timeLastWBarChange = timer;
                            currentWRate = maxWRate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for w combo!");
                        }
                        break;
                    case "ww":
                        if (currentw >= 20)
                        {
                            Debug.Log("ww combo!");

                            wBlasterObject.doubleCombo(); //"WW" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currentw -= 20;
                            timeLastWBarChange = timer;
                            currentWRate = maxWRate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for ww combo!");
                        }
                        break;
                    case "www":
                        if (currentw >= 45)
                        {
                            Debug.Log("www combo!");

                            wBlasterObject.tripleCombo(); //"WWW" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currentw -= 45;
                            timeLastWBarChange = timer;
                            currentWRate = maxWRate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for www combo!");
                        }
                        break;
                    case "e":
                        if (currente >= 5)
                        {
                            Debug.Log("e combo!");

                            eBlasterObject.singleCombo(); //"E" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currente -= 5;
                            timeLastEBarChange = timer;
                            currentERate = maxERate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for e combo!");
                        }
                        break;
                    case "ee":
                        if (currente >= 20)
                        {
                            Debug.Log("ee combo!");

                            eBlasterObject.doubleCombo(); //"EE" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currente -= 20;
                            timeLastEBarChange = timer;
                            currentERate = maxERate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for ee combo!");
                        }
                        break;
                    case "eee":
                        if (currente >= 45)
                        {
                            Debug.Log("eee combo!");

                            eBlasterObject.tripleCombo(); //"EEE" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currente -= 45;
                            timeLastEBarChange = timer;
                            currentERate = maxERate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for eee combo!");
                        }
                        break;
                }
                temporaryCombo = "";
            }

            if (comboLettersList.Count == 0)
            {
                Debug.Log(temporaryCombo);
                switch (temporaryCombo)
                {
                    case "q":
                        if (currentq >= 5)
                        {
                            Debug.Log("q combo!");

                            qBlasterObject.singleCombo(); //"Q" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currentq -= 5;
                            timeLastQBarChange = timer;
                            currentQRate = maxQRate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for q combo!");
                        }
                        break;
                    case "qq":
                        if (currentq >= 20)
                        {
                            Debug.Log("qq combo!");

                            qBlasterObject.doubleCombo(); //"QQ" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currentq -= 20;
                            timeLastQBarChange = timer;
                            currentQRate = maxQRate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for qq combo!");
                        }
                        break;
                    case "qqq":
                        if (currentq >= 45)
                        {
                            Debug.Log("qqq combo!");

                            qBlasterObject.tripleCombo(); //"QQQ" combo execution

                            yield return StartCoroutine(WaitFrames(50)); //Gives a 50-frame window between each combo
                            currentq -= 45;
                            timeLastQBarChange = timer;
                            currentQRate = maxQRate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for qqq combo!");
                        }
                        break;
                    case "w":
                        if (currentw >= 5)
                        {
                            Debug.Log("w combo!");

                            wBlasterObject.singleCombo(); //"W" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currentw -= 5;
                            timeLastWBarChange = timer;
                            currentWRate = maxWRate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for w combo!");
                        }
                        break;
                    case "ww":
                        if (currentw >= 20)
                        {
                            Debug.Log("ww combo!");

                            wBlasterObject.doubleCombo(); //"WW" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currentw -= 20;
                            timeLastWBarChange = timer;
                            currentWRate = maxWRate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for ww combo!");
                        }
                        break;
                    case "www":
                        if (currentw >= 45)
                        {
                            Debug.Log("www combo!");

                            wBlasterObject.tripleCombo(); //"WWW" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currentw -= 45;
                            timeLastWBarChange = timer;
                            currentWRate = maxWRate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for www combo!");
                        }
                        break;
                    case "e":
                        if (currente >= 5)
                        {
                            Debug.Log("e combo!");

                            eBlasterObject.singleCombo(); //"E" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currente -= 5;
                            timeLastEBarChange = timer;
                            currentERate = maxERate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for e combo!");
                        }
                        break;
                    case "ee":
                        if (currente >= 20)
                        {
                            Debug.Log("ee combo!");

                            eBlasterObject.doubleCombo(); //"EE" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currente -= 20;
                            timeLastEBarChange = timer;
                            currentERate = maxERate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for ee combo!");
                        }
                        break;
                    case "eee":
                        if (currente >= 45)
                        {
                            Debug.Log("eee combo!");

                            eBlasterObject.tripleCombo(); //"EEE" combo execution

                            yield return StartCoroutine(WaitFrames(50));
                            currente -= 45;
                            timeLastEBarChange = timer;
                            currentERate = maxERate / 2;
                        }
                        else
                        {
                            Debug.Log("Not enough power for eee combo!");
                        }
                        break;
                }
                temporaryCombo = "";
            }
        }

        Debug.Log("Q level: " + currentq + ", W level: " + currentw + ", E level: " + currente);
        Debug.Log("Executed and Cleared Combos!");
        comboLettersList.Clear(); //Clears array
    }

    public static IEnumerator WaitFrames(int frameCount) //Waits for "frameCount" number of frames to prevent combos from overlapping
    {
        while (frameCount > 0)
        {
            frameCount--;
            yield return null;
        }
    }

    private void printArray() //Debugging method
    {
        Debug.Log("Letter Array: " + comboLettersList.ToString());
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;

        float moveAmount = currentSpeed * Time.fixedDeltaTime;
        Vector2 move = Vector2.zero;

        //Basic movement
        if (moveUp)
        {
            move.y += moveAmount;
        }
        if (moveDown)
        {
            move.y -= moveAmount;
        }
        if (moveLeft)
        {
            move.x -= moveAmount;
        }
        if (moveRight)
        {
            move.x += moveAmount;
        }

        //Adjust for faster diagonal movement
        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }

        //Boundaries
        pos += move;
        if (pos.x <= -8.32f)
        {
            pos.x = -8.32f;
        }
        if (pos.x >= 8.32f)
        {
            pos.x = 8.32f;
        }
        if (pos.y >= 3.74f)
        {
            pos.y = 3.74f;
        }
        if (pos.y <= -3.86f)
        {
            pos.y = -3.86f;
        }

        transform.position = pos;
    }

    public void TemporarilyIncreaseFireRate(float fireRateModifier, int time)
    {
        //using subtract because rate of fire increases as currentFireRate decreases
        Debug.Log("Fire Rate Increased!");
        currentFireRate -= fireRateModifier;
        fireRateBuffTime = time;
    }

    public void TemporarilyIncreaseSpeed(float speedModifier, int time)
    {
        //using addition because speed increases as currentSpeed increases
        Debug.Log("Speeding!");
        currentSpeed += speedModifier;
        speedBuffTime = time;
    }

    public void TemporarilyIncreaseDamage(float damageModifier, int time)
    {
        //1 = 100% damage, 1 * (1 + 0.20) = 1.20 = 120%
        Debug.Log("Damage Increased!");
        currentDamagePercentage *= (1 + damageModifier);
        damageBuffTime = time;
    }

    public void PermanentlyIncreaseRandomQWE() //Increases the Q, W, or E bar by anywhere between 5 and 10 points
    {
        int qweDeterminer = Random.Range(1, 3); //Range method returns a random integer (inclusive)
        if (qweDeterminer == 1)
        {
            qBar += (int)Random.Range(5, 10);
        }
        if (qweDeterminer == 2)
        {
            wBar += (int)Random.Range(5, 10);
        }
        if (qweDeterminer == 3)
        {
            eBar += (int)Random.Range(5, 10);
        }
    }
}
