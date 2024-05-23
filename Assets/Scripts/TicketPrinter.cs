using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TicketPrinter : MonoBehaviour
{
    [SerializeField] private string ticketCode;
    private string currentCode = "";

    [SerializeField] private TextMeshProUGUI codeText;
    [SerializeField] private TextMeshProUGUI outText;

    [Header("Audio Stuff")]
    
    [SerializeField] private AudioClip onInput;
    [SerializeField] private AudioClip onCorrect;
    [SerializeField] private AudioClip onIncorrect;
    [SerializeField] private Transform audioPoint;

    [Header("Ticket Stuff")]
    
    [SerializeField] private GameObject ticketPrefab;
    [SerializeField] private Transform ticketPos;
    
    

    private bool inputLocked = false;

    // Start is called before the first frame update
    void Start()
    {
        outText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewChar(string character)
    {
        if (inputLocked)
        {
            return;
        }
        
        AudioSource.PlayClipAtPoint(onInput, audioPoint.position);
        
        currentCode += character;
        UpdateText();
        if (currentCode.Length >= ticketCode.Length)
        {
            StartCoroutine(finishCode());
        }
    }

    public void ResetCode()
    {
        if (inputLocked)
        {
            return;
        }
        currentCode = "";
        UpdateText();
    }

    void UpdateText()
    {
        string output = "";
        foreach (char codeChar in currentCode)
        {
            output += codeChar + " ";
        }

        for (int n = 0; n < (ticketCode.Length - currentCode.Length); n++)
        {
            output += "_ ";
        }

        codeText.text = output;
    }

    IEnumerator finishCode()
    {
        inputLocked = true;
        yield return new WaitForSeconds(0.1f);
        Debug.Log(currentCode);
        Debug.Log(ticketCode);
        if (currentCode == ticketCode)
        {
            outText.text = "PRINTING TICKET...";
            Instantiate(ticketPrefab, ticketPos.position, ticketPos.rotation);
            AudioSource.PlayClipAtPoint(onCorrect, audioPoint.position);
        }
        else
        {
            outText.text = "INCORRECT...";
            AudioSource.PlayClipAtPoint(onIncorrect, audioPoint.position);
            
        }
        yield return new WaitForSeconds(1.5f);
        outText.text = "";
        currentCode = "";
        UpdateText();
        inputLocked = false;
    }
}
