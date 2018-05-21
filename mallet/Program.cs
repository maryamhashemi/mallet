using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.IO;
using NHazm;
using edu.stanford.nlp.ling;


namespace mallet
{
    class Program
    {
        static void Main(string[] args)
        {
            string class1FilePath = @"C:\Users\maryam\Documents\Visual Studio 2015\Projects\mallet\politicsTrain.txt";
            string class2FilePath = @"C:\Users\maryam\Documents\Visual Studio 2015\Projects\mallet\sportsTrain.txt";
            string dataTrainFilePath = @"C:\Users\maryam\Documents\Visual Studio 2015\Projects\mallet\dataTrain.txt";

            TrainData(class1FilePath, class2FilePath, dataTrainFilePath);

            WriteLine("done!");
            ReadLine();
        }

        static void TrainData(string class1FilePath, string class2FilePath, string dataTrainFilePath)
        {
            string[] c1 = File.ReadAllLines(class1FilePath);
            string[] c2 = File.ReadAllLines(class2FilePath);

            int count = 2 * Math.Min(c1.Count(), c2.Count());

            string[] contents = new string[count];
            int c1Index = 0;
            int c2Index = 0;

            POSTagger tagger = new POSTagger(@"C:\Users\maryam\Documents\Visual Studio 2015\Projects\mallet\NHazm-master\NHazm\Resources\persian.tagger");

            for (int i = 0; i < count; i++)
            {
                string label;
                string[] features = new string[100];
                if (i%2 == 0)
                {
                    label = "politics";
                    features[0] = c1[c1Index].Contains("تیم") ? "1" : "0";
                    features[1] = c1[c1Index].Contains("فوتبال") ? "1" : "0";
                    features[2] = c1[c1Index].Contains("لیگ") ? "1" : "0";
                    features[3] = c1[c1Index].Contains("قهرمان") ? "1" : "0";
                    features[4] = c1[c1Index].Contains("ملی") ? "1" : "0";
                    features[5] = c1[c1Index].Contains("استقلال") ? "1" : "0";
                    features[6] = c1[c1Index].Contains("هفته") ? "1" : "0";
                    features[7] = c1[c1Index].Contains("آسیا") ? "1" : "0";
                    features[8] = c1[c1Index].Contains("فیلم") ? "1" : "0";
                    features[9] = c1[c1Index].Contains("رقابت") ? "1" : "0";
                    features[10] = c1[c1Index].Contains("پرسپولیس") ? "1" : "0";
                    features[11] = c1[c1Index].Contains("مجلس") ? "1" : "0";
                    features[12] = c1[c1Index].Contains("وزیر") ? "1" : "0";
                    features[13] = c1[c1Index].Contains("استیضاح") ? "1" : "0";
                    features[14] = c1[c1Index].Contains("کشور") ? "1" : "0";
                    features[15] = c1[c1Index].Contains("خبرنگار") ? "1" : "0";
                    features[16] = c1[c1Index].Contains("خارجه") ? "1" : "0";
                    features[17] = c1[c1Index].Contains("گفت و گو") ? "1" : "0";
                    features[18] = c1[c1Index].Contains("نماینده") ? "1" : "0";    
                    features[19] = c1[c1Index].Contains("اسلامی") ? "1" : "0";
                    features[20] = c1[c1Index].Contains("رهبر") ? "1" : "0";
                    features[21] = c1[c1Index].Contains("جلسه") ? "1" : "0";

                    string[] st = c1[c1Index].Split(' ');
                    List<TaggedWord> stTag = tagger.BatchTag(new List<string>(st));

                    for(int j = 0; j < stTag.Count; j++)
                    {
                        var wordTag = stTag[j];
                        features[22 + j] = wordTag.tag();
                    }

                    for(int j =0; j < st.Length-1; j++)
                    {
                        features[22 + stTag.Count + j] = st[j] + " " + st[j + 1];
                    }
                    
                    c1Index++;

                    contents[i] = i.ToString() + " " +
                                  label + " ";

                    for (int j = 0; j < 22 + stTag.Count + st.Length -1; j++)
                    {
                        contents[i] += "f" + j.ToString() + " " + features[j] + " ";
                    }
                }
                else
                {
                    label = "sports";
                    features[0] = c2[c2Index].Contains("تیم") ? "1" : "0";
                    features[1] = c2[c2Index].Contains("فوتبال") ? "1" : "0";
                    features[2] = c2[c2Index].Contains("لیگ") ? "1" : "0";
                    features[3] = c2[c2Index].Contains("قهرمان") ? "1" : "0";
                    features[4] = c2[c2Index].Contains("ملی") ? "1" : "0";
                    features[5] = c2[c2Index].Contains("استقلال") ? "1" : "0";
                    features[6] = c2[c2Index].Contains("هفته") ? "1" : "0";
                    features[7] = c2[c2Index].Contains("آسیا") ? "1" : "0";
                    features[8] = c2[c2Index].Contains("فیلم") ? "1" : "0";
                    features[9] = c2[c2Index].Contains("رقابت") ? "1" : "0";
                    features[10] = c2[c2Index].Contains("پرسپولیس") ? "1" : "0";
                    features[11] = c2[c2Index].Contains("مجلس") ? "1" : "0";
                    features[12] = c2[c2Index].Contains("وزیر") ? "1" : "0";
                    features[13] = c2[c2Index].Contains("استیضاح") ? "1" : "0";
                    features[14] = c2[c2Index].Contains("کشور") ? "1" : "0";
                    features[15] = c2[c2Index].Contains("خبرنگار") ? "1" : "0";
                    features[16] = c2[c2Index].Contains("خارجه") ? "1" : "0";
                    features[17] = c2[c2Index].Contains("گفت و گو") ? "1" : "0";
                    features[18] = c2[c2Index].Contains("نماینده") ? "1" : "0";
                    features[19] = c2[c2Index].Contains("اسلامی") ? "1" : "0";
                    features[20] = c2[c2Index].Contains("رهبر") ? "1" : "0";
                    features[21] = c2[c2Index].Contains("جلسه") ? "1" : "0";

                    string[] st = c2[c1Index].Split(' ');
                    List<TaggedWord> stTag = tagger.BatchTag(new List<string>(st));

                    for (int j = 0; j < stTag.Count; j++)
                    {
                        var wordTag = stTag[j];
                        features[22 + j] = wordTag.tag();
                    }

                    for (int j = 0; j < st.Length - 1; j++)
                    {
                        features[22 + stTag.Count + j] = st[j] + " " + st[j + 1];
                    }

                    c2Index++;

                    contents[i] = i.ToString() + " " +
                                  label + " ";

                    for (int j = 0; j < 22 +stTag.Count + st.Length - 1; j++)
                    {
                        contents[i] += "f" + j.ToString() + " " + features[j] + " ";
                    }
                }
            }
            File.WriteAllLines(dataTrainFilePath, contents);
        }

    }
}
