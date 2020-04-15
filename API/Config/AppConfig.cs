using Domain.Contexts;
using RapidFireLib.Lib.Core;

namespace Api.Config
{
    public class AppConfig : IConfig
    {
        public string MyLocal = "";

        public void Configure(ref Configuration configuration)
        {
            configuration.DB.DefaultDbContext = new DefaultMSSQLContext(SAASType.NoSaas);
            configuration.APP.User = new testUser() { UserName = "abc", UserId = "123" };

            configuration.FCM.RequestUri = "https://fcm.googleapis.com/fcm/send";
            configuration.FCM.ServerKey = @"AAAAuYZIzbo:APA91bFDt1ekYu2n_HfpQNn1M69bdWJPSDL2o-84nLZELW3YKObVly-f9UzaFxYR_RCE2v7qRgtyr
                                                CTOM8G8V0IsRbonaxI-lzJ0tkdhHrJ36u-ETOtJdP6Tc1qSHOdUfpdiYczT1YnT";
            configuration.FCM.SenderId = "796821867962";

            configuration.SMS.TextSMSRequestUri = "";
            configuration.JwtKeys.SecretKey = @"MIGpAgEAAiEAuMmqfAzvVKJpIieaQkfC8BlZACwoOZssBCc/HIphNXcCAwEAAQIg
                                            Givly4ABfZkrDr1RKcYEI8Oyi9IoYes6eiO2fU1ALIECEQDe3gSNIlRk7Y8isu+Y
                                            qS1hAhEA1EJmx1b6rhjMxd4r2SG51wIQVylfgE7/0KU0CK8Qk5T+oQIQOI1cft3g
                                            ukPnQwy3mAlRTwIQDeu1TMQl74QOdaI3YZ5voA==";
        }

        public class testUser
        {
            public string UserName { get; set; }
            public string UserId { get; set; }
        }
    }
}