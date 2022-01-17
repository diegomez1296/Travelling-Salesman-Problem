using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TSPManager : MonoBehaviour
{
    [SerializeField] private MainPanelManager mainPanelManager;
    [SerializeField] private Transform mapPanel;
    [SerializeField] private City cityPrefab;

    private City[] cities;
    private Genotype[] genotypes;

    private int matingPopulationSize;
    private int favoredPopulationSize;

    private int cutLength;
    private int generation;

    private bool wasInit = false;

    public void RunTSP()
    {
        mainPanelManager.SetGlobalStats();
        StartCoroutine(TSPCoroutine());
    }

    private IEnumerator TSPCoroutine()
    {
        wasInit = false;
        mainPanelManager.SetStartButtonInteractable(false);
        mainPanelManager.SetFeedbackText(GlobalStats.feedbackTexts[0]);
        yield return new WaitForSeconds(0.5f);
        Initialization();
        yield return new WaitUntil(() => wasInit == true);
        mainPanelManager.SetFeedbackText(GlobalStats.feedbackTexts[1]);
        yield return new WaitForSeconds(0.5f);
        TSPCompute();
        yield return new WaitUntil(() => wasInit == false);
        mainPanelManager.SetFeedbackText(GlobalStats.feedbackTexts[2]);
        mainPanelManager.SetStartButtonInteractable(true);
    }

    private void Initialization()
    {
        CleanCityList();

        matingPopulationSize = GlobalStats.Population / 2;
        favoredPopulationSize = matingPopulationSize / 2;
        cutLength = GlobalStats.Cities / 5;

        // create a random list of cities
        cities = new City[GlobalStats.Cities];

        for (int i = 0; i < GlobalStats.Cities; i++)
        {
            cities[i] = Instantiate(cityPrefab, mapPanel);
            cities[i].InitCity();
        }

        // create the initial genotypes

        genotypes = new Genotype[GlobalStats.Population];

        for (int i = 0; i < GlobalStats.Population; i++)
        {
            genotypes[i] = new Genotype(cities);
            genotypes[i].CrossoverPoint = cutLength;
            genotypes[i].MutationPercent = GlobalStats.MutationPercent;
        }

        Genotype.SortGenotypes(genotypes, GlobalStats.Population);
        generation = 0;
        wasInit = true;
    }

    private void CleanCityList()
    {
        if (cities == null) return;

        foreach (var item in cities)
        {
            Destroy(item.gameObject);
        }
        cities = null;
    }

    private void TSPCompute()
    {
        double thisCost = 500.0;
        double oldCost = 0.0;
        double dcost = 500.0;
        int countSame = 0;

        while (countSame < 120)
        {
            generation++;
            int ioffset = matingPopulationSize;
            int mutated = 0;

            for (int i = 0; i < favoredPopulationSize; i++)
            {
                Genotype cmother = genotypes[i];
                int father = (int)(Random.Range(0.0f, 1.0f) * matingPopulationSize);
                Genotype cfather = genotypes[father];
                mutated += cmother.Mate(cfather, genotypes[ioffset], genotypes[ioffset + 1]);
                ioffset += 2;
            }

            for (int i = 0; i < matingPopulationSize; i++)
            {
                genotypes[i] = genotypes[i + matingPopulationSize];
                genotypes[i].CalculateCost(cities);
            }

            // Now sort the new population
            Genotype.SortGenotypes(genotypes, matingPopulationSize);
            double cost = genotypes[0].Cost;
            dcost = System.Math.Abs(cost - thisCost);
            thisCost = cost;
            float mutationRate = 100.0f * (float)mutated / (float)matingPopulationSize;

            //Debug.Log("Generation = " + generation.ToString() + " Cost = " + thisCost.ToString() + " Mutated Rate = " + mutationRate.ToString() + "%");
            GlobalStats.OutputList.Add("Generation = " + generation.ToString() + " Cost = " + thisCost.ToString() + " Mutated Rate = " + mutationRate.ToString() + "%");
            GlobalStats.OutputList.Add(System.Environment.NewLine);

            if ((int)thisCost == (int)oldCost)
            {
                countSame++;
            }
            else
            {
                countSame = 0;
                oldCost = thisCost;
            }
        }

        for (int i = 0; i < cities.Length; i++)
        {
            genotypes[i].PrintCity(i, cities);
        }
        wasInit = false;
    }
}
