namespace GuessTheNumber.Scripts.Extensions
{
    public static class StringExtensions
    {
        public static string GetPercents(this float progress)
        {
            return (progress * 100.0f).ToString("f0") + "%";
        }
    }
}