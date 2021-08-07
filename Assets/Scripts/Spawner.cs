using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [Tooltip("Список настроек для цифр")]
    [SerializeField] private List<CardData> cardSettings;
    [SerializeField] private List<CardData> temporarySettings;
    [SerializeField] private List<CardData> variants;
    [SerializeField] private List<CardData> answers;
    [SerializeField]
    private GameObject card;
    [SerializeField]
    private Text ask;
    [SerializeField]
    private GameObject finish;
    [SerializeField]
    private GameObject downloadBack;
    [SerializeField]
    private GameObject particles;
    private string answerName;

    private List<GameObject> Cards;

    private float m;

    private void Start()
    {
        Cards = new List<GameObject>();
        StartCoroutine(NextLvl(0));
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
        if (m == 3) finish.SetActive(true);
        else
        {
            
            variants.Clear();
            for (int i = 0; i < cardSettings.Count; i++)
                {
                if (answers.Count > 0)
                {
                    for (int j = 0; j < answers.Count; j++)
                    {
                        if (cardSettings[i] != answers[j]) temporarySettings.Add(cardSettings[i]);
                    }
                }
                else temporarySettings.Add(cardSettings[i]);
            }

            for (int i = 0; i < Cards.Count; i++)
            {
                Destroy(Cards[i]);
            }
            particles.SetActive(false);
            Cards.Clear();
            m++;
            Spawn(m);
            
        }
    }

    public void ManageCorotunes()
    {
        StartCoroutine(Return());
    }

    private IEnumerator Return()
    {
        downloadBack.SetActive(true);
        m = 0;
        answerName = null;
        answers.Clear();
        StartCoroutine(NextLvl(2));
        yield return new WaitForSeconds(2);
        finish.SetActive(false);
        downloadBack.SetActive(false);
    }
}
