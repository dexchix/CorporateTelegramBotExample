using DAL.Models.Enums;

namespace ServiseBot
{
    public class Helper
    {
        public static int GuidToInt(Guid guid)
        {
            byte[] bytes = guid.ToByteArray();
            int result = 0;

            foreach (byte b in bytes)
            {
                result += b;
            }

            return result;
        }
        public static RequestType GetOperationTypeToEnum(string operation)
        {
            return operation switch
            {
                "Заявка на переработку" => RequestType.Переработка,
                "Заявка на отпуск" => RequestType.Отпуск,
                "Заявка на отгул" => RequestType.Отгул,
                "Заявка на больничный" => RequestType.Больничный,
                _ => throw new ArgumentException("awdawd")
            };
        }

        public static string GetOperationTypeToString(RequestType? operation)
        {
            return operation switch
            {
                RequestType.Переработка => "Заявка на переработку",
                RequestType.Отпуск => "Заявка на отпуск",
                RequestType.Отгул => "Заявка на отгул",
                RequestType.Больничный => "Заявка на больничный",
                _ => throw new ArgumentException("awdawd")
            };
        }
    }
}
