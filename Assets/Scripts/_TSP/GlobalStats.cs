using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public static class GlobalStats
{
    //General
    public static int Cities = 50;
    public static int Population = 1000;

    //Gene options
    public static float MutationPercent = 0.001f;
    public static int CrossoverPoint = 1; //Punkt krzy¿owania

    // Map dimensions
    public static int MinX = 0;
    public static int MaxX = 1450;

    public static int MinY = 0;
    public static int MaxY = 950;

    //Feedback dialogs
    public static string[] feedbackTexts = {
        "Trwa generowanie miast...",
        "Wyliczanie najkrótszej drogi...",
        "Najkrótsza droga zosta³a wyznaczona.",
        "B³¹d przetwarzania. SprawdŸ output."
    };

    //Output
    public static List<string> OutputList = new List<string>();
}
