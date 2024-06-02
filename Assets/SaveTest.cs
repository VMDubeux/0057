using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

public class SaveTest : MonoBehaviour
{
    public int shift = 47;
    [FormerlySerializedAs("_gameObjects")] [SerializeField] List<GameObject> gameObjects = new List<GameObject> ();

    [ContextMenu("Save")]
    public void Save()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject [] enemies = GameObject.FindGameObjectsWithTag("Enemies");
        Debug.Log("Achou o player? ("+player+")");
        StreamWriter sw = new StreamWriter("D:\\Unity\\0057\\Assets\\Others\\SaveTest\\save.txt");
        gameObjects.Clear(); // Limpa a lista de objetos atual
        if(player != null)  gameObjects.Add(player);
        foreach(GameObject go in enemies)
        {
            gameObjects.Add(go);
        }

        gameObjects.Sort((obj1, obj2) => String.Compare(obj1.name, obj2.name, StringComparison.Ordinal));

        foreach (GameObject go in gameObjects)
        {   Vector3 pos = go.GetComponent<Transform>().position;
            sw.WriteLine(Criptografar(pos.ToString(), shift));
        }
        sw.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        Debug.Log("A capacidade da lista é: " + gameObjects.Capacity);

        using (StreamReader sr = new StreamReader("D:\\Unity\\0057\\Assets\\Others\\SaveTest\\save.txt"))
        {
            if (gameObjects.Count == 0) // verifica se a lista está vazia
            {
                Debug.Log("Lista vazia");
            }
            else // se não houver save, carrega as posições
            {
                foreach (GameObject go in gameObjects) // passa por todos os objetos na lista
                {
                    string s = Decriptografar(sr.ReadLine(), shift); // lê a posição de cada um
                    Debug.Log(s);

                    if (!string.IsNullOrEmpty(s))
                    {
                        string[] componentes = s.Trim('(', ')').Split(", "); // remove parênteses e separa coordenadas

                        if (componentes.Length == 3)
                        {
                            float x = float.Parse(componentes[0], System.Globalization.CultureInfo.InvariantCulture);
                            float y = float.Parse(componentes[1], System.Globalization.CultureInfo.InvariantCulture);
                            float z = float.Parse(componentes[2], System.Globalization.CultureInfo.InvariantCulture);

                            var pos = new Vector3(x, y, z);
                            go.GetComponent<Transform>().position = pos; // coloca o objeto na devida posição
                        }
                        else
                        {
                            Debug.LogWarning("Formato da linha inválido: " + s);
                        }
                    }
                }
            }
        }
    }

        public string Criptografar(string frase, int factor)
    {
        string crypto = "";

        foreach (char c in frase)
        {
            char shifted = (char)(c + factor);
            crypto += shifted;
        }
        return crypto;
    }

        public string Decriptografar(string frase, int factor)
    {
        string crypto = "";

        foreach (char c in frase)
        {
            char shifted = (char)(c - factor);
            crypto += shifted;
        }  
        return crypto;
    }
}
