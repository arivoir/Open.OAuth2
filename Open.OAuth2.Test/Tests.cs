using System.Text.Json;

namespace Open.OAuth2.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestSerialization()
        {
            var authentiactionToken = new OAuth2Token
            {
                AccessToken = "Hello",
                RefreshToken = "world!",
                ExpiresIn = 3600,
                Type = "Bearer",
            };
            var serializedToken = JsonSerializer.Serialize(authentiactionToken);
            var deserializedAuthentiactionToken = JsonSerializer.Deserialize<OAuth2Token>(serializedToken);

            Assert.That(deserializedAuthentiactionToken, Is.Not.Null);
            Assert.That(deserializedAuthentiactionToken.AccessToken, Is.EqualTo("Hello"));
            Assert.That(deserializedAuthentiactionToken.RefreshToken, Is.EqualTo("world!"));
            Assert.That(deserializedAuthentiactionToken.ExpiresIn, Is.EqualTo(3600));
            Assert.That(deserializedAuthentiactionToken.Type, Is.EqualTo("Bearer"));
        }

        [Test]
        public void TestDeserialization()
        {
            var responseString = "{\"token_type\":\"Bearer\",\"scope\":\"Files.ReadWrite.All\",\"expires_in\":3599,\"ext_expires_in\":3599,\"access_token\":\"Hello world!\"}";

            var deserializedAuthentiactionToken = JsonSerializer.Deserialize<OAuth2Token>(responseString);

            Assert.That(deserializedAuthentiactionToken, Is.Not.Null);
            Assert.That(deserializedAuthentiactionToken.AccessToken, Is.EqualTo("Hello world!"));
            Assert.That(deserializedAuthentiactionToken.Scope, Is.EqualTo("Files.ReadWrite.All"));
            Assert.That(deserializedAuthentiactionToken.ExpiresIn, Is.EqualTo(3599));
            Assert.That(deserializedAuthentiactionToken.Type, Is.EqualTo("Bearer"));
        }
    }
}
