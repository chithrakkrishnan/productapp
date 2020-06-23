using System.ComponentModel;

namespace SuperMarket.API.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        ///     @enum enum is key
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static string ToDescriptionString<TEnum>(this TEnum @enum)
        {
            var info = @enum.GetType().GetField(@enum.ToString());

            //finds all Description attributes applied over the enumeration value and stores their data into an array
            //(we can specify multiple attributes for a same property in some cases).
            var attributes = (DescriptionAttribute[]) info.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes?[0].Description ?? @enum.ToString();
        }
    }
}