namespace MealTracking.Contract.Models.Foods
{
    public class Macros
    {
        public static Macros operator +(Macros macros, Macros other)
            => new Macros
            {
                Carbs = macros.Carbs + other.Carbs,
                Fats = macros.Fats + other.Fats,
                Protein = macros.Protein + other.Protein,
                Alcohol = macros.Alcohol + other.Alcohol
            };

        public static Macros operator *(Macros macros, double multiplier)
            => new Macros
            {
                Carbs = macros.Carbs * multiplier,
                Fats = macros.Fats * multiplier,
                Protein = macros.Protein * multiplier,
                Alcohol = macros.Alcohol * multiplier
            };
        
        public static Macros operator /(Macros macros, double divisor)
            => new Macros
            {
                Carbs = macros.Carbs / divisor,
                Fats = macros.Fats / divisor,
                Protein = macros.Protein / divisor,
                Alcohol = macros.Alcohol / divisor
            };

        public double Protein { get; set; }

        private double ProteinCalories => Protein * 4;
        
        public double Carbs { get; set; }

        private double CarbsCalories => Carbs * 4;
        
        public double Fats { get; set; }

        private double FatsCalories => Fats * 9;
        
        public double Alcohol { get; set; }

        private double AlcoholCalories => Alcohol * 7;

        public double Calories => ProteinCalories + CarbsCalories + FatsCalories + AlcoholCalories;

        public double ProteinPercentage => ProteinCalories / Calories * 100;

        public double CarbsPercentage => CarbsCalories / Calories * 100;

        public double FatsPercentage => FatsCalories / Calories * 100;

        public double AlcoholPercentage => AlcoholCalories / Calories * 100;
        
        public Macros() {}

        public Macros(double protein, double carbs, double fats, double alcohol)
        {
            Protein = protein;
            Carbs = carbs;
            Fats = fats;
            Alcohol = alcohol;
        }
        
        public Macros Clone() => new Macros
        {
            Protein = Protein,
            Carbs = Carbs,
            Fats = Fats,
            Alcohol = Alcohol
        };
    }
}