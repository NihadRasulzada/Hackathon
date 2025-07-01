namespace Application.Helpers
{
    public static class OTPGenerator
    {
        private static Random _random = new Random();
        public static string GenerateOtp(int length = 6)
        {
            string otp = string.Empty;

            for (int i = 0; i < length; i++)
            {
                otp += _random.Next(0, 10).ToString();
            }

            return otp;
        }
    }
}
