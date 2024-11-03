using System.Security.Cryptography;
using System.Text;
using RecipeWebsite.Domain.AccountEntity;

namespace RecipeWebsite.Domain.Tests;

public class AccountEntityTest
{
    [Test]
    public void AccountEmailTest()
    {
        var validEmail = Email.Create("some-valid_email@gmail.com");
        var invalidEmail1 = Email.Create("some invalid email@domain.org");
        var invalidEmail2 = Email.Create("invalid.email@google/org");
        
        Assert.Multiple(() =>
        {
            Assert.That(validEmail.IsSuccess, Is.EqualTo(true));
            Assert.That(invalidEmail1.IsSuccess, Is.EqualTo(false));
            Assert.That(invalidEmail2.IsSuccess, Is.EqualTo(false));
        });
    }

    [Test]
    public void AccountNicknameTest()
    {
        var valid1 = Nickname.Create("SomeValidNickname");
        var valid2 = Nickname.Create("Another-Valid_Nick");
        var valid3 = Nickname.Create("short_");
        var invalid1 = Nickname.Create("Some invalid nick");
        var invalid2 = Nickname.Create("InvalidNick.");
        var invalid3 = Nickname.Create("Inval*dNick?");
        var invalid4 = Nickname.Create("short");
        var invalid5 = Nickname.Create("SOOOOOOOOOOOOOOOOOOOOOOOOOOOBIIIIIIG_______NICK");
        
        Assert.Multiple(() =>
        {
            Assert.That(valid1.IsSuccess, Is.EqualTo(true));
            Assert.That(valid2.IsSuccess, Is.EqualTo(true));
            Assert.That(valid3.IsSuccess, Is.EqualTo(true));
            Assert.That(invalid1.IsSuccess, Is.EqualTo(false));
            Assert.That(invalid2.IsSuccess, Is.EqualTo(false));
            Assert.That(invalid3.IsSuccess, Is.EqualTo(false));
            Assert.That(invalid4.IsSuccess, Is.EqualTo(false));
            Assert.That(invalid5.IsSuccess, Is.EqualTo(false));
        });
    }

    [Test]
    public void AccountPasswordTest()
    {
        const string rawPassword = "Some interesting password...!";
        var encryptedPassword = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(rawPassword))).ToLower();

        var valid = Password.Create(encryptedPassword);
        var invalid = Password.Create(rawPassword);
        
        Assert.Multiple(() =>
        {
            Assert.That(valid.IsSuccess, Is.EqualTo(true));
            Assert.That(invalid.IsSuccess, Is.EqualTo(false));
        });
    }
}