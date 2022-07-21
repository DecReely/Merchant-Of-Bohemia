namespace MerchantOfBohemia
{
    public class Enums
    {
        public enum GameState //sonra başka stateler eklenebilir.
        {
            Idle,
            Traveling
        }
        
        public enum HexType
        {
            Road,
            Obstacle,
            Location,
            none
        }
        
        public enum Season
        {
            Spring,
            Summer,
            Autumn,
            Winter,
            count
        }

        public enum TimeInterval
        {
            Dawn,
            EarlyMorning,
            Morning,
            Noon, 
            Afternoon,
            Dusk,
            Evening,
            Midnight,
            LateNight,
            none
        }
    }
}
