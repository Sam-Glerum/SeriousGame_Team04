using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Text UIText;
    public GameObject UI;
    public GameObject Game;
    public AudioSource halloIkBenAlbert;
    public AudioSource theater;
    public AudioSource casette;
    private bool firstTimeTheatre;
    private bool firstTimeCasette;

    // Start is called before the first frame update
    void Start()
    {
        Game.SetActive(false);
        firstTimeTheatre = true;
        firstTimeCasette = true;
        halloIkBenAlbert.Play();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeUI();
    }

    public void ChangeUI()
    {
        if (!halloIkBenAlbert.isPlaying)
        {
            if (!theater.isPlaying && firstTimeTheatre)
            {
                firstTimeTheatre = false;
                theater.Play();
                UIText.fontSize = 15;
                UIText.text = "Ik loop hier al rond zolang ik leef, mijn vader was ooit de oude directeur van deze plek." +
                    " Dus je zou wel kunnen zeggen dat ik ben opgegroeid in dit theater." +
                    " Ik ben hier altijd en ik ken alle hoeken en gaten van dit theater," +
                    " maar toch kom je elke keer weer nieuwe dingen tegen";
            }

            if (!theater.isPlaying && firstTimeCasette)
            {
                firstTimeCasette = false;
                casette.Play();
                UIText.text = "Nu we het er toch over hebben, ik heb gisteren een casettebandje gevonden." +
                    " Ik ben hem helaas kwijt geraakt maar ik denk dat ik wel weet waar hij ligt." +
                    " Ik had gisteren namelijk een ander jasje aan, zou je kunnen kijken in de garderobe of hij in mijn rode colbert met gouden knopen ligt?";
            }
            if(!casette.isPlaying && !firstTimeCasette)
            {
                UI.SetActive(false);
                Game.SetActive(true);
            }
        }
    }
}
