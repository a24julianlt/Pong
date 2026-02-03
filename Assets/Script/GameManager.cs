using UnityEngine;
using TMPro; //Pqra que recoñeza "TMP Text"

public class GameManager : MonoBehaviour
{
    int p1Score;
    int p2Score;
    bool running = false;

    [SerializeField] GameObject pelota;
    [SerializeField] PelotaController pelotaController;


    [SerializeField] TMP_Text txtP1Score;
    [SerializeField] TMP_Text txtP2Score;

    [SerializeField] TMP_Text txtInstruc;
    [SerializeField] TMP_Text txtWin1;
    [SerializeField] TMP_Text txtWin2;

    void Start()
    {
        Cursor.visible = false;
    }

    public void AddPointP1()
    {
        p1Score++;
        txtP1Score.text = p1Score.ToString();
    }
    public void AddPointP2()
    {
        p2Score++;
        txtP2Score.text = p2Score.ToString();
    }

    void Update()
    {
        if (!running && Input.GetKeyDown(KeyCode.Space))
        {

            p1Score = 0;
            txtP1Score.text = "0";
            p2Score = 0;
            txtP2Score.text = "0";

            txtWin1.gameObject.SetActive(false);
            txtWin2.gameObject.SetActive(false);
            
            txtInstruc.gameObject.SetActive(false);

            // Activamos la pelota 
            pelota.SetActive(true);
            pelotaController.ResetBall();
            // Indicamos que el juego ha comenzado
            running = true;
        }

        // Si se pulsa la tecla Escape, salimos de la aplicación 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (p1Score >= 5 || p2Score >= 5)
        {
            running = false;
            pelota.SetActive(false);
            txtInstruc.gameObject.SetActive(true);

            if (p2Score >= 5)
            {
                txtWin1.gameObject.SetActive(true);
            }
            else
            {
                txtWin2.gameObject.SetActive(true);
            }
        }
    }
}