using System;

namespace tpmodul4_103082400009
{
    public enum Kelurahan
    {
        Batununggal, Kujangsari, Mengger, Wates, Cijaura,
        Jatisari, Margasari, Sekejati, Kebonwaru, Maleer, Samoja
    }

    class KodePos
    {
        public string getKodePos(Kelurahan kelurahan)
        {
            string[] kodePosTabel = {
                "40266", "40287", "40267", "40256", "40287",
                "40286", "40286", "40286", "40272", "40274", "40273"
            };

            return kodePosTabel[(int)kelurahan];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            KodePos kPos = new KodePos();

            Console.WriteLine("=== Kode Pos (Table-Driven) ===");
            Console.WriteLine($"{"Kelurahan",-15} | {"Kode Pos",-10}");
            Console.WriteLine("-------------------------------------");

            foreach (Kelurahan kel in Enum.GetValues(typeof(Kelurahan)))
            {
                Console.WriteLine($"{kel,-15} | {kPos.getKodePos(kel)}");
            }
        }
    }
}