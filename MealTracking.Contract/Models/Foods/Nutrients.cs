namespace MealTracking.Contract.Models.Foods
{
    public class Nutrients
    {
        public static Nutrients operator +(Nutrients nutrients, Nutrients other)
        {
            var macros = nutrients.Macros + other.Macros;
            var micros = nutrients.Micros + other.Micros;

            return new Nutrients(macros, micros);
        }

        public static Nutrients operator *(Nutrients nutrients, double multiplier)
        {
            var macros = nutrients.Macros * multiplier;
            var micros = nutrients.Micros * multiplier;

            return new Nutrients(macros, micros);
        }
        
        public static Nutrients operator /(Nutrients nutrients, double divisor)
        {
            var macros = nutrients.Macros / divisor;
            var micros = nutrients.Micros / divisor;
            return new Nutrients(macros, micros);
        }
        
        public Macros Macros { get; set; } = new Macros();

        public Micros Micros { get; set; } = new Micros();

        public Nutrients() { }

        public Nutrients(Macros macros, Micros micros)
        {
            Macros = macros;
            Micros = micros;
        }

        public Nutrients Clone() => new Nutrients(Macros.Clone(), Micros.Clone());
    }
}
