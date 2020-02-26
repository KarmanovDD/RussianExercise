using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WordBehavour : MonoBehaviour
{
    List<GameObject> letters = new List<GameObject>();
    List<GameObject> missedLetters = new List<GameObject>();
    //List<Vector3> letterPositions = new List<GameObject>();
    public float letterSpacing = 1f;
    int wordLength;
    string wordStr;
    public UnityEvent correctLetterSet;
    public UnityEvent wrongLetterSet;
    //public GameObject Console;
    //ConsoleBehaviour Console;

    wordsManager wordsDictionary;

    string path;
    public int amountWordsToRead = -1;

    [HideInInspector]
    public bool reset = false;

    void Start()
    {
        try
        {
            //Console = FindObjectOfType<ConsoleBehaviour>();
            path = Application.dataPath + "/StreamingAssets/dictionaryInput.txt";
            //Console.GetComponent<ConsoleBehaviour>().write(path);
            //Console.write(path);
            wordsDictionary = new wordsManager();
            wordsDictionary.readWordsFromFile(path, amountWordsToRead);
            initWord();
        }
        catch(System.Exception e)
        {
            //Console.write(e.Message);
        }
    }

    void initWord()
    {
        int wordNumber = Random.Range(0, wordsDictionary.wordsList.Count);
        wordStr = wordsDictionary.wordsList[wordNumber];
        wordLength = wordStr.Length;
        getLetters();
        setLettersPos(calculateLettersLength());
        setLettersActiveAndEraseMissing(wordStr);


    }

    void setMissingLetter(string nameFrom, string nameTo)
    {

    }
    IEnumerator waitForReset(float sec)
    {
        yield return new WaitForSeconds(sec);
        reset = true;
    }

    void wrongSpawnParticles(Vector3 pos)
    {
        spawnParticles(pos, Color.red);
    }
    void correctSpawnParticles(Vector3 pos)
    {
        spawnParticles(pos, Color.green);
    }
    void spawnParticles(Vector3 pos, Color32 color)
    {
        GameObject TempParticle = Instantiate(Resources.Load("outcomeParticles"), pos, Quaternion.identity) as GameObject;
        ParticleSystem parts = TempParticle.GetComponent<ParticleSystem>();
        parts.startColor = color;
        StartCoroutine(playAndDestroyParticleSystem(parts));

    }

    IEnumerator playAndDestroyParticleSystem(ParticleSystem parts)
    {
        parts.Play();
        yield return new WaitForSeconds(parts.main.startLifetimeMultiplier);
        Destroy(parts.gameObject);
    }




    public void handleInputLetter(string nameOfMissing, Collider inputLetter)
    {
        GameObject letterOn = missedLetters.Find(obj => obj.name == nameOfMissing);
        if (letterOn.name[4] == inputLetter.name[4])
        {
            correctLetterSet.Invoke();
            letterOn.GetComponent<BoxCollider>().isTrigger = false;
            letterOn.GetComponent<MeshRenderer>().enabled = true;
            missedLetters.Remove(letterOn);
            correctSpawnParticles(letterOn.GetComponent<BoxCollider>().bounds.center);
            if (missedLetters.Count == 0)
                StartCoroutine(waitForReset(1.5f));
        }
        else
        {
            wrongLetterSet.Invoke();
            wrongSpawnParticles(letterOn.GetComponent<BoxCollider>().bounds.center);
        }
    }


    void setLettersActiveAndEraseMissing(string wordStr)
    {
        for(int i = 0; i < wordStr.Length; i ++)
        {
            if(char.IsUpper(wordStr[i]))
            {
                letters[i].GetComponent<BoxCollider>().isTrigger = true;
                letters[i].GetComponent<MeshRenderer>().enabled = false;
                letters[i].AddComponent<letterTriggerHandler>();
                missedLetters.Add(letters[i]);
                letters[i].SetActive(true);
            }
            else
            {
                letters[i].SetActive(true);
            }
        }
    }

    void setLettersPos(float sizeX)
    {
        float prevCentrePosX = 0;
        float prevSizeX = 0;
        for (int i = 0; i < wordLength; i++)
        {
                if (i == 0)
                {
                    letters[i].transform.position = new Vector3(transform.position.x - sizeX / 2f + letters[i].GetComponent<BoxCollider>().size.x/2, transform.position.y, transform.position.z);
                    prevSizeX = letters[i].GetComponent<BoxCollider>().size.x;
                    prevCentrePosX = letters[i].transform.position.x;
                }
                else
                {
                    letters[i].transform.position = new Vector3(prevCentrePosX + prevSizeX / 2+ letterSpacing + letters[i].GetComponent<BoxCollider>().size.x/2, transform.position.y, transform.position.z);
                    prevCentrePosX = letters[i].transform.position.x;
                    prevSizeX = letters[i].GetComponent<BoxCollider>().size.x;
                }
        }
    }

    float calculateLettersLength()
    {
        float accLetterLength = 0;
        foreach(GameObject letter in letters)
        {
            accLetterLength += letter.GetComponent<BoxCollider>().size.x;
        }
        return accLetterLength + (letters.Count - 1) * letterSpacing;
    }

    void getMissedLetter(ref GameObject desiredLetter)
    {
        desiredLetter.GetComponent<BoxCollider>().isTrigger = true;
        desiredLetter.GetComponent<MeshRenderer>().enabled = false;
    }


    void getLetters()
    {
        foreach (char letter in wordStr)
        {
            GameObject tmp = getLetterPrefab(letter);
            tmp.SetActive(false);
            letters.Add(tmp);            
        }
    }

    GameObject getLetterPrefab(char letter)
    {
        letter = char.ToLower(letter);
        switch (letter)
        {
            case 'а': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'б': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'в': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'г': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'д': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'е': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'ё': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'ж': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'з': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'и': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'й': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'к': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'л': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'м': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'н': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'о': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'п': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'р': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'с': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'т': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'у': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'ф': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'х': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'ц': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'ч': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'ш': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'щ': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'ъ': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'ы': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'ь': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'э': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'ю': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            case 'я': return Instantiate(Resources.Load("charsPrefabs/char" + letter)) as GameObject;
            default: throw new System.Exception("Letter prefab not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(reset)
        {
            //Invoke(nameof(resetWord), 3.0f);
            //StartCoroutine(resetWord());
            for (int i = 0; i < wordLength; i++)
            {
                GameObject letterToDestroy = letters[wordLength - i - 1];
                letters.Remove(letterToDestroy);
                Destroy(letterToDestroy);
            }
            initWord();
            reset = false;
        }
    }

    IEnumerator resetWord()
    {
        yield return new WaitForSeconds(4);

    }

}
