using SCA.Shared.Entities.Enums;

namespace SCA.Shared.Utils
{
    public static class Functions
    {
        public static string GetStyleStatus(SensorStatus status)
        {
            switch (status)
            {
                case SensorStatus.Verde:
                    return "flag_green";
                case SensorStatus.Amarelo:
                    return "flag_yellow";
                case SensorStatus.Vermelho:
                    return "flag_red";
                case SensorStatus.Preto:
                    return "flag_black";
                default:
                    return "flag_gray";
            }
        }
    }
}
