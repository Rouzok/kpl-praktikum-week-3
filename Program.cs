using System;

namespace tpmodul4_103082400009
{
    public enum Kelurahan
    {
        Batununggal, Kujangsari, Mengger, Wates, Cijaura,
        Jatisari, Margasari, Sekejati, Kebonwaru, Maleer, Samoja
    }

    public enum DoorState { Terkunci, Terbuka }
    public enum DoorTrigger { KunciPintu, BukaPintu }

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

    class DoorMachine
    {
        public class Transition
        {
            public DoorState prevState;
            public DoorState nextState;
            public DoorTrigger trigger;

            public Transition(DoorState prevState, DoorState nextState, DoorTrigger trigger)
            {
                this.prevState = prevState;
                this.nextState = nextState;
                this.trigger = trigger;
            }
        }

        Transition[] transitions =
        {
                new Transition(DoorState.Terkunci, DoorState.Terbuka, DoorTrigger.BukaPintu),
                new Transition(DoorState.Terbuka, DoorState.Terkunci, DoorTrigger.KunciPintu),
                new Transition(DoorState.Terkunci, DoorState.Terkunci, DoorTrigger.KunciPintu),
                new Transition(DoorState.Terbuka, DoorState.Terbuka, DoorTrigger.BukaPintu)
            };

        public DoorState currentState = DoorState.Terkunci;

        public DoorState getNextState(DoorState prevState, DoorTrigger trigger)
        {
            DoorState nextState = prevState;
            for (int i = 0; i < transitions.Length; i++)
            {
                if (transitions[i].prevState == prevState && transitions[i].trigger == trigger)
                {
                    nextState = transitions[i].nextState;
                }
            }
            return nextState;
        }

        public void activateTrigger(DoorTrigger trigger)
        {
            currentState = getNextState(currentState, trigger);

            if (currentState == DoorState.Terkunci)
            {
                Console.WriteLine("Pintu terkunci");
            }
            else if (currentState == DoorState.Terbuka)
            {
                Console.WriteLine("Pintu tidak terkunci");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
            {
                // Table-Driven (Kode Pos)
                KodePos kPos = new KodePos();
                Console.WriteLine("=== Kode Pos (Table-Driven) ===");
                Console.WriteLine($"{"Kelurahan",-15} | {"Kode Pos",-10}");
                Console.WriteLine("-------------------------------------");

                foreach (Kelurahan kel in Enum.GetValues(typeof(Kelurahan)))
                {
                    Console.WriteLine($"{kel,-15} | {kPos.getKodePos(kel)}");
                }

                // State-Based (Door Machine) 
                Console.WriteLine("\n=== Posisi Pintu (State-Based) ===");
                DoorMachine door = new DoorMachine();

                door.activateTrigger(DoorTrigger.BukaPintu);  // Terkunci -> Terbuka
                door.activateTrigger(DoorTrigger.KunciPintu); // Terbuka -> Terkunci
                door.activateTrigger(DoorTrigger.KunciPintu); // Terkunci -> Terkunci (Stay)
                door.activateTrigger(DoorTrigger.BukaPintu);  // Terkunci -> Terbuka
                door.activateTrigger(DoorTrigger.BukaPintu);  // Terbuka -> Terbuka (Stay)
        }
    }
}