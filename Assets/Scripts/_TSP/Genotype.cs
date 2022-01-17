using UnityEngine;

public class Genotype : MonoBehaviour
{
    public int CrossoverPoint { get; set; }
    public float MutationPercent { get; set; }

    public float Cost { get; private set; }
    private int[] geneList; // lista genów (nowych miast)
    int mutate = 0;

    public Genotype(City[] cities)
    {
        bool[] taken = new bool[cities.Length];
        geneList = new int[cities.Length];
        Cost = 0.0f;

        for (int i = 0; i < geneList.Length; i++) 
            taken[i] = false;

        for (int i = 0; i < geneList.Length - 1; i++)
        {
            int subject;
            do
            {
                subject = (int)(Random.Range(0.0f, 1.0f) * geneList.Length-1);
            } while (taken[subject]);

            geneList[i] = subject;

            taken[subject] = true;

            if (i == geneList.Length - 2)
            {
                subject = 0;
                while (taken[subject]) 
                    subject++;

                geneList[i + 1] = subject;
            }
        }

        CalculateCost(cities);
    }

    public void CalculateCost(City[] cities)
    {
        Cost = 0;

        for (int i = 0; i < geneList.Length - 1; i++)
        {
            float dist = cities[geneList[i]].GetDistanceFromCity(cities[geneList[i + 1]]);
            Cost += dist;
        }
    }

    public void PrintCity(int i, City[] cities)
    {
        //Debug.Log($"City {i}: ({cities[geneList[i]].X}, {cities[geneList[i]].Y})");
        GlobalStats.OutputList.Add($"City {i}: ({cities[geneList[i]].X}, {cities[geneList[i]].Y})");
        GlobalStats.OutputList.Add(System.Environment.NewLine);
        cities[geneList[i]].SetCityIdx(i);
    }

    public int Mate(Genotype father, Genotype offspring1, Genotype offspring2)
    {
        int crossoverPostion1 = (int)(Random.Range(0.0f, 1.0f) * (geneList.Length - CrossoverPoint));
        int crossoverPostion2 = crossoverPostion1 + CrossoverPoint;

        int[] offset1 = new int[geneList.Length];
        int[] offset2 = new int[geneList.Length];
        bool[] taken1 = new bool[geneList.Length];
        bool[] taken2 = new bool[geneList.Length];

        for (int i = 0; i < geneList.Length; i++)
        {
            taken1[i] = false;
            taken2[i] = false;
        }

        for (int i = 0; i < geneList.Length; i++)
        {
            if (i < crossoverPostion1 || i >= crossoverPostion2)
            {
                offset1[i] = -1;
                offset2[i] = -1;
            }
            else
            {
                int imother = geneList[i];
                int ifather = father.geneList[i];

                offset1[i] = ifather;
                offset2[i] = imother;

                taken1[ifather] = true;
                taken2[imother] = true;
            }
        }

        for (int i = 0; i < crossoverPostion1; i++)
        {
            if (offset1[i] == -1)
            {
                for (int j = 0; j < geneList.Length; j++)
                {
                    int imother = geneList[j];

                    if (!taken1[imother])
                    {
                        offset1[i] = imother;
                        taken1[imother] = true;
                        break;
                    }
                }
            }
            if (offset2[i] == -1)
            {
                for (int j = 0; j < geneList.Length; j++)
                {
                    int ifather = father.geneList[j];

                    if (!taken2[ifather])
                    {
                        offset2[i] = ifather;
                        taken2[ifather] = true;
                        break;
                    }
                }
            }
        }

        for (int i = geneList.Length - 1; i >= crossoverPostion2; i--)
        {
            if (offset1[i] == -1)
            {
                for (int j = geneList.Length - 1; j >= 0; j--)
                {
                    int imother = geneList[j];
                    if (!taken1[imother])
                    {
                        offset1[i] = imother;
                        taken1[imother] = true;
                        break;
                    }
                }
            }

            if (offset2[i] == -1)
            {
                for (int j = geneList.Length - 1; j >= 0; j--)
                {
                    int ifather = father.geneList[j];

                    if (!taken2[ifather])
                    {
                        offset2[i] = ifather;
                        taken2[ifather] = true;
                        break;
                    }
                }
            }
        }

        offspring1.geneList = offset1;
        offspring2.geneList = offset2;

        mutate = 0;
        CheckMutation(offset1);
        CheckMutation(offset2);

        return mutate;
    }

    private void CheckMutation(int[] offset)
    {
        if (Random.Range(0.0f, 1.0f) < MutationPercent)
        {
            int swapPoint1 = (int)(Random.Range(0.0f, 1.0f) * (geneList.Length-1));
            int swapPoint2 = (int)(Random.Range(0.0f, 1.0f) * (geneList.Length-1));

            int i = offset[swapPoint1];
            offset[swapPoint1] = offset[swapPoint2];
            offset[swapPoint2] = i;

            mutate++;
        }
    }

    public static void SortGenotypes(Genotype[] genotypes, int num) //num -- the number of the chromosome list
    {
        bool swapped = true;
        Genotype tmp;

        while (swapped)
        {
            swapped = false;

            for (int i = 0; i < num - 1; i++)
            {
                if (genotypes[i].Cost > genotypes[i + 1].Cost)
                {
                    tmp = genotypes[i];
                    genotypes[i] = genotypes[i + 1];
                    genotypes[i + 1] = tmp;
                    swapped = true;
                }
            }
        }
    }
}