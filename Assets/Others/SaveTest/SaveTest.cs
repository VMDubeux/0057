using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    public int shift = 47;
    [SerializeField] List<GameObject> _gameObjects = new List<GameObject> ();

    [ContextMenu("Save")]
    public void Save()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        GameObject [] _enemies = GameObject.FindGameObjectsWithTag("Enemies");
        Debug.Log("Achou o player? ("+_player+")");
        StreamWriter sw = new StreamWriter("D:\\Unity\\0057\\Assets\\Others\\SaveTest\\save.txt");
        _gameObjects.Clear(); // Limpa a lista de objetos atual
        if(_player != null)  _gameObjects.Add(_player);
        foreach(GameObject go in _enemies)
        {
            _gameObjects.Add(go);
        }

        _gameObjects.Sort((obj1, obj2) => obj1.name.CompareTo(obj2.name));

        foreach (GameObject go in _gameObjects)
        {   Vector3 pos = go.GetComponent<Transform>().position;
            sw.WriteLine(Criptografar(pos.ToString(), shift));
        }
        sw.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        Debug.Log("A capacidade da lista é: " + _gameObjects.Capacity);

        using (StreamReader sr = new StreamReader("D:\\Unity\\0057\\Assets\\Others\\SaveTest\\save.txt"))
        {
            if (_gameObjects.Count == 0) // verifica se a lista está vazia
            {
                Debug.Log("Lista vazia");
                return;
            }
            else // se não houver save, carrega as posições
            {
                foreach (GameObject go in _gameObjects) // passa por todos os objetos na lista
                {
                    Vector3 pos = go.GetComponent<Transform>().position;
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

                            pos = new Vector3(x, y, z);
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

        public string Criptografar(string frase, int shift)
    {
        string crypto = "";

        foreach (char c in frase)
        {
            char shifted = (char)(c + shift);
            crypto += shifted;
        }
        return crypto;
    }

        public string Decriptografar(string frase, int shift)
    {
        string crypto = "";

        foreach (char c in frase)
        {
            char shifted = (char)(c - shift);
            crypto += shifted;
        }  
        return crypto;
    }
}
