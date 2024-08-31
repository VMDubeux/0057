using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;
        }
    }

    [Header("Sprites: ")]
    public Sprite CartaComum,
        PerfumePeq,
        PerfumeMed,
        PerfumeGrd, 
        CartaEsp,
        CartaMed;
}
