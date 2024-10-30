using RecipeWebsite.Domain.CommentEntity;
using RecipeWebsite.SharedKernel.Constraints;

namespace RecipeWebsite.Domain.Tests;

public class CommentEntityTest
{
    [Test]
    public void CommentContentTest()
    {
        var validString1 = "Very interesting recipe! Nowadays that's my favorite recipe!";
        var validString2 = "I have a question, what if I use similar but not exactly ingredient?";
        var validString3 = new string('Y', CommentConstraints.MaxContentLength);
        var invalidString1 = new string('N', CommentConstraints.MaxContentLength + 10);
        var invalidString2 = "Hello, I wanna ask a question. <script src='...'>alert('You've been hacked!')</script>";

        var valid1 = CommentContent.Create(validString1);
        var valid2 = CommentContent.Create(validString2);
        var valid3 = CommentContent.Create(validString3);
        var invalid1 = CommentContent.Create(invalidString1);
        var invalid2 = CommentContent.Create(invalidString2);
        
        Assert.Multiple(() =>
        {
            Assert.That(valid1.IsSuccess, Is.EqualTo(true));
            Assert.That(valid2.IsSuccess, Is.EqualTo(true));
            Assert.That(valid3.IsSuccess, Is.EqualTo(true));
            Assert.That(invalid1.IsSuccess, Is.EqualTo(false));
            Assert.That(invalid2.IsSuccess, Is.EqualTo(false));
        });
    }
}