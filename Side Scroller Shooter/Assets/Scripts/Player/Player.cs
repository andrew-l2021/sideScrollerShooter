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
    private char[] comboLetters = new char[9]; //Make sure the variable maxComboInput is NEVER greater than 8!
    private int numQ = 0; //Number of q's, w's, and e's in combo window
    private int numW = 0;
    private int numE = 0;
    public float currentq { get; private set; }
    public float currentw { get; private set; }
    public float currente { get; private set; }

    //Instance variables
    float timer = 0;
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

    //Shooting variables
    bool shoot;
    float lastShot = 0;

    //Property variables
    float currentSpeed;
    float currentFireRate;
    float currentDamagePercentage;

    //Buff Timer variables
    float speedBuffTime = 0;
    float fireRateBuffTime = 0;
    float damageBuffTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        guns = transform.GetComponentsInChildren<Gun>();
        currentSpeed = speed;
        currentFireRate = fireRate;
        currentDamagePercentage = damagePercentage;
        currentq = qBar;
        currentw = wBar;
        currente = eBar;
    }

    // Update is called once per frame
    void Update()
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

            if(speedBuffTime <= 0 )
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
                    gun.Shoot(currentDamagePercentage);
                }
                lastShot = timer;
            }
        }else{
            lastShot = timer - currentFireRate;
        }

        //Combos

        if (presst) //Clears combo array
        {
            comboLetters = new char[9];
            printArray();
            Debug.Log("Cleared combos!");
        }

        if (pressq)
        {
            AddCombo('q');
        }

        if (pressw)
        {
            AddCombo('w');
        }

        if (presse)
        {
            AddCombo('e');
        }

        if (pressSpace)
        {
            StartCoroutine(ExecuteCombo());
        }
    }

    private void AddCombo(char comboLetter) //Adds a letter to the combo array, spanning from left to right.
                                            //Only a subsection of the array is accessed depending on the value of maxComboInput.
                                            //If (sub)array is full, all combos are shifted to the left by one and the letter in the first index is lost.
    {
        for (int i = 0; i < maxComboInput; i++)
        {
            if (comboLetters[maxComboInput - 1] == 'q' || comboLetters[maxComboInput - 1] == 'w' || comboLetters[maxComboInput - 1] == 'e') //Checking if (sub)array is full yet
            {
                for (int k = 0; k < maxComboInput - 1; k++) //Shifting (sub)array to the left by one
                {
                    comboLetters[k] = comboLetters[k + 1];
                }
                comboLetters[maxComboInput - 1] = comboLetter;
                printArray();
                Debug.Log("Combo window was full!");
                break;
            } else //If (sub)array isn't full
            {
                if (comboLetters[i] != 'q' && comboLetters[i] != 'w' && comboLetters[i] != 'e') //Checking for next available slot to put combo in
                {
                    comboLetters[i] = comboLetter;
                    printArray();
                    Debug.Log("Added combo in index " + i + " of array!");
                    break;
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
        for (int i = 0; i < maxComboInput; i++)
        {
            if (comboLetters[i] == 'q')
            {
                numQ++;
                if (numQ == 3) //The 3-letter combo is checked for first because there is no combo that requires more letters
                {
                    if (currentq >= 45) {
                        Debug.Log("qqq combo!");
                        //PLACEHOLDER HERE TO CALL ACTUAL QQQ COMBO
                        yield return StartCoroutine(WaitFrames(50)); //Gives a 50-frame window between each combo
                        currentq -= 45;
                    } else
                    {
                        Debug.Log("Not enough power for qqq combo!");
                    }
                    numQ = 0; //Deletes combo even if not enough energy for it in order to move onto next combo
                }
                if (comboLetters[i + 1] != 'q') //If the letter after is not the same letter, a one-letter or two-letter combo will be checked
                {
                    if (numQ == 2) //2-letter combo
                    {
                        if (currentq >= 20)
                        {
                            Debug.Log("qq combo!");
                            //PLACEHOLDER HERE TO CALL ACTUAL QQ COMBO
                            yield return StartCoroutine(WaitFrames(50));
                            currentq -= 20;
                        } else
                        {
                            Debug.Log("Not enough power for qq combo!");
                        }
                        numQ = 0;
                    }
                    if (numQ == 1) //1-letter combo
                    {
                        if (currentq >= 5)
                        {
                            Debug.Log("q combo!");
                            //PLACEHOLDER HERE TO CALL ACTUAL Q COMBO
                            yield return StartCoroutine(WaitFrames(50));
                            currentq -= 5; 
                        } else
                        {
                            Debug.Log("Not enough power for q combo!");
                        }
                        numQ = 0;
                    }
                }
            }

            if (comboLetters[i] == 'w')
            {
                numW++;
                if (numW == 3)
                {
                    if (currentw >= 45)
                    {
                        Debug.Log("www combo!");
                        //PLACEHOLDER HERE TO CALL ACTUAL WWW COMBO
                        yield return StartCoroutine(WaitFrames(50));
                        currentw -= 45;
                    }
                    else
                    {
                        Debug.Log("Not enough power for www combo!");
                    }
                    numW = 0;
                }
                if (comboLetters[i + 1] != 'w')
                {
                    if (numW == 2)
                    {
                        if (currentw >= 20)
                        {
                            Debug.Log("ww combo!");
                            //PLACEHOLDER HERE TO CALL ACTUAL WW COMBO
                            yield return StartCoroutine(WaitFrames(50));
                            currentw -= 20;
                        }
                        else
                        {
                            Debug.Log("Not enough power for ww combo!");
                        }
                        numW = 0;
                    }
                    if (numW == 1)
                    {
                        if (currentw >= 5)
                        {
                            Debug.Log("w combo!");
                            //PLACEHOLDER HERE TO CALL ACTUAL W COMBO
                            yield return StartCoroutine(WaitFrames(50));
                            currentw -= 5;
                        }
                        else
                        {
                            Debug.Log("Not enough power for w combo!");
                        }
                        numW = 0;
                    }
                }
            }

            if (comboLetters[i] == 'e')
            {
                numE++;
                if (numE == 3)
                {
                    if (currente >= 45)
                    {
                        Debug.Log("eee combo!");
                        //PLACEHOLDER HERE TO CALL ACTUAL EEE COMBO
                        yield return StartCoroutine(WaitFrames(50));
                        currente -= 45;
                    }
                    else
                    {
                        Debug.Log("Not enough power for eee combo!");
                    }
                    numE = 0;
                }
                if (comboLetters[i + 1] != 'e')
                {
                    if (numE == 2)
                    {
                        if (currente >= 20)
                        {
                            Debug.Log("ee combo!");
                            //PLACEHOLDER HERE TO CALL ACTUAL WW COMBO
                            yield return StartCoroutine(WaitFrames(50));
                            currente -= 20;
                        }
                        else
                        {
                            Debug.Log("Not enough power for ee combo!");
                        }
                        numE = 0;
                    }
                    if (numE == 1)
                    {
                        if (currente >= 5)
                        {
                            Debug.Log("e combo!");
                            //PLACEHOLDER HERE TO CALL ACTUAL E COMBO
                            yield return StartCoroutine(WaitFrames(50));
                            currente -= 5;
                        }
                        else
                        {
                            Debug.Log("Not enough power for e combo!");
                        }
                        numE = 0;
                    }
                }
            }
        }

        Debug.Log("Q level: " + currentq + ", W level: " + currentw + ", E level: " + currente);
        Debug.Log("Executed and Cleared Combos!");
        comboLetters = new char[9]; //Clears array
        numQ = 0;
        numW = 0;
        numE = 0;
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
        for (int i = 0; i < comboLetters.Length; i++)
        {
            Debug.Log(i + ": " + comboLetters[i]);
        }
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
        if (pos.y >= 4.47f)
        {
            pos.y = 4.47f;
        }
        if (pos.y <= -4.47f)
        {
            pos.y = -4.47f;
        }

        transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            Destroy(gameObject);
            Destroy(bullet);
        }
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
}
