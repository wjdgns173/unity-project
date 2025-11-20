using UnityEngine;

public class SunCon : MonoBehaviour
{

    [SerializeField] private SumFattern _fattern;


    [SerializeField] int maxFatternCount = 7;

    void Awake()
    {
        _fattern = GetComponent<SumFattern>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _fattern.isFatterning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!_fattern.isFatterning)
        {
            float randomFattern = 1; //Random.Range(1,1);
            switch (randomFattern)
            {
                case 1:
                    _fattern.Fettern_1();
                    break;
                case 2:
                    _fattern.Fettern_2();
                    break;
                case 3:
                    _fattern.Fettern_3();
                    break;
                case 4:
                    _fattern.Fettern_4();
                    break;
                case 5:
                    _fattern.Fettern_5();
                    break;
                case 6:
                    _fattern.Fettern_6();
                    break;
                case 7:
                    _fattern.Fettern_7();
                    break;
            }
        }
    }
}
