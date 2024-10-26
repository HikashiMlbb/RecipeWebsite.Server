namespace RecipeWebsite.Domain.RecipeEntity;

public class Rating
{
    public int Rate => ComputeRate();

    public List<int> Ratings;

    public Rating()
    {
        Ratings = [0, 0, 0, 0, 0];
    }

    private int ComputeRate()
    {
        var rate = (int)Stars.One * Ratings[0] 
                   + (int)Stars.Two * Ratings[1]
                   + (int)Stars.Three * Ratings[2] 
                   + (int)Stars.Four * Ratings[3] 
                   + (int)Stars.Five * Ratings[4];
        
        var votes = Ratings[0] + Ratings[1] + Ratings[2] + Ratings[3] + Ratings[4];
        return votes == 0 ? 0 : (int)Math.Round((float)rate / votes);
    }

    public void AddRate(Stars star)
    {
        Ratings[(int)star - 1]++;
    }
}