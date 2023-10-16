using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public int ID => id;

        public int TypeID => typeID;

        public int SubTypeID => subTypeID;

        public DialogContent Content => content;
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
            int id = int.Parse(csvGrid[i][0]);
            int type = int.Parse(csvGrid[i][1]);
            int subType = int.Parse(csvGrid[i][2]);

            DialogContent content = new DialogContent(csvGrid[i][3],csvGrid[i][4]);
            DataRow data = new DataRow(id,type, subType, content);

            datas.Add(data);
        }
    }

    public List<DataRow> GetAllDatasRowWithTypes(MISSION_TYPE type, MISSION_SUBTYPE subType) => datas.Where(data => data.TypeID == (int)type && data.SubTypeID == (int)subType).ToList();
    
}
