
using System.Text;


namespace Services.AutoMapper
{
    public static class AutoMapperHalper
    {
        private static Dictionary<char, char> dection = new Dictionary<char, char>() {
            { 'а','a' },
            { 'б','b' },
            { 'в','v' },
            { 'г','h' },
            { 'ґ','g' },
            { 'д','d' },
            { 'е','e' },
            { 'є','e'},
            { 'ж','z' },
            { 'з','z' },
            { 'и','y' },
            { 'і','i' },
            { 'ї','i'},
            { 'й','j' },
            { 'к','k' },
            { 'л','l' },
            { 'м','m' },
            { 'н','n' },
            { 'о','o' },
            { 'п','p' },
            { 'р','r' },
            { 'с','s' },
            { 'т','t' },
            { 'у','u' },
            { 'ф','f' },
            { 'х','x' },
            { 'ц','c' },
            { 'ч','c' },
            { 'ш','s' },
            { 'щ','s' },
            { 'ю','j' },
            { 'я','j' },
            {' ','-' },
            {'1','1' },
            {'2','2' },
            {'3','3' },
            {'4','4' },
            {'5','5' },
            {'6','6' },
            {'7','7' },
            {'8','8' },
            {'9','9' },
            {'0','0' },
            {'-','-' },
            {'+','+' }
            
        };
        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.ToLower();
            StringBuilder strB = new StringBuilder();

            foreach (char c in str)
            {
                if(dection.ContainsKey(c))
                strB.Append(dection[c]);
            }

            return strB.ToString();
        }
    }
}
