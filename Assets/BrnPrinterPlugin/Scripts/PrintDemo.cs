using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using PrintLib;

public class PrintDemo : MonoBehaviour
{
    Printer printer; // our main printer object
    List<string> printers; // used to save printer names
    Button printBtn;
    InputField field;

    void Start()
    {
        // create printer object
        printer = new Printer();
        // get controls and set events
       
        GameObject obj = GameObject.Find("/Canvas/Win/ScrollView/Viewport/Content/InputField");
        if (obj) field = obj.GetComponent<InputField>();
        // enum all printers
        int printerCount = printer.GetPrinterCount();
        printers = new List<string>();
        for (int i = 0; i < printerCount; i++)
        {
            string printerName = printer.GetPrinterName(i);
            printers.Add(printerName);
        }
        // select the default printer
        if (printerCount > 0)
        {
            string defaultPrinter = printer.GetDefaultPrinterName();
            int index = 0;
            // find the index of the default printer
            if (!string.IsNullOrEmpty(defaultPrinter)) index = printers.IndexOf(defaultPrinter);
            // select this printer
            ChangePrinter(index);
        }
    }

    void ChangePrinter(int index)
    {
        string printerName = printer.GetPrinterName(index);
        printer.SelectPrinter(printerName);
    }

    // start the print test
    public void Print(Planet printPlanet)
    {
        ChangePrinter(2);
        // change printer settings here!
        Debug.Log("print my ticket");
        printer.SetPrinterSettings(Orientation.Default, PaperFormat.LetterSmall, 0, ColorMode.Default);
        
        printer.StartDocument();

        // if we specify a width and a height for the text, the text will be wrapped in a rectangle
        printer.SetTextFontFamily("Verdana");
        printer.SetTextFontSize(2);
        printer.SetTextColor(new Color(0.2f, 0.2f, 0.2f));
        
        printer.SetTextFontStyle(TextFontStyle.Bold);
        printer.SetPrintPosition(5, 120);
        printer.PrintText("MISSION REPORT");
        
        printer.SetTextFontStyle(TextFontStyle.Bold);
        printer.SetPrintPosition(5, 160);
        printer.PrintText("Planet :");
        
       // printer.SetTextFontSize(2);
        printer.SetTextFontStyle(TextFontStyle.Regular);
        printer.SetPrintPosition(5,190);
        printer.PrintText(printPlanet.name);
        
        
        printer.SetTextFontStyle(TextFontStyle.Bold);
        printer.SetPrintPosition(5, 280);
        printer.PrintText("Coordinates : ");
        
        
        printer.SetTextFontStyle(TextFontStyle.Regular);
        printer.SetPrintPosition(5, 310);
        printer.PrintText(printPlanet.planetCoordinates.ToString());
        
        printer.SetTextFontStyle(TextFontStyle.Bold);
        printer.SetPrintPosition(5, 400);
        printer.PrintText("Required modules : ");


        int beginY = 520;
        for (int i = 0; i < printPlanet.Missions.Count; i++)
        {
            printer.SetTextFontStyle(TextFontStyle.Regular);
            printer.SetPrintPosition(5, beginY);
            printer.PrintText(printPlanet.Missions[i].Subtype.ToString());
            beginY += 15;

        }
        
        printer.SetTextFontStyle(TextFontStyle.Regular);
        beginY += 15;
        printer.SetPrintPosition(5, beginY);
        printer.PrintText("------------------------------------------------------");
        
        printer.SetTextFontStyle(TextFontStyle.Regular);
        beginY += 30;
        printer.SetPrintPosition(5, beginY);
        printer.PrintText("------------------------------------------------------");
        // 30 = 1 ligne
        
        // end document and send to printer!
        printer.EndDocument(); 
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }
}
