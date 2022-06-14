namespace SerpAnalysis.Core.CommonServices
{
    public class CsUtilities
    {
        /// <summary>
        /// This method is to search the enumeration value where the name text contains the names from two enums.
        /// For example: return SearchEngineProvidersWithCountry of "GoogleAu" from searching the Search Provider of "Google" and CountryCode of "Au"
        /// </summary>
        public static TOutput GetEnum<TInput1, TInput2, TOutput>(TInput1 input1, TInput2 input2)
            where TInput1 : struct, Enum
            where TInput2 : struct, Enum
            where TOutput : struct, Enum
        {
            var options = Enum.GetNames(typeof(TOutput));
            var input1Name = Enum.GetName(input1)?.ToUpper();
            var input2Name = Enum.GetName(input2)?.ToUpper();

            foreach (var option in options)
            {
                //either the input is null or matched. If the value provided has a value but not match, it will skip.
                if (option.ToUpper().Contains(input1Name) && option.ToUpper().Contains(input2Name))
                {
                    TOutput s = GetEnumValue<TOutput>(option);
                    return s;
                }
            }

            throw new Exception("No Enum Found.");
        }


        /// <summary>
        /// Get Enum Value from string.
        /// Modified method which is referenced from https://stackoverflow.com/questions/23563960/how-to-get-enum-value-by-string-or-int
        /// </summary>
        public static T GetEnumValue<T>(string str) where T : struct, Enum
        {
            if (!string.IsNullOrEmpty(str))
            {
                foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
                {
                    if (enumValue.ToString().ToUpper().Equals(str.ToUpper()))
                    {
                        return enumValue;
                    }
                }
            }

            throw new Exception("No Enum Found.");
        }
    }
}
