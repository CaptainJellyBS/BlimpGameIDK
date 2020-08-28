using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public Camera thirdperson, firstPerson;
    public Turret frontTurret, rearTurret;
    public float airPressureForce;
    public float minTimeBetweenRandomImps, maxTimeBetweenRandomImps;
    public int amountOfDevils;
    public GameObject imp, devil;
    private float devilsLeft;

    public bool hasMoved = false, hasVertical = false, hasSwitchedCam = false, hasSwitchedTurret = false, hasShotTurret = false;
    public GameObject hasMovedT, hasVerticalT, hasSwitchedCamT, hasSwitchedTurretT, hasShotTurretT, killDevilsT;
    public GameObject winScreen, loseScreen;
    public float DevilsLeft
    {
        get { return devilsLeft; }
        set { devilsLeft = value; devilsLeftText.text = value.ToString(); }
    }
    public Text devilsLeftText;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountOfDevils; i++)
        {
            SpawnDevils();
        }
        StartCoroutine(Tutorial());
        StartCoroutine(SpawnImps());
        CountDevils();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            hasSwitchedCam = true;
            FirstPersonMode();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hasSwitchedCam = true;
            ThirdPersonMode();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hasSwitchedTurret = true;
            FrontTurretMode();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            hasSwitchedTurret = true;
            RearTurretMode();
        }
    }

    #region camera switching
    public void FirstPersonMode()
    {
        thirdperson.enabled = false; frontTurret.enabled = false; rearTurret.enabled = false; firstPerson.enabled = true;
    }

    public void ThirdPersonMode()
    {
        thirdperson.enabled = true; frontTurret.enabled = false; rearTurret.enabled = false; firstPerson.enabled = false;
    }

    public void FrontTurretMode()
    {
        thirdperson.enabled = false; rearTurret.enabled = false; frontTurret.enabled = true; firstPerson.enabled = false;
    }

    public void RearTurretMode()
    {
        thirdperson.enabled = false; frontTurret.enabled = false; rearTurret.enabled = true; firstPerson.enabled = false;
    }
    #endregion

    public Vector3 GetAirPressureForce(Transform obj)
    {
        return Vector3.down * airPressureForce * obj.position.y;
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        loseScreen.SetActive(true);

    }

    public void Win()
    {
        Time.timeScale = 0.0f;
        winScreen.SetActive(true);
    }

    IEnumerator SpawnImps()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTimeBetweenRandomImps, maxTimeBetweenRandomImps));
            Vector3 randomPos = new Vector3(Random.Range(-500, 500), Random.Range(3, 10), Random.Range(-500, 500));

            if (Vector3.Distance(randomPos, Blimp.Instance.transform.position) < 50) { continue; }

            Instantiate(imp, randomPos, Quaternion.identity);
        }
    }

    public void CountDevils()
    {
        StartCoroutine(CountDevilsC());
    }

    /// <summary>
    /// Godverlaten fix
    /// </summary>
    /// <returns></returns>
    IEnumerator CountDevilsC()
    {
        yield return null;
        Devil[] devils = FindObjectsOfType<Devil>();
        DevilsLeft = devils.Length;

        if (DevilsLeft <= 0) { Win(); }
    }

    #region tutorial
    public void HasMoved()
    {
        hasMoved = true;
    }

    public void HasVertical()
    {
        hasVertical = true;
    }

    IEnumerator Tutorial()
    {
        hasMovedT.SetActive(true);
        while (!hasMoved) { yield return null; }
        hasMovedT.SetActive(false);
        hasVerticalT.SetActive(true);

        while (!hasVertical) { yield return null; }
        hasVerticalT.SetActive(false);
        hasSwitchedCamT.SetActive(true);

        while (!hasSwitchedCam) { yield return null; }
        hasSwitchedCamT.SetActive(false);
        hasSwitchedTurretT.SetActive(true);

        while (!hasSwitchedTurret) { yield return null; }
        hasSwitchedTurretT.SetActive(false);
        hasShotTurretT.SetActive(true);

        while (!hasShotTurret) { yield return null; }
        hasShotTurretT.SetActive(false);
        killDevilsT.SetActive(true);

        yield return new WaitForSeconds(3.0f);
        killDevilsT.SetActive(false);
    }
#endregion

    void SpawnDevils()
    {
        Vector3 randomPos = Vector3.zero;
        bool fuck = true;
        while(fuck)
        {
            randomPos = new Vector3(Random.Range(-500, 500), Random.Range(3, 10), Random.Range(-500, 500));
            fuck = Vector3.Distance(randomPos, Blimp.Instance.transform.position) < 100;
        }
        Instantiate(devil, randomPos, Quaternion.identity);
    }

    public void RestartGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
