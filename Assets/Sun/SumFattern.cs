using System.Collections;
using UnityEngine;

public class SumFattern : MonoBehaviour
{

    public bool isFatterning = false;

    public GameObject  player;
    public GameObject  firePillar;

    public Transform[] points;

    /*public Transform point_1; //맵 왼쪽 끝
    public Transform point_2; //맵 오른쪽 끝
    public Transform point_3; //맵 위쪽 중앙 
    public Transform point_4; //맵 중앙
    public Transform point_5; //맵 왼쪽 밑 끝
    public Transform point_6; //맵 밑 중앙
    public Transform point_7; //맵 오른쪽 밑 끝*/

    public void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        
    }

    public void Fettern_1()
    {
        StartCoroutine(Fettern_1Coru());
    }

    public IEnumerator Fettern_1Coru() 
    {
        int attackCnt = 0;
        while (attackCnt <= 4)
        {
            attackCnt++;
            isFatterning = true;
            Debug.Log("1번 패턴 실행 중 ");
            //플레이어 위(최대 높이 7{천장 보다 살짝 밑})에서 불기둥 생성 빨간색으로 반짝이며 공격 예시 보여주기
            transform.position = 
                new Vector3(
                    player.transform.position.x, 
                    transform.position.y,
                    transform.position.z
                    );

            yield return new WaitForSeconds(0.2f);
            GameObject fireBall = Instantiate(

                                  firePillar, 
                                  new Vector3
                                  (
                                       transform.position.x,
                                       transform.position.y - 3,
                                       transform.position.z
                                  ),

                                  firePillar.transform.rotation
                                  );

            yield return new WaitForSeconds(0.5f);
        }
        attackCnt    = 0;
        isFatterning = false;
    }

    //=/=/=/=/=/===/

    public void Fettern_2()
    {
        isFatterning = true;
        StartCoroutine(Fettern_2Coru());
    }

    public IEnumerator Fettern_2Coru()
    {
        isFatterning = true;
        Debug.Log("2번 패턴 실행 중 ");
        //포인트 4(중앙) 으로 이동후 검을 360도 돌리며 베기 
        yield return new WaitForSeconds(4); 
        isFatterning = false;
    }

    //=/=/=/=/=/=/=/=/=

    public void Fettern_3()
    {
        isFatterning = true;
        StartCoroutine(Fettern_3Coru());
    }

    public IEnumerator Fettern_3Coru()
    {
        isFatterning = true;
        Debug.Log("3번 패턴 실행 중 ");
        //벼락 떨구기
        yield return new WaitForSeconds(4);
        isFatterning = false;
    }

    //==/=/=/=/=/===

    public void Fettern_4()
    {
        isFatterning = true;
        StartCoroutine(Fettern_4Coru());
    }

    public IEnumerator Fettern_4Coru()
    {
        isFatterning = true;
        Debug.Log("4번 패턴 실행 중 ");
        //맵 전체에 토네이도 순서 : 오른쪽 -> 왼쪽 , 왼쪽 -> 오른쪽 총 두번 
        yield return new WaitForSeconds(4);
        isFatterning = false;
    }

    public void Fettern_5()
    {
        isFatterning = true;
        StartCoroutine(Fettern_5Coru());
    }

    public IEnumerator Fettern_5Coru()
    {
        isFatterning = true;
        Debug.Log("5번 패턴 실행 중 ");
        //예시 5번 참고 왼쪽 맨 끝에서 부터 오른쪽 끝까지 지그재그로 공격 1회 후 반대로 공격 , 총 2회 -> <-
        yield return new WaitForSeconds(4);
        isFatterning = false;
    }

    public void Fettern_6()
    {
        isFatterning = true;
        StartCoroutine(Fettern_6Coru());
    }

    public IEnumerator Fettern_6Coru()
    {
        isFatterning = true;
        Debug.Log("6번 패턴 실행 중 ");
        //플레이어를 따라가 폭발하는 유도탄 생성 한번에 2개 총 3번
        yield return new WaitForSeconds(4);
        isFatterning = false;
    }

    public void Fettern_7()
    {
        isFatterning = true;
        StartCoroutine(Fettern_7Coru());
    }

    public IEnumerator Fettern_7Coru()
    {
        isFatterning = true;
        Debug.Log("7번 패턴 실행 중 ");
        //무작위 위치에 별을 생성하고 1.3 ~ 1.5정도 후 폭발 
        yield return new WaitForSeconds(4);
        isFatterning = false;
    }


}
