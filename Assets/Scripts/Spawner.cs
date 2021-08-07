using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [Tooltip("Список настроек для цифр")]
    [SerializeField] private List<CardData> cardNumbers;
    [SerializeField] private List<CardData> cardWords;
    [SerializeField] private List<CardData> temporarySettings;
    [SerializeField] private List<CardData> variants;
    [SerializeField] private List<CardData> answers;
    [SerializeField] private GameObject card;
    [SerializeField] private Text ask;
    [SerializeField] private GameObject finish;
    [SerializeField] private GameObject downloadBack;
    [SerializeField] private GameObject particles;
    [SerializeField] public List<CardBase> cardSets = new List<CardBase>(3);
    private string answerName;
    //private List<List<CardData>> cardSets;
    private List<GameObject> Cards;
    [SerializeField] private List<CardData> choises;

    private float lvlcount;

    private void Start()
    {
        Cards = new List<GameObject>();
        //cardSets[1].cards = cardNumbers;
        //cardSets[2].cards = cardWords;
        ChoiseCards();
        StartCoroutine(NextLvl(0));
    }

    public void ChoiseCards()
    {
        //int randChoise = Random.Range(0, cardSets.Count);
        //choises = cardSets[randChoise].cards;
        int randChoise = Random.Range(0, 1);
        if (randChoise == 0) choises = cardNumbers;
        else if(randChoise == 1) choises = cardWords;
    }

    public bool Сomparison(string name)
    {
        if (name == answerName)
        {
            particles.SetActive(true);
            StartCoroutine(NextLvl(2));
            return true;
        }
        else return false;
    }

    public void Spawn(float m)
    {
        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                var prefab = Instantiate(card, transform.position + new Vector3(j - 1, (m - 1) / 2 - i, 0), transform.rotation);
                var script = prefab.GetComponent<Card>();
                Cards.Add(prefab);
                int randGeneric = Random.Range(0, temporarySettings.Count);
                script.Init(temporarySettings[randGeneric]);
                variants.Add(temporarySettings[randGeneric]);
                temporarySettings.RemoveAt(randGeneric);
            }
               
        }
        int rand = Random.Range(0, variants.Count);
        answerName = variants[rand].name;
        answers.Add(variants[rand]);
        ask.text = "Find " + answerName;
        temporarySettings.Clear();
    }

    public IEnumerator NextLvl(float time)
    {
        yield return new WaitForSeconds(time);
        if (lvlcount == 3) finish.SetActive(true);
        else
        {
            
            variants.Clear();
            for (int i = 0; i < choises.Count; i++)
                {
                if (answers.Count > 0)
                {
                    for (int j = 0; j < answers.Count; j++)
                    {
                        if (choises[i] != answers[j]) temporarySettings.Add(choises[i]);
                    }
                }
                else temporarySettings.Add(choises[i]);
            }

            for (int i = 0; i < Cards.Count; i++)
            {
                Destroy(Cards[i]);
            }
            particles.SetActive(false);
            Cards.Clear();
            lvlcount++;
            Spawn(lvlcount);
            
        }
    }

    public void ManageCorotunes()
    {
        StartCoroutine(Return());
    }

    private IEnumerator Return()
    {
        downloadBack.SetActive(true);
        lvlcount = 0;
        answerName = null;
        answers.Clear();
        ChoiseCards();
        StartCoroutine(NextLvl(2));
        yield return new WaitForSeconds(2);
        finish.SetActive(false);
        downloadBack.SetActive(false);
    }
}
