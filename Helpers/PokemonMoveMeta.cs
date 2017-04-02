using POGOProtos.Enums;

// Ported to C# by BadChoicesZ
// Code ported from Grover-c13 - https://github.com/Grover-c13/PokeGOAPI-Java/

namespace PokemonGo.RocketAPI.Helpers
{
    public class PokemonMoveMeta
    {
        public PokemonMove Move;
        private PokemonType _type;
        private int _power;
        private int _accuracy;
        private double _critChance;
        private int _time;
        private int _energy;

        public int GetTime()
        {
            return _time;
        }
        public void SetTime(int time)
        {
            _time = time;
        }

        public PokemonMove GetMove()
        {
            return Move;
        }
        public void SetMove(PokemonMove move)
        {
            Move = move;
        }

        public int GetPower()
        {
            return _power;
        }
        public void SetPower(int power)
        {
            _power = power;
        }

        public int GetAccuracy()
        {
            return _accuracy;
        }

        public void SetAccuracy(int accuracy)
        {
            _accuracy = accuracy;
        }

        public double GetCritChance()
        {
            return _critChance;
        }
        public void SetCritChance(double critChance)
        {
            _critChance = critChance;
        }
        public int GetEnergy()
        {
            return _energy;
        }

        public void SetEnergy(int energy)
        {
            _energy = energy;
        }

        public void SetType(PokemonType type)
        {
            _type = type;
        }

        private new PokemonType GetType()
        {
            return _type;
        }
    }
}