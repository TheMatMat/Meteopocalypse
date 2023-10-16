using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    [Serializable]
    public class DataRow
    {
        [SerializeField] private int id;
        [SerializeField] private int typeID;
        [SerializeField] private int subTypeID;
        [SerializeField] private DialogContent content;

        public DataRow(int id, int typeID, int subTypeID, DialogContent content)
        {
            this.id = id;
            this.typeID = typeID;
            this.subTypeID = subTypeID;
            this.content = content;
        }
    }

    [Serializable]
    public struct DialogContent
    {
        public string fr;
        public string en;

        public DialogContent(string fr, string en)
        {
            this.fr = fr;
            this.en = en;
        }
    }

    private static CSVReader _instance;

    public static CSVReader Instance
    {
        get => _instance;
    }

    [SerializeField] private TextAsset csvFile;
    [SerializeField] private List<DataRow> datas;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    private void Start()
    {
        Load(csvFile);
    }

    private void Load(TextAsset csv)
    {
        datas.Clear();
        string[][] csvGrid = CsvParser2.Parse(csv.text);

        for (int i = 1; i < csvGrid.Length; i++)
        {
            int type = int.Parse(csvGrid[i][0]);
            int subType = int.Parse(csvGrid[i][1]);

            DialogContent content = new DialogContent(csvGrid[i][2],csvGrid[i][3]);
            DataRow data = new DataRow(0,type, subType, content);

            datas.Add(data);
        }
    }
}
