//Tekstiniame faile pateiktas tekstas. Žodžiai iš eilutės į kitą eilutę nekeliami. Skyrikliai žinomi.. Kokių 
//žodžių daugiausia: tų, kurių pirmoji raidė “didesnė” už paskutiniąją ar tų, kurių pirmoji raidė 
//“mažesnė” už paskutiniąją? Pašalinti žodžius, kurių pirmosios dvi raidės sutampa su dviem 
//paskutinėmis raidėmis

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace Laboras_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CFd = "Duomenys.txt";
            const string Cfr = "Analize.txt";
            const string Cfz = "Rezultatai.txt";

            char[] skyrikliai = { ' ', '.', ',', '!', '?', ':', ';', '(', ')', '\t' };

            int d, m;
            int kiekis1, kiekis2;

            Apdoroti(CFd, skyrikliai, out kiekis1, out kiekis2);


            d = kiekis1;
            m = kiekis2;

            SkaitoRaso(CFd, Cfz);

            using (var fr = File.AppendText(Cfz))
            {
                fr.WriteLine();

                fr.WriteLine("REZULTATAI :");

                if (d > m)
                {

                    fr.WriteLine("Žodžių, kurių pirmoji raidė didesnė už paskutiniąją yra daugiau");
                }

                else if  (d == m)
                    {

                        fr.WriteLine("Žodžių kiekis yra vienodas");
                    }
                    else fr.WriteLine("Žodžių, kurių pirmoji raidė mažesnė už paskutiniąją yra daugiau");

                Analize(CFd, Cfr);

            }


        }
        //----------------------------------------------------------------------------------------
        /** Deletes from the line the words that the first two letters ar the same as the two last
        @param line - line of the text
        @param nauja - line of the text without the word/ words */
        //----------------------------------------------------------------------------------------

        static bool BeTamTikruZodziu(string line, out string nauja)
        {
            nauja = line;
            for (int i = 0; i < line[i].ToString().Length - 1; i++)
            {
                if (line[i].ToString().Substring(0, 2) == line[i + 1].ToString().Substring(line[i + 1].ToString().Length - 2, 2))
                {

                    nauja = line[i].ToString().Remove(0, line[i].ToString().Length);
                    return true;

                }

            }
            return false;
        }

        //---------------------------------------------------------------------------------------
        /** Splits the line into words and analyzes the words.
        @param eilute - data string
        @param skyrikliai - word delimiters
        returns (kiek) which is how many words there are whose first letter is bigger than the last*/
        //-----------------------------------------------------------------------------------------
        static int ZodziaiDidesne(string eilute, char[] skyrikliai)
        {
            string[] parts = eilute.Split(skyrikliai,
            StringSplitOptions.RemoveEmptyEntries);
            int kiek = 0;
            foreach (string zodis in parts)
                if (zodis[0] > zodis[zodis.Length-1])
                    kiek++;
            return kiek;
        }

        //----------------------------------------------------------------------------------------
        /** Splits the line into words and analyzes the words.
        @param eilute - data string
        @param skyrikliai - word delimiters
        returns (kiek) which is how many words there are whose first letter is smaller than the last*/
        
        //-----------------------------------------------------------------------------------------
        static int ZodziaiMazesne(string eilute, char[] skyrikliai)
        {
            string[] parts = eilute.Split(skyrikliai,
            StringSplitOptions.RemoveEmptyEntries);
            int kiek = 0;
            foreach (string zodis in parts)
                if (zodis[0] < zodis[zodis.Length-1])
                    kiek++;

            return kiek;
        }
        //------------------------------------------------------------
        /** Reads the data from the txt file and writes the data to a txt file.
        @param fs - initial data file
        @param fr - rezult data file*/
        //------------------------------------------------------------
        static void SkaitoRaso(string fs, string fr)
        {
            using (var frr = File.CreateText(fr))
            {
                using (StreamReader reader = new StreamReader(fs,
                Encoding.UTF8))
                {
                    frr.WriteLine("PRADINIAI DUOMENYS (TEKSTAS):");
                    frr.WriteLine();
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {

                        // text operations
                        frr.WriteLine(line);
                    }
                }
            }
        }
        //------------------------------------------------------------------
        /** Analizes the data and writes the information to the analysis file
        @param fs - Initial data file
        @param fz - Analysis data file */
        //------------------------------------------------------------------
        static void Analize(string fs, string fz)
        {
            string[] lines = File.ReadAllLines(fs, Encoding.GetEncoding(1257));

            using (var frr = File.CreateText(fz))
            {
                using (StreamReader reader = new StreamReader(fs,
                Encoding.UTF8))
                {

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {

                        if (line.Length > 0)
                        {
                            string nauja = line;
                            if (BeTamTikruZodziu(line, out nauja))

                          
                                frr.WriteLine(nauja);
                            if (nauja.Length > 0)
                                frr.WriteLine(nauja);

                        }

                        else frr.WriteLine(line);
                    }
                }
            }
        }

        //--------------------------------------------------------------------------------
        /** Reads the data file and analyzes the data
        @param fv - data file
        @param skyrikliai - word delimiters 
        @param kiekis1 - the ammount of words whose first letter is bigger than the last 
        @param kiekis2 - the ammount of words whose first letter is smaller than the last */
        //-------------------------------------------------------------------------------

        static void Apdoroti(string fv, char[] skyrikliai, out int kiekis1, out int kiekis2)
        {
            string[] lines = File.ReadAllLines(fv, Encoding.GetEncoding(1257));

            kiekis1 = 0;
            kiekis2 = 0;
            foreach (string line in lines)
                if (line.Length > 0)
                    kiekis1 += ZodziaiDidesne(line, skyrikliai);

            foreach (string line in lines)
                if (line.Length > 0)
                    kiekis2 += ZodziaiMazesne(line, skyrikliai);

        }

    }

}
