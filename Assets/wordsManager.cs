using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class wordsManager
{
    System.Exception curException;

    public List<string> wordsList { get; private set; }
    public void readWordsFromFile(string path, int amount)
    {
        if (amount != -1)
            wordsList = new List<string>(amount);
        else
            wordsList = new List<string>();
        StreamReader reader = new StreamReader(path, Encoding.GetEncoding(1251));
        if (amount == -1)
            while (!reader.EndOfStream)
            {
                string readStr = reader.ReadLine();
                if (checkCorrectInput(readStr))
                    wordsList.Add(readStr);
                else
                    throw curException;
            }
        else
            for (int i = 0; i < amount; i++)
            {
                string readStr = reader.ReadLine();
                if (checkCorrectInput(readStr))
                    wordsList.Add(readStr);
                else
                    throw curException;
            }
        //Debug.Log(reader.ReadToEnd());
        reader.Close();
    }
    
    private bool checkCorrectInput(string inStr)
    {
        Encoder dd  = Encoding.Unicode.GetEncoder();
        if (inStr.Length < 3)
        {
            curException = new System.Exception("Wrong dictionary input. " + inStr);
            return false;
        }

        string possibleCharsLower = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        string possibleCharsUpper = possibleCharsLower.ToUpper();
        Debug.Log(possibleCharsUpper);
        bool hasUpper = false;
        foreach (char chr in inStr)
        {
            if (possibleCharsLower.Contains(chr.ToString()) || possibleCharsUpper.Contains(chr.ToString()))
            {
                if (possibleCharsUpper.Contains(chr.ToString()))
                    hasUpper = true;
            }
            else
            {
                curException = new System.Exception("Wrong dictionary input. Line \"" + inStr + "\" contains inappropriate sign: \"" + chr + "\"");
                return false;
            }
        }
        if(!hasUpper)
        {
            curException = new System.Exception("Wrong dictionary input. Line \"" + inStr + " has no upper case");
            return false;
        }
        return true;            
            
    }

}
