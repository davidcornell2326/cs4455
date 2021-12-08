using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public static UIManager instance;
    [SerializeField] private Animator healthAnim;

    [SerializeField] private string mainSceneName;
    [SerializeField] private string startSceneName;
    [SerializeField] private string winSceneName;

    [Header("UI Panels")]
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject deathScreenConfirmation;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject pauseScreenConfirmation;
    [SerializeField] private GameObject HUD;

    [Header("Sliders")]
    [SerializeField] private Slider HPSlider;
    [SerializeField] private Slider ammoSlider;
    [SerializeField] private Slider blueSlider;
    [SerializeField] private Slider greenSlider;
    [SerializeField] private Slider purpleSlider;
    [SerializeField] private Slider yellowSlider;

    [Header("Target Counter")]
    [SerializeField] private GameObject targetCounter;
    [SerializeField] private TMP_Text counterText;

    void Awake() {
        //singleton!
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        if (deathScreen)
            deathScreen.SetActive(false);
        if (deathScreenConfirmation)
            deathScreenConfirmation.SetActive(false);
        if (pauseScreenConfirmation)
            pauseScreenConfirmation.SetActive(false);
        if (pauseScreen)
            pauseScreen.SetActive(false);
        if (HUD)
            HUD.SetActive(true);
        if (!healthAnim)
            healthAnim = HPSlider.transform.GetChild(0).GetComponent<Animator>();
    }

    public void StartGame() {
        SceneManager.LoadScene(mainSceneName);
    }

    public void ShowDeathScreen() {
        HUD.SetActive(false);
        deathScreen.SetActive(true);

        CursorLock.SetCursorLock(false);
    }

    public void Respawn() {
        print("Respawn button has been pressed");

        deathScreen.SetActive(false);
        pauseScreen.SetActive(false);
        HUD.SetActive(true);
        CursorLock.SetCursorLock(true);

        Respawner.instance.Respawn(GameObject.Find("Player"));
    }

    public void Home() {
        print("home button has been pressed");
        Time.timeScale = 1;
        SceneManager.LoadScene(startSceneName);
    }

    public void Win() {
        SceneManager.LoadScene(winSceneName);
    }

    public void TogglePause() {
        if (!deathScreen.activeSelf && !deathScreenConfirmation.activeSelf) {
            CursorLock.SetCursorLock(!CursorLock.isLocked);

            if (CursorLock.isLocked)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;

            HUD.SetActive(pauseScreen.activeSelf);
            pauseScreen.SetActive(!pauseScreen.activeSelf);
        }
    }

    public void ToggleDeathPopup()
    {
        if (deathScreenConfirmation.active)
        {
            deathScreenConfirmation.SetActive(false);
        } else
        {
            deathScreenConfirmation.SetActive(true);
        }
    }

    public void TogglePausePopup()
    {
        if (pauseScreenConfirmation.active)
        {
            pauseScreenConfirmation.SetActive(false);
        }
        else
        {
            pauseScreenConfirmation.SetActive(true);
        }
    }

    public void SetHP() {
        Health playerHealth = GameObject.Find("Player").GetComponentInParent<Health>();
        float percentage = ((float) playerHealth.currentHealth) / playerHealth.maxHealth;

        //print("health set: " + percentage);

        HPSlider.value = percentage;
        healthAnim.SetFloat("health", percentage);
    }

    public void SetAmmo(float percentage) {
        ammoSlider.value = percentage;

        GameObject fillArea = ammoSlider.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        if (percentage == 0.0f && fillArea.activeSelf == true)
        {
            fillArea.SetActive(false);
        } else if (fillArea.activeSelf == false && percentage > 0.0f)
        {
            fillArea.SetActive(true);
        }
    }
    
    public void SetInventorySlider(int collected, int total, JelloColor jc) {
        Slider s = null;

        switch (jc) {
            case JelloColor.BLUE: {
                    s = blueSlider;
                    break;
                }
            case JelloColor.GREEN: {
                    s = greenSlider;
                    break;
                }
            case JelloColor.PURPLE: {
                    s = purpleSlider;
                    break;
                }
            case JelloColor.YELLOW: {
                    s = yellowSlider;
                    break;
                }
            default: {
                    Debug.LogWarning("THERE SHOULD BE NO DEFAULT JELLO COLOR (INVENTORY SLIDER)");
                    return;
                }
        }

        s.value = ((float) collected) / total;

        GameObject fillArea = s.transform.GetChild(1).gameObject.transform.GetChild(0).gameObject;
        if (collected == 0 && fillArea.activeSelf == true)
        {
            fillArea.SetActive(false);
        }
        else if (fillArea.activeSelf == false && collected > 0)
        {
            fillArea.SetActive(true);
        }

        s.gameObject.GetComponentInChildren<TMP_Text>().text = collected + " / " + total;
    }

    private Coroutine fadeIn;
    private Coroutine fadeOut;
    public void UpdateTargetCount(int hitTargets, int totalTargets)
    {
        string hit = hitTargets.ToString();
        string total = totalTargets.ToString();

        counterText.text = hit + " / " + total;
        if (fadeIn != null) {
            StopCoroutine(fadeIn);
        }
        if (fadeOut != null) {
            StopCoroutine(fadeOut);
        }
        fadeIn = StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        for (float i = 0f; i < 1; i += Time.deltaTime)
        {
            counterText.color = new Color(1f, 1f, 1f, i);
            yield return null;
        }
        counterText.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(1f);
        fadeOut = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        for (float i = 0f; i < 1; i += Time.deltaTime)
        {
            counterText.color = new Color(1f, 1f, 1f, (1f - i));
            yield return null;
        }
        counterText.color = new Color(1f, 1f, 1f, 0f);
    }
}
