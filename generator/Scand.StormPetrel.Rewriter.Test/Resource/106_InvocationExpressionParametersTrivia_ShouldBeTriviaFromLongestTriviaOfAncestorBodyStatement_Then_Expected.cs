using System.Collections.Generic;

public class Foo
{
    public void ShouldBeCorrectTrivia()
    {
        //Assert
        actual.Should().Be(
            new List<string>()
            { 
                "102",
                "103"
            });
    }

    public void ShouldBeTriviaFromArgumentAndMethodBodyStatement()
    {
        //Assert
        actual.Should().Be(  new List<string>()
                                {
                                    "102",
                                    "103"
                                });
    }

    public void ShouldBeTriviaFromArgumentAndLongestTriviaFromAncestors()
    {
        //Assert
        actual
            .Should().Be(   new List<string>()
                            {
                                "102",
                                "103"
                            });
    }

    public void ShouldBeTriviaFromArgumentAndLongestTriviaFromArgumentList()
    {
        //Assert
        actual
            .Should
                ().Be(new List<string>()
                            {
                                "102",
                                "103"
                            });
    }

    public void ShouldBeTriviaFromCodeLineWithoutDot()
    {
        //Assert
        actual
            .Should().
                Be(new List<string>()
                            {
                                "102",
                                "103"
                            });
    }

    public void ShouldBeTriviaFromAssertEqual()
    {
        //Assert
        Assert
            .Equal(new List<string>()
                            {
                                "102",
                                "103"
                            }, actual
                                .ToList()); //ignore `.ToList()` trivia
    }

    public void ShouldBeTriviaFromLongestTriviaOfAncestorBodyStatement()
                                        {//This trivia should be ignored
        //Assert
        actual
            .Should()
                .Be(new List<string>()
                {
                    "1",
                    "2"
                },
                                [//This array trivia should be ignored
                                    "Longest trivia in argument"
                                ]);
        actual
                                    .Should().Be(1);//This trivia should be ignored
    }

    public void ShouldBeTriviaFromLongestTriviaOfExpressionBody()
                                    => //This trivia should be ignored
        actual
                .Should() //This trivia should be used
            .Be(new List<string>()
                            {
                                "102",
                                "103"
                            },
                                [//This array trivia should be ignored
                                    "Longest trivia in argument"
                                ]);
}
