using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVReaderTest : MonoBehaviour
{
    [SerializeField] TextAsset csvFile; // CSVファイル
    List<Test> tests;
    bool isSkip = true;

    void Start()
    {
        tests = new List<Test>();

        //csvFile = Resources.Load("testCSV") as TextAsset; // Resouces下のCSV読み込み
        StringReader reader = new StringReader(csvFile.text);

        // , で分割しつつ一行ずつ読み込み
        // リストに追加していく
        while (reader.Peek() != -1) // reader.Peaekが-1になるまで
        {
            string line = reader.ReadLine(); // 一行ずつ読み込み
            string[] tmplists = line.Split(',');

            //一行目はスキップ
            if (isSkip)
            {
                isSkip = false;
                return;
            }

            tests.Add(new Test(tmplists[0], tmplists[1], int.Parse(tmplists[2]), int.Parse(tmplists[3])));
        }

        // csvDatas[行][列]を指定して値を自由に取り出せる
        //Debug.Log(csvDatas[0][1]);
        Debug.Log(tests);
    }

}

public class Test
{
    string a;
    string b;
    int c;
    int d;

    public Test (string a, string b, int c, int d)
    {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
    }
}