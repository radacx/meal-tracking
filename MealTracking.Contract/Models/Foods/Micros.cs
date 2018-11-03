namespace MealTracking.Contract.Models.Foods
{
    public class Micros
    {
        public static Micros operator +(Micros micros, Micros other)
            => new Micros
            {
                Iron = micros.Iron + other.Iron,
                Sodium = micros.Sodium + other.Sodium,
                Zinc = micros.Zinc + other.Zinc
            };

        public static Micros operator *(Micros micros, double multiplier)
            => new Micros
            {
                Iron = micros.Iron * multiplier,
                Sodium = micros.Sodium * multiplier,
                Zinc = micros.Zinc * multiplier
            };

        public static Micros operator /(Micros micros, double divisor)
            => new Micros
            {
                Iron = micros.Iron / divisor,
                Sodium = micros.Sodium / divisor,
                Zinc = micros.Zinc / divisor
            };
        
        public double Iron { get; set; }

        public double Sodium { get; set; }

        public double Zinc { get; set; }
        
        public Micros Clone() => new Micros
        {
            Iron = Iron,
            Sodium = Sodium,
            Zinc = Zinc
        };
    }
}
